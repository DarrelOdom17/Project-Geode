using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject menuLayout;

    public void StartGame()
    {
        SceneManager.LoadScene("MovementTestScene");
        menuLayout.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#endif
        Debug.Log("Game has closed");
    }
}
