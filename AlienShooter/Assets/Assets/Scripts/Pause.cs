using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public PlayerShooting playerShooting;
    public PlayerHealth playerHealth;
    float timetoScreen = 3f;

    public GameObject pauseButton;
    public GameObject resumeButton;

    public GameObject restartButton;
    public GameObject menuButton;

    public Text time;
    public int timeLeft = 180;
    public string level = "SampleScene";

    public GameObject winCanva;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        resumeButton.SetActive(false);
        restartButton.SetActive(false);
        menuButton.SetActive(false);

        StartCoroutine("LoseTime");
        Time.timeScale = 1;
        winCanva.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            pauseButton.SetActive(false);

            timetoScreen -= Time.deltaTime;
            if (timetoScreen <= 0)
            {
                StopCoroutine("LoseTime");
                restartButton.SetActive(true);
                menuButton.SetActive(true);
            }
        }

        time.text = ("Time Reamening: " + timeLeft);
        if (timeLeft == 0)
        {
            pauseButton.SetActive(false);
            Time.timeScale = 0;
            winCanva.SetActive(true);
        }
            //SceneManager.LoadScene(level);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        playerShooting.enabled = false;
        resumeButton.SetActive(true);
        pauseButton.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        playerShooting.enabled = true;
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(level);
    }
}
