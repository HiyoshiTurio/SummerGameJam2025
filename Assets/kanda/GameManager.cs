using UnityEngine;
using System;
[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private float _inGameTime = 60;
    private bool _isFinish = false;
    public event Action<Player, int> OnScoreChanged;
    public event Action<float> OnTimerChanged;
    private float _timer = 5;
    private Gamestate _gamestate = Gamestate.Countdown;

    private PlayerScore[] _playerScores = new[] 
        { new PlayerScore{Player = Player.One,Score = 0}, new PlayerScore{Player = Player.Two,Score = 0} };
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
        foreach (var variable in _playerScores)
        {
            variable.OnScoreChanged += OnScoreChanged;
        }

        StartCoroutine(timer.CountDown());
        SoundManager.Instance.PlayBgm();
    }
    
    private void Update()
    {
        switch (_gamestate)
        {
            case Gamestate.Countdown:
            {
                _timer -= Time.deltaTime;
                if (_timer < 0)
                {
                    IngameStart();
                }
                break;
            }
            case Gamestate.Ingame:
            {
                Timer -= Time.deltaTime;
                if (Timer <= 0 && _isFinish == false)
                {
                    Timer = 0;
                    TimerStop();
                    SoundManager.Instance.StopBgm();
                    _isFinish = true;
                    timer.FinishUIA();
                }
                break;
            }
            case Gamestate.Suddendeath:
            {
                break;
            }
            case Gamestate.Idle:
            {
                break;
            }
        }
    }
    
    void IngameStart()
    {
        _gamestate = Gamestate.Ingame;
        Timer = _inGameTime;
    }
    
    private void TimerStop()
    {
        Time.timeScale = 0;
    }

    public void AddScore(int value, Player player)
    {
        _playerScores[(int)player].Score += value;
        Debug.Log("Add Score");
    }

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

[Serializable]
public enum Gamestate
{
    Countdown,
    Ingame,
    Idle,
    Suddendeath,
}
[Serializable]
public enum Player
{
    One,
    Two,
}
