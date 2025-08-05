using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private float _gameTimer;
    [SerializeField] private int _countdownTimer = 60;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _timerText;
    public event Action<Player, int, int> OnScoreChanged;
    public event Action<float> OnCountdownChanged;
    private float _countdownBuffer = 0f;
    public bool _stopTime;
    //private int _score = 0;// 他のクラスから取得
    private float _timer = 3;
    private gamestate _gamestate = gamestate.countdown;
    private Dictionary<Player,int> _playerScore = new Dictionary<Player, int>();
    
    public float GameTime => _gameTimer;
    
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
    
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        // OnScoreChanged += UpdateScoreText;
        OnCountdownChanged += UpdateTimerText;
        _stopTime = true;
        _playerScore[Player.One]  = 0;
        _playerScore[Player.Two] = 0;
    }
    
    private void Update()
    {
        switch (_gamestate)
        {
            case gamestate.countdown:
            {
                Timer -= Time.deltaTime;
                if (Timer <= 0)
                {
                    ingameStart();
                }
                break;
            }
            case gamestate.ingame:
            {
                Timer -= Time.deltaTime;
                if (Timer <= 0)
                {
                    Timer = 0;
                    TimerStop();
                    Debug.Log("Finish Game");// シーン遷移先に書き替える
                }
                break;
            }
        }

        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     Score++;
        // }
    }
    
    void ingameStart()
    {
        _gamestate = gamestate.ingame;
        Timer = 60;
    }
    
    // public int Score
    // {
    //     get => _score;
    //     private set
    //     {
    //         _score = value;
    //         OnScoreChanged?.Invoke(_score);
    //     }
    // }
    
    void UpdateScoreText(int score)
    {
        _scoreText.text = score.ToString();
    }
    
    public float Timer
    {
        get { return _timer; }
        private set
        {
            _timer = value;
            OnCountdownChanged?.Invoke(_timer);
        }
    }
    
    void UpdateTimerText(float timer)
    {
        _timerText.text = Mathf.CeilToInt(timer).ToString();
    }
    
    public void TimerStop()
    {
        Time.timeScale = 0;
        _stopTime = true;
    }
    
    public void TimeResume()
    {
        Time.timeScale = 0;
        _stopTime = true;
    }

    public void AddScore(int value, Player player)
    {
        //Score += value;
        var beforeScore = _playerScore[player];
        var afterScore = _playerScore[player] + value;
        _playerScore[player] += value;
        OnScoreChanged?.Invoke(player, beforeScore, afterScore);
    }
}

enum gamestate
{
    countdown,
    ingame,
}

public enum Player
{
    One,
    Two,
}