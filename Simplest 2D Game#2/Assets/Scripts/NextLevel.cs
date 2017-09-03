﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public AudioClip aud;
    public Rigidbody2D player;
    public PlayerController playerC;
   
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        StopCoroutine(Load());
        StartCoroutine(Load()); //to add delay to play sound when level is completed
    }

    IEnumerator Load()
    {
        this.GetComponent<Collider2D>().enabled = false;
        playerC.enabled = false;
        AudioSource.PlayClipAtPoint(aud, this.transform.position);
        yield return new WaitForSecondsRealtime(3);
        if (SceneManager.GetActiveScene().buildIndex >=2)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}