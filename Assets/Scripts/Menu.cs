using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject EndCanvas;
    public TMPro.TextMeshProUGUI pauseTxt, finalScoreTxt;
    public static bool bActive;

    public void PlayButton()
    {
        SceneManager.LoadScene("Main");
    }
    public void ExitButton()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            ManagePause();
            ManagePauseText();
        }
    }

    private void ManagePause()
    {
        if (!GameController.bEnd)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !bActive)
            {
                EndCanvas.SetActive(true);
                Time.timeScale = 0;
                bActive = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && bActive)
            {
                EndCanvas.SetActive(false);
                Time.timeScale = 1;
                bActive = false;
            }
        }
    }

    private void ManagePauseText()
    {
        if (GameController.bEnd)
        {
            pauseTxt.text = "You`ve lost all your cities!";
            finalScoreTxt.text = "Your score is: " + GameController.userScore.ToString() + "." +
            "\nYou riched the " + GameController.stage.ToString() + " stage.";
        }
        else
        {
            pauseTxt.text = "PAUSE";
            finalScoreTxt.text = null;
        }
    }

    private void OnDisable()
    {
        bActive = false;
        Time.timeScale = 1;
    }
}
