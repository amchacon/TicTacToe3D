using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour 
{
    [SerializeField] private Text endGameText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button backMenuButton;
    public float gameOverInfoDelay;
    WaitForSeconds timer;

    private void Start()
    {
        gameOverInfoDelay = 0.75f;
        timer = new WaitForSeconds(gameOverInfoDelay);
        backMenuButton.onClick.RemoveAllListeners();
        backMenuButton.onClick.AddListener(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        });

        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        });
    }

    public IEnumerator ShowGameOverInfo(int pID)
    {
        yield return timer;
        gameOverPanel.SetActive(true);
        if (pID > 0)
        {
            endGameText.text = string.Format("Player {0} won!!!", pID);
        }
        else
        {
            endGameText.text = string.Format("It's a Draw!!!");
        }
    }
}
