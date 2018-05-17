//TODO: implement a visual feedback of who's playing at the current turn
//TODO: implement a ranking/score system

using UnityEngine;
using System.Linq;

[RequireComponent(typeof(GridManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public ReplayManager replayManager;
    public GameConfig config;
    [SerializeField] private Transform parentHolder;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private GUIController guiController;
    private int playerID;
    private int turnCount;
    private int winner;
    private int lineWinner;

    private void Reset()
    {
        gridManager = GetComponent<GridManager>();
        if (gridManager == null)
        {
            gridManager = gameObject.AddComponent<GridManager>();
        }
        guiController = FindObjectOfType<GUIController>();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// SetUp the gameplay
    /// </summary>
    private void Start()
    {
        gridManager.enabled = true;
        playerID = 0;
        turnCount = 0;
        winner = -1;
        lineWinner = -1;
        gridManager.GridSetup();
        gridManager.CreateValidationList();
        replayManager = new ReplayManager();
    }

    /// <summary>
    /// Called after player's move. Process the player input
    /// </summary>
    public void EndTurn(Vector3 pos, Tile tile)
    {
        pos += Vector3.up * 3;
        Instantiate(config.playersInGame[playerID], pos, Quaternion.identity, parentHolder);
        tile.ownerID = playerID+1;
        //Persist the player movement
        replayManager.UpdateInfo(turnCount, playerID, tile);
        turnCount++;
        //After 3 moves of the Player 1, starts to check if the game is over and someone won
        if (turnCount > 4)
        {
            if (WinnerCheck())
            {
                GameOver(winner);
            }
        }
        //Change turn (who's must play after)
        playerID = (playerID == 0) ? 1 : 0;
        //Ending in a Draw
        if (turnCount >= Mathf.Pow((int)config.gameMode,2) && winner == -1)
        {
            GameOver(winner);
        }
    }

    /// <summary>
    /// Checks if the current player won the match
    /// </summary>
    private bool WinnerCheck()
    {
        int check = gridManager.winnerLines.FindIndex(l => l.lines.Sum(o => o.ownerID) == (int)config.gameMode * (playerID + 1));
        if (check != -1)
        {
            winner = playerID;
            lineWinner = check;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Called when game is over (with winner or not)
    /// </summary>
    private void GameOver(int whoWon)
    {
        //Disables interactions and shows win effects
        gridManager.OnGameEnd(lineWinner);
        //Show on screen feedback message
        StartCoroutine(guiController.ShowGameOverInfo(winner + 1));
        //Persists final replay file
        replayManager.PersistFinalReplayFile();

        #if DEV
        Debug.Log(string.Format("Player {0} won on row {1}", whoWon, lineWinner));
        foreach (var item in replayManager.replayInfo)
        {
            Debug.Log(string.Format("Replay Info: #{0}|Player: {1}|Tile: {2}", item.Key, item.Value.playerID, item.Value.tileID));
        }
        #endif
    }

    private void OnDestroy()
    {
        replayManager.PersistFinalReplayFile();
    }
}