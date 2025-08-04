using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    //タイマーとスコア
    private static GameManager _instance;
    [SerializeField] private float _gameTimer;
    [SerializeField] private int _countdownTimer;
    [SerializeField] private Text _text;
    public event Action<int> OnScoreChanged;
    public bool _stopTime;
    private int _score;// 他のクラスから取得
    
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
        _stopTime = true;
    }

    private void Update()
    {
        if (!_stopTime)
        {
            _gameTimer += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Score++;
        }
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
        _text.text = score.ToString();
    }

    public int GetScore()
    {
        return _score;
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
