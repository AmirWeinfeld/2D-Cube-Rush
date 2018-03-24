using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public GameObject menu, pauseButton, highScore;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (Time.timeScale == 1 || Time.timeScale == 0))
        {
            if (menu)
            {
                ShowMenu();
                pauseButton.SetActive(!pauseButton.activeInHierarchy);
                highScore.SetActive(!highScore.activeInHierarchy);
            }
        }
    }

    public void OnClickReset()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowMenu()
    {
        menu.SetActive(!menu.activeInHierarchy);
        if (Time.timeScale != 1)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }
}
