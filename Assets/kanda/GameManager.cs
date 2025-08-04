using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private float _gameTimer;
    [SerializeField] private int _countdownTimer = 60;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _timerText;
    public event Action<int> OnScoreChanged;
    public event Action<float> OnCountdownChanged;
    private float _countdownBuffer = 0f;
    public bool _stopTime;
    private int _score = 0;// 他のクラスから取得
    private float _timer = 3;
    private gamestate _gamestate = gamestate.countdown;
    
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
        OnScoreChanged += UpdateScoreText;
        OnCountdownChanged += UpdateTimerText;
        _stopTime = true;
    }
    
    private void Update()
    {
        // if (!_stopTime)
        // {
        //     _countdownBuffer += Time.deltaTime;
        //
        //     if (_countdownBuffer >= 1f)
        //     {
        //         _countdownBuffer = 0f;
        //
        //         Timer--;
        //
        //         if (_countdownTimer <= 0)
        //         {
        //             TimerStop();
        //         }
        //     }
        // }

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
                    Debug.Log("Finish Game");
                }
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Score++;
        }
    }

    void ingameStart()
    {
        _gamestate = gamestate.ingame;
        Timer = 60;
        
    }
    
    public int Score
    {
        get { return _score; }
        private set
        {
            if (OnScoreChanged != null) OnScoreChanged(value);
            _score = value;
        }
    }
    
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
        _timerText.text = timer.ToString();
    }
    
    public void TimerStop()
    {
        Time.timeScale = 0;
        _stopTime = true;
    }
    
    public void TimeResume()
    {
        Time.timeScale = 1;
        _stopTime = false;
    }
}

enum gamestate
{
    countdown,
    ingame,
}