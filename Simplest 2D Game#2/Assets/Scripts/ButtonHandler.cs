using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour {

    #region Initializations
    public AudioClip aud;
    string highscoreSave = "";
    #endregion Initializations;

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

    public void ResetHighScore()
    {
        AudioSource.PlayClipAtPoint(aud, new Vector3(0, 0, 0));

        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt("Highscore" + i.ToString(), 0);
        }
    }
}
