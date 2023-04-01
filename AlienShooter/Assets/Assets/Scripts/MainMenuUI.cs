using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject play;
    public GameObject help;
    public GameObject quit;

    public GameObject easy;
    public GameObject medium;
    public GameObject hard;

    void Start()
    {
        easy.SetActive(false);
        medium.SetActive(false);
        hard.SetActive(false);
    }
    public void Play()
    {
        //SceneManager.LoadScene("SampleScene");
        easy.SetActive(true);
        medium.SetActive(true);
        hard.SetActive(true);

        play.SetActive(false);
        help.SetActive(false);
        quit.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Easy()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Medium()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Hard()
    {
        SceneManager.LoadScene("Level3");
    }

}
