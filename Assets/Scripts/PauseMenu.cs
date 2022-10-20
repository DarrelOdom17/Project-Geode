using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject resumeButton;
    [SerializeField]
    GameObject mainMenuButton;
    [SerializeField]
    GameObject optionsButton;
    [SerializeField]
    GameObject quitButton;

    [SerializeField]
    GameObject menuLayout;

    //Gets reference to player canvas in hierarchy
    private GameObject playerCanvas;
    private GameObject gun;
    public GameObject winCanvas;
    public GameObject GameUI;

    public static bool gameIsPaused = false;
    

    void Start()
    {
        menuLayout.SetActive(false);
        //winCanvas.SetActive(false);
        GameUI = GameObject.Find("GameUI");
        GameObject.Find("AudioManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!gameIsPaused)
                Pause();
            else
                Resume();
        }
    }

     public void Pause()
    {
        menuLayout.SetActive(true);
        resumeButton.SetActive(true);
        mainMenuButton.SetActive(true);
        optionsButton.SetActive(true);
        gameIsPaused = true;

        Time.timeScale = 0f;
    }

    public void Resume()
    {
        menuLayout.SetActive(false);
        resumeButton.SetActive(false);
        mainMenuButton.SetActive(false);
        optionsButton.SetActive(false);
        gameIsPaused = false;

        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
        // Application.Quit();
    }
    public void QuitGame()
    {

#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#endif
        Debug.Log("Game has closed!");
    }
}
