using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    #region Singleton
    public static Door instance { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public Animator needKeyText;
    public Animator nextLevel;

    private SpriteRenderer sprite;

    public bool hasKey;

    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
		sprite.color = new Color(1f,1f,1f,0.15f);
    }

    void Update()
    {
        if (hasKey == true)
        {
            sprite.color = new Color(1f,1f,1f,1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            if (hasKey == false)
            {
                needKeyText.SetBool("Activate", true);
            } else 
            {
                AudioManager audioManager = AudioManager.instance;
                audioManager.PlaySFX("door");
                nextLevel.SetTrigger("Next");
                GameManager.instance.isFinished = true;  
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {    
        if (other.CompareTag("Player"))
        {
            if (hasKey == false)
            {
                needKeyText.SetBool("Activate", false);
            }
        }
    }
}
