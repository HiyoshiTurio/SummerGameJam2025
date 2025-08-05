using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Net;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _gameTimer;
    [SerializeField] private int _countdownTimer = 3;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _timerText;
    public event Action<Player, int, int> OnScoreChanged;
    public event Action<float> OnTimerChanged;
    private float _countdownBuffer = 0f;
    //private int _score = 0;// 他のクラスから取得
    private float _timer = 0;
    private gamestate _gamestate = gamestate.countdown;
    private Dictionary<Player,int> _playerScore = new();
    
    public static GameManager Instance { get; private set; }
    private float Timer
    {
        get => _timer;
        set
        {
            _timer = value;
            OnTimerChanged?.Invoke(_timer);
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    private void Start()
    {
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
                    IngameStart();
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
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    void IngameStart()
    {
        _gamestate = gamestate.ingame;
        Timer = 60;
    }
    
    void UpdateScoreText(int score)
    {
        _scoreText.text = score.ToString();
    }
    
    private void TimerStop()
    {
        Time.timeScale = 0;
    }
    
    public void TimeResume()
    {
        Time.timeScale = 0;
    }

    public void AddScore(int value, Player player)
    {
        //Score += value;
        var beforeScore = _playerScore[player];
        var afterScore = _playerScore[player] + value;
        _playerScore[player] += value;
        OnScoreChanged?.Invoke(player, beforeScore, afterScore);
    }

    public int GetScore(Player player)
    {
        return _playerScore[player];
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
