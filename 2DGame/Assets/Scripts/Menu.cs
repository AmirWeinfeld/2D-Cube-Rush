using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour {
    public GameObject menu, pauseButton, highScore;

    public TextMeshProUGUI highScoreText;

    public void OnClickReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickLoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ShowMenu()
    {
        menu.SetActive(!menu.activeInHierarchy);
        if (Time.timeScale != 1)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
        int scoreInt = 0;

        if (SceneManager.GetActiveScene().buildIndex == 1) // in normal game
        {
            scoreInt = PlayerPrefs.GetInt("HighScoreGame");
        }
        else // in stack game
        {
            scoreInt = PlayerPrefs.GetInt("HighScoreStack");
        }

        highScoreText.text = "High Score: " + scoreInt;
    }
}
