using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("制限時間"),SerializeField]private int _limit = 100;
    [SerializeField]private TextMeshProUGUI _timerUI;
    private float _currentTime;//残り時間
    public bool finish;

    // Start is called before the first frame update
    void Start()
    {
        finish = false;
        //タイマーの初期化
        _currentTime = _limit + 1f;
        TimerTextUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;

            if(_currentTime < 0)
            {
                _currentTime = 0;
                finish = true;
            }
            TimerTextUpdate();
        }
    }
    /// <summary>
    /// タイマーの更新
    /// </summary>
    void TimerTextUpdate()
    {
        int _minutes = Mathf.FloorToInt(_currentTime / 60);
        int _seconds = Mathf.FloorToInt(_currentTime % 60);
        _timerUI.text = string.Format("{0:00}:{1:00}",_minutes, _seconds);
    }
}
