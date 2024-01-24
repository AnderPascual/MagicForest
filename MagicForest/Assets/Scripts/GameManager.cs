using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public bool playerIsDead = false;
    public static int level = 1;
    private static int lives = 3;
    private int numKeyPlantsInLevel;
    private int numKeyPlantsCollected = 0;
    private static Timer timer;
    private int numLevels = 3;

    [SerializeField] private AudioSource music;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject spikes;
    [SerializeField] private Image[] livesUI;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TextMeshProUGUI textNumKeyPlants;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitializeTimer();//Para que arranque el timer
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        ManageLivesUI();
        UpdateTimer();
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu != null)
        {
            if (Time.timeScale == 0f)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        SetNumKeyPlantsInLevel();
        UpdateKeyPlantsCounter();
        if (music != null)
        {
            music.loop = true;
            music.Play();

        }
        if (SceneManager.GetActiveScene().name == "GameOverScene")
        {
            StartCoroutine(WaitCredits());
        }
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
    public void IncrementLife()
    {
        if (lives < 3)
        {
            lives++;
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
        else if (level > numLevels)
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            if (Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
            }
            playerIsDead = false;
            SetNumKeyPlantsInLevel();
            SceneManager.LoadScene(level);
        }
    }

    IEnumerator WaitCredits()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("CreditsScene");

    }

    private void SetNumKeyPlantsInLevel()
    {
        switch (level)
        {
            case 1:
                numKeyPlantsInLevel = 1;
                break;
            case 2:
                numKeyPlantsInLevel = 2;
                break;
            case 3:
                numKeyPlantsInLevel = 3;
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
        timer.RestartTimer();
        LoadLevel();
    }

    public void GetKey()
    {
        numKeyPlantsCollected++;
        if (numKeyPlantsCollected == numKeyPlantsInLevel)
        {
            Destroy(spikes);
        }
        UpdateKeyPlantsCounter();
    }

    public void QuitGame() 
    {
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

    private void InitializeTimer()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.timerText = timerText; 
    }

    private void UpdateTimer()
    {
        if (timer != null)
        {
            timer.UpdateTimer();
        }
    }

    private void UpdateKeyPlantsCounter()
    {
        if (textNumKeyPlants != null)
        {
            textNumKeyPlants.text = numKeyPlantsCollected + "/" + numKeyPlantsInLevel;
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        if (music != null)
        {
            music.Pause();
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        if (music != null)
        {
            music.UnPause();
        }
    }
}
