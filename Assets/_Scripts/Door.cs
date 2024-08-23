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

    private SpriteRenderer sprite;

    public bool hasKey;

    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
		sprite.color = new Color(1f,1f,1f,0.25f);
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
        if (hasKey == false)
        {
            Debug.Log("Need a key");
        }
    }
}
