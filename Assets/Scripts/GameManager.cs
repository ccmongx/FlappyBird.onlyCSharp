using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Player player;
    private Spawner spawner;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject panel;

    public int score { get; private set; }
    private void Awake(){
        Application.targetFrameRate = 60;

        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        Pause();
    }

    public void Play(){
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        exitButton.SetActive(false);
        panel.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++) {
            Destroy(pipes[i].gameObject);
        }

        UpdateHighScore();
    }

    public void GameOver(){
        playButton.SetActive(true);
        gameOver.SetActive(true);
        exitButton.SetActive(true);
        panel.SetActive(true);

        UpdateHighScore();
        Pause();
    }

    public void Pause(){
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void IncreaseScore(){
        score++;
        scoreText.text = score.ToString();
    }

    public void Exit(){
        SceneManager.LoadScene(0);
    }

    private void UpdateHighScore(){
        int highScore = PlayerPrefs.GetInt("highScore", 0);

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
        }

        highScoreText.text = highScore.ToString();
    }
}