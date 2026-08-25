using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject rankingMenu;
    [SerializeField]
    private GameObject pauseButton;
    [SerializeField]
    private GameObject changeNameMenu;

    public static MenuManager menu;

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);

        Time.timeScale = 0f;
    }

    public void ReanudeGame()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);

        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 1f;
    }

    public void OpenRanking()
    {
        rankingMenu.SetActive(true);
    }

    public void CloseRanking()
    {
        rankingMenu.SetActive(false);
    }

    public void OpenChangeName()
    {
        changeNameMenu.SetActive(true);
    }

    public void CloseChangeName()
    {
        changeNameMenu.SetActive(false);
    }
}
