using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;

    public void StartGame()
    {
        SceneManager.LoadScene("RoomDeck");
    }
    public void OpenMainMenu()
    {
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void OpenCreditsMenu()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
}
