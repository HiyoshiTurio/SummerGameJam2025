using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using DG.Tweening;

public class Timer : MonoBehaviour
{
    [Header("カウントダウン画像"), SerializeField] private Image[] _countdown;
    [Header("終了の画像"), SerializeField] private Image _finishImage;
    [Header("終了の画像のアニメーション時間"),SerializeField] private float _delay = 4f;
    [Header("制限時間"),SerializeField]private int _limit = 100;
    [Header("リザルト"), SerializeField] private Image _result;
    [SerializeField]private TextMeshProUGUI _timerUI;
    private float _currentTime;//残り時間
    private bool Wait;

    // Start is called before the first frame update
    void Start()
    {
        _result.gameObject.SetActive(false);
        _finishImage.gameObject.SetActive(false);
        foreach (var image in _countdown)
        {
            image.gameObject.SetActive(false);
        }
        Wait = true;
        StartCoroutine(CountDown());
        //タイマーの初期化
        _currentTime = _limit;
        TimerTextUpdate();
    }
    /// <summary>
    /// カウントダウン
    /// </summary>
    /// <returns></returns>
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < _countdown.Length; i++)
        {
            _countdown[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            Destroy( _countdown[i].gameObject );
        }
        yield return new WaitForSeconds(1f);
        Wait = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (_currentTime > 0 && !Wait)
        {
            _currentTime -= Time.deltaTime;

            if(_currentTime < 0)
            {
                _currentTime = 0;
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
        if(_minutes == 0 && _seconds == 0)
        {
            StartCoroutine(FinishUI());
        }
    }
    IEnumerator FinishUI()
    {
        _finishImage.gameObject.SetActive(true);
        _finishImage.rectTransform.DOAnchorPosX(0f, _delay);
        yield return new WaitForSeconds(_delay + 3f);
        _finishImage.gameObject.SetActive(false);
        _result.gameObject.SetActive(true);
    }
}
