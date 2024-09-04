using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button[] buttons;

    MainMenu mainMenu;

    private void Start() {
        mainMenu = gameObject.GetComponent<MainMenu>();
    }

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
    }

    public void SelectLevel(string levelName)
    {
        StartCoroutine(LoadLevel(levelName));
    }

    IEnumerator LoadLevel(string name)
    {
        mainMenu.transitionAnim.SetTrigger("Transition");
        AudioManager audioManager = AudioManager.instance;
        audioManager.PlaySFX("transition");

        yield return new WaitForSeconds(1f);
        
        SceneManager.LoadScene(name);
    }
}
