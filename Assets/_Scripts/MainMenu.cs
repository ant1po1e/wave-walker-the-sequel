using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transitioAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectLevel()
    {
        StartCoroutine(Transition());
    }

    public void ExitGame()
    {
        StartCoroutine(ExitTransition());
    }

    private IEnumerator ExitTransition()
    {
        transitioAnimator.SetTrigger("Transition");
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }

    private IEnumerator Transition()
    {
        transitioAnimator.SetTrigger("Transition");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level 1");
    }
}
