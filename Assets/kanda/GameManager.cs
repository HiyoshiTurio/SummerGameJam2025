using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private float _inGameTime = 60;
    public event Action<Player, int> OnScoreChanged;
    public event Action<float> OnTimerChanged;
    private float _timer = 5;
    private gamestate _gamestate = gamestate.countdown;

    private PlayerScore[] _playerScores = new[] 
        { new PlayerScore(){Player = Player.One,Score = 0}, new PlayerScore(){Player = Player.Two,Score = 0} };
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
        foreach (var VARIABLE in _playerScores)
        {
            VARIABLE.OnScoreChanged += OnScoreChanged;
        }

        StartCoroutine(timer.CountDown());
    }
    
    private void Update()
    {
        switch (_gamestate)
        {
            case gamestate.countdown:
            {
                _timer -= Time.deltaTime;
                if (_timer < 0)
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
                }
                break;
            }
        }
    }
    
    void IngameStart()
    {
        _gamestate = gamestate.ingame;
        Timer = 60;
    }
    
    private void TimerStop()
    {
        Time.timeScale = 0;
    }

    public void AddScore(int value, Player player) { _playerScores[(int)player].Score += value; }

    public (int,int) GetScore()
    {
        return (_playerScores[0].Score, _playerScores[1].Score);
    }
}

public class PlayerScore
{
    private int _score;
    public Player Player;
    

    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            OnScoreChanged?.Invoke(Player,value);
        }
    }
    public event Action<Player,int> OnScoreChanged;
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
