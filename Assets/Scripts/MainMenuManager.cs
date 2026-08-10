using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject instructionsPanel;

    public void StartLearning()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ShowInstructions()
    {
        instructionsPanel.SetActive(true);
    }

    public void HideInstructions()
    {
        instructionsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}