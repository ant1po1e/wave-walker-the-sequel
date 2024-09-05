using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    #endregion

    private bool isPaused;
    private bool isDead;
    public bool isFinished;

    public GameObject GUI;
    public float jumpscareChance = 0.05f;

    public Animator pauseAnim;
    public Animator deadAnim;
    public Animator transitionAnim;
    
    void Start()
    {
        pauseAnim.SetBool("Is Paused", false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isDead == false || isFinished == false)
            {
                isPaused = !isPaused;
                UpdatePauseState();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isDead == true || isFinished == true)
                Action("Main Menu");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isDead == true)
                Action("Restart");
            else if (isFinished == true)
                Action("Next Level");
        }
    }
    
    void UpdatePauseState()
    {
        pauseAnim.SetBool("Is Paused", isPaused); 
        GUI.SetActive(!isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void PlayerDead()
    {
        isDead = true;

        if (Random.value < jumpscareChance)
        {
            TriggerJumpscare();
        }
        else
        {
            deadAnim.SetTrigger("Dead");
            AudioManager.instance.PlaySFX("dead");  
        }

        Time.timeScale = 0f;
    }

    void TriggerJumpscare()
    {
        AudioManager.instance.PlaySFX("jumpscare"); 
        deadAnim.SetTrigger("Jumpscare");
    }

    public void Action(string name)
    {
        switch (name)
        {
            case "Resume":
            isPaused = !isPaused;
            UpdatePauseState();
            break;

            case "Restart":
            StartCoroutine(ReloadScene());
            break;

            case "Next Level":
            StartCoroutine(NextLevel());
            break;

            case "Main Menu": 
            StartCoroutine(GoToMainMenu());
            break;
        }
    }
    
    IEnumerator GoToMainMenu()
    {
        Time.timeScale = 1f;
        AudioManager.instance.PlaySFX("transition");  
        transitionAnim.SetTrigger("Transition");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Main Menu");
    }

    IEnumerator ReloadScene()
    {
        Time.timeScale = 1f;
        AudioManager.instance.PlaySFX("transition");  
        transitionAnim.SetTrigger("Transition");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator NextLevel()
    {
        transitionAnim.SetTrigger("Transition");
        AudioManager.instance.PlaySFX("nextLevel");  
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }
}