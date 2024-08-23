using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;

    public GameObject pausePanel;
    public Animator transitionAnim;
    
    void Start()
    {
        pausePanel.SetActive(false);    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            UpdatePauseState();
        }
    }
    
    void UpdatePauseState()
    {
        pausePanel.SetActive(isPaused);
        // GUI.SetActive(!isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void Action(string name)
    {
        switch (name)
        {
            case "Resume":
            isPaused = !isPaused;
            UpdatePauseState();
            break;
            case "Main Menu": StartCoroutine(GoToMainMenu()); break;
        }
    }
    
    IEnumerator GoToMainMenu()
    {
        Time.timeScale = 1f;
        transitionAnim.SetTrigger("Transition");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Main Menu");
    }
}
