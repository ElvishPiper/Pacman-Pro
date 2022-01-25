using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    bool isGamePaused = false;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject levelCompleteScreen;
    [SerializeField] AudioClip gameOverMusic, victoryMusic;
    AudioSource audioSource;
    PlayerHealth playerHealth;
    AudioSource bg_music; 

    private void Awake() {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        bg_music = GameObject.Find("Bg_music")?.GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseResumeGame();

        if (Input.GetKeyDown(KeyCode.F1))
            LevelComplete();
    }

    public void GoToMainMenu(){
        ScoreManager.Instance.resetScore();
        playerHealth.resetLives();
        SceneManager.LoadScene("Menus");
    }

    public void PlayAgain(){
        ScoreManager.Instance.resetScore();
        playerHealth.resetLives();
        SceneManager.LoadScene("Level 1");
    }
    

    public void PauseResumeGame()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            bg_music.Pause();
        }
        else
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            bg_music.UnPause();
        }
    }

    public void GameOver(){
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        Text scoreText = gameOverScreen.transform.Find("ScoreText").GetComponent<Text>();
        scoreText.text = "Your Score: "+ScoreManager.Instance.getScore();
        bg_music.Pause();
        audioSource.PlayOneShot(gameOverMusic);
    }   
    
    public void LevelComplete(){
        Time.timeScale = 0;
        levelCompleteScreen.SetActive(true);
        Text scoreText = levelCompleteScreen.transform.Find("ScoreText").GetComponent<Text>();
        scoreText.text = "Your Score: " + ScoreManager.Instance.getScore();
        bg_music.Pause();
        audioSource.PlayOneShot(victoryMusic);
    }

    public void NextLevel(){

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
            SceneManager.LoadScene(nextSceneIndex);
        
    }
}
