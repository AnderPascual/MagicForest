using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public bool playerIsDead = false;
    public static int level = 1;
    private static int lives = 3;
    private int numKeyPlants;

    [SerializeField] private int numlevels = 2;
    [SerializeField] private GameObject Spikes;
    [SerializeField] private Image[] livesUI;
    


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        ManageLivesUI();
    }

    private void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        SetNumKeyPlantsInLevel();
    }

    public void DecreaseLife()
    {
        if (playerIsDead == false)
        {
            lives--;
            playerIsDead = true;
            StartCoroutine(WaitLoadLevel());
        }
    }

    IEnumerator WaitLoadLevel()
    {
        if (playerIsDead == true)
        {
            yield return new WaitForSeconds(0.8f);
            LoadLevel();
        }
        else
        {
            LoadLevel();
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    private void ManageLivesUI()
    {
        for(int i = 0; i < livesUI.Length; i++)
        {
            if (i < lives)
            {
                livesUI[i].enabled = true;
            }
            else
            {
                livesUI[i].enabled = false;
            }
        }
    }

    public void LoadLevel()
    {
        if (lives <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
        else if (level > numlevels)
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            playerIsDead = false;
            SetNumKeyPlantsInLevel();
            SceneManager.LoadScene(level);
        }
    }

    private void SetNumKeyPlantsInLevel()
    {
        switch (level)
        {
            case 1:
                numKeyPlants = 1;
                break;
            case 2:
                numKeyPlants = 2;
                break;
        }
    }

    public void LevelCompleted()
    {
        level++;
        StartCoroutine(WaitLoadLevel());
    }

    public void RestartGame()
    {
        level = 1;
        lives = 3;
        LoadLevel();
    }

    public void GetKey()
    {
        numKeyPlants--;
        if (numKeyPlants <= 0)
        {
            Destroy(Spikes);
        }
    }

    public void QuitGame() 
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
    public void StartCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
