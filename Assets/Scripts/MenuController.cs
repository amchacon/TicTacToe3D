using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour 
{
    public Text titleTxt;
    public GameConfig config;
    public GameObject gameModePanel;
    public GameObject playerSelectionPanel;

    public void SetGameMode(int mode)
    {
        config.gameMode = mode == 0 ? GameMode.MODE_3x3 : GameMode.MODE_4x4;
        gameModePanel.SetActive(false);
        playerSelectionPanel.SetActive(true);
        config.playersInGame.Clear();
        titleTxt.text = "Select Player 1";
    }

    public void SetPlayerInGame(int pID)
    {
        if (config.playersInGame.Count <= 0)
        {
            config.playersInGame.Add(config.playersPrefabs[pID]);
            titleTxt.text = "Select Player 2";
        }
        else
        {
            config.playersInGame.Add(config.playersPrefabs[pID]);
            SceneManager.LoadScene(1);
        }

    }

}
