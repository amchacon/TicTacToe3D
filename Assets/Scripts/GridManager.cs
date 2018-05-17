using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class GridManager : MonoBehaviour
{
    [SerializeField] private CameraController camController;
    [SerializeField] private Transform parentHolder;
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject tile = null;
    [HideInInspector] public List<WinnerLine> winnerLines;
    private List<Tile> tiles = new List<Tile>();
    private int size;

    private void Reset()
    {
        if(grid == null)
        {
            grid = gameObject.AddComponent<Grid>();
            grid.cellSize = new Vector3(3, 3, 0);
            grid.cellGap = new Vector3(0.2f, 0.2f, 0);
            grid.cellSwizzle = GridLayout.CellSwizzle.XZY;
            grid.cellLayout = GridLayout.CellLayout.Rectangle;
        }
        grid = GetComponent<Grid>();
        camController = FindObjectOfType<CameraController>();
    }
   
    internal void GridSetup ()
    {
        size = (int)GameManager.Instance.config.gameMode;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Tile newTile = Instantiate(tile, grid.GetCellCenterWorld(new Vector3Int(i, j, 0)), Quaternion.Euler(90,0,0)).GetComponent<Tile>();
                newTile.transform.SetParent(parentHolder);
                newTile.Id = string.Concat(i.ToString(), j.ToString());
                newTile.name = newTile.Id;
                tiles.Add(newTile);
                newTile.Initialize();
            }
        }

        //Finding the board center
        float midBoard = ((grid.cellSize.x + grid.cellGap.x) * size) / 2;
        camController.BoardCenter = new Vector3(midBoard, 0, midBoard);
    }

    internal void OnGameEnd(int lineWinner)
    {
        foreach (Tile t in tiles)
        {
            t.Disable();
            t.particles.Stop();
        }

        foreach (var item in winnerLines[lineWinner].lines)
        {
            item.particles.Play();
        }
    }

    internal void CreateValidationList()
    {
        GameMode gameMode = GameManager.Instance.config.gameMode;
        switch (gameMode)
        {
            case GameMode.MODE_3x3:
                //3x3
                //Cols
                winnerLines.AddNewWinnerLine(gameMode, tiles[0], tiles[1], tiles[2]);
                winnerLines.AddNewWinnerLine(gameMode, tiles[3], tiles[4], tiles[5]);
                winnerLines.AddNewWinnerLine(gameMode, tiles[6], tiles[7], tiles[8]);
                //Rows
                winnerLines.AddNewWinnerLine(gameMode, tiles[0], tiles[3], tiles[6]);
                winnerLines.AddNewWinnerLine(gameMode, tiles[1], tiles[4], tiles[7]);
                winnerLines.AddNewWinnerLine(gameMode, tiles[2], tiles[5], tiles[8]);
                //Transversal
                winnerLines.AddNewWinnerLine(gameMode, tiles[0], tiles[4], tiles[8]);
                winnerLines.AddNewWinnerLine(gameMode, tiles[2], tiles[4], tiles[6]);
                break;
            case GameMode.MODE_4x4:
                //4x4
                //Cols
                winnerLines.AddNewWinnerLine(gameMode, tiles[0], tiles[1], tiles[2], tiles[3]);
                winnerLines.AddNewWinnerLine(gameMode, tiles[4], tiles[5], tiles[6], tiles[7]);
                winnerLines.AddNewWinnerLine(gameMode, tiles[8], tiles[9], tiles[10], tiles[11]);
                winnerLines.AddNewWinnerLine(gameMode, tiles[12], tiles[13], tiles[14], tiles[15]);
                //Rows
                winnerLines.AddNewWinnerLine(gameMode, tiles[0], tiles[4], tiles[8], tiles[12]);
                winnerLines.AddNewWinnerLine(gameMode, tiles[1], tiles[5], tiles[9], tiles[13]);
                winnerLines.AddNewWinnerLine(gameMode, tiles[2], tiles[6], tiles[10], tiles[14]);
                winnerLines.AddNewWinnerLine(gameMode, tiles[3], tiles[7], tiles[11], tiles[15]);
                //Transversal
                winnerLines.AddNewWinnerLine(gameMode, tiles[0], tiles[5], tiles[10], tiles[15]);
                winnerLines.AddNewWinnerLine(gameMode, tiles[3], tiles[6], tiles[9], tiles[12]);
                break;
        }
    }
}

[System.Serializable]
public class WinnerLine
{
    public List<Tile> lines = new List<Tile>(3);
}