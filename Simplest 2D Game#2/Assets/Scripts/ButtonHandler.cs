using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour {

    public AudioClip aud;
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Restart()
    {
        AudioSource.PlayClipAtPoint(aud, new Vector3(0, 0, 0));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        AudioSource.PlayClipAtPoint(aud, new Vector3(0, 0, 0));
        SceneManager.LoadScene(0);
    }

    public void Play()
    {
        AudioSource.PlayClipAtPoint(aud, new Vector3(0, 0, 0));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        AudioSource.PlayClipAtPoint(aud, new Vector3(0, 0, 0));
        Application.Quit();
    }
}
