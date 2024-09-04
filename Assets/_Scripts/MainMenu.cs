using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Animator transitionAnim;
    public Animator settingAnim;
    public Animator levelSelectAnim;

    private bool settingActive;
    private bool levelSelectActive; 
    
    public void ActivateSetting()
    {
        settingActive = !settingActive;
        settingAnim.SetBool("Setting Active", settingActive);
    }

    public void ActivateLevelSelect()
    {
        levelSelectActive = !levelSelectActive;
        levelSelectAnim.SetBool("Level Active", levelSelectActive);
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
}
