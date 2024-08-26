using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transitionAnim;
    public Animator settingAnim;

    private bool settingActive;

    public void SelectLevel()
    {
        StartCoroutine(Transition());
    }
    
    public void ActivateSetting()
    {
        settingActive = !settingActive;
        settingAnim.SetBool("Setting Active", settingActive);
    }

    public void ExitGame()
    {
        StartCoroutine(ExitTransition());
    }

    private IEnumerator ExitTransition()
    {
        transitionAnim.SetTrigger("Exit");
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }

    private IEnumerator Transition()
    {
        transitionAnim.SetTrigger("Transition");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Basic");
    }
}
