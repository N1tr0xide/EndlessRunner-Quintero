using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private float _score = 0, _worldSpeed = 0;
    private const float MaxWorldSpeed = 30;
    private bool _isGameOver = false;
    private bool _gameStarted = false;

    [Header("UI")] 
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _startGamePanel;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _highScoreText;

    public float WorldSpeed => _worldSpeed;
    public bool IsGameOver => _isGameOver;
    public bool GameStarted => _gameStarted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameOverPanel.SetActive(false);
        _startGamePanel.SetActive(true);
    }

    public void StartGame()
    {
        _gameStarted = true;
        _startGamePanel.SetActive(false);
        FindFirstObjectByType<RandomObjectSpawner>().StartSpawning();
        IncreaseScore(0);
        StartCoroutine(IncreaseScoreEachSecond());
    }

    public void IncreaseScore(float byAmount, bool shouldIncreaseSpeed = true)
    {
        _score += byAmount;
        _scoreText.text = "Score: " + _score;
        
        if(!shouldIncreaseSpeed) return;
        _worldSpeed += .05f;
        print(_worldSpeed);
        if (_worldSpeed > MaxWorldSpeed) _worldSpeed = MaxWorldSpeed;
    }

    public void GameOver()
    {
        _isGameOver = true;
        FindFirstObjectByType<PlayerAnim>().PlayDeathAnim();

        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (_score > PlayerPrefs.GetFloat("HighScore"))
            {
                PlayerPrefs.SetFloat("HighScore", _score);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("HighScore", _score);
        }

        _highScoreText.text = "High Score: " + PlayerPrefs.GetFloat("HighScore");
        _gameOverPanel.SetActive(true);
    }

    #region UI Buttons

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    #endregion

    private IEnumerator IncreaseScoreEachSecond()
    {
        while (!IsGameOver)
        {
            yield return new WaitForSeconds(1);
            if(!_isGameOver) IncreaseScore(1, false);
        }
    }
}
