using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController main;
    public Spawner spawner;
    public Health playerHP;
    public Health bossHP;
    public GameObject WinScreenUI;
    public GameObject DeathScreenUI;


    private void Awake()
    {
        if(main != null)
        { Destroy(this); }
        else 
        { main = this; }

        HPLevels();
    }
    void Start()
    {
        HideEndScreens();
        Time.timeScale = 1f;
    }

    public void HideEndScreens()
    {
        WinScreenUI.gameObject.SetActive(false);
        DeathScreenUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Reset Game");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Exit the Game");
        }

        DeathScreen();
        WinScreen();
    }

    public void DeathScreen()
    {
        if(playerHP.currentHP <= 0)
        {
            DeathScreenUI.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

    }

    public void WinScreen()
    {
        if(bossHP.currentHP <= 0)
        {
            WinScreenUI.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

    }

    public void HPLevels()
    {
      
    }




}
