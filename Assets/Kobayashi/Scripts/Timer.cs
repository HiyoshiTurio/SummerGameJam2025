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
    Result Result;

    void Start()
    {
        Result = FindObjectOfType<Result>();
        GameManager.Instance.OnTimerChanged += TimerTextUpdate;
        _result.gameObject.SetActive(false);
        _finishImage.gameObject.SetActive(false);
        foreach (var image in _countdown)
        {
            image.gameObject.SetActive(false);
        }
        StartCoroutine(CountDown());
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
    }

    /// <summary>
    /// タイマーの更新
    /// </summary>
    void TimerTextUpdate(float time)
    {
        int _minutes = Mathf.FloorToInt(time / 60);
        int _seconds = Mathf.FloorToInt(time % 60);
        _timerUI.text = string.Format("{0:00}:{1:00}",_minutes, _seconds);
        if(time <= 0)
        {
            if (!OvertimeChecker())
            {
                StartCoroutine(FinishUI());
            }
        }
    }
    bool OvertimeChecker()
    {         
        (int ,int) scores = GameManager.Instance.GetScore();
        var playerOneResultScore = scores.Item1;
        var playerTwoResultScore = scores.Item2;
        if(playerOneResultScore == playerTwoResultScore)
        {
            return true;
        }
        return false;
    }
    
    /// <summary>
    /// 終了の表示、アニメーション
    /// </summary>
    /// <returns></returns>
    IEnumerator FinishUI()
    {
        _finishImage.gameObject.SetActive(true);
        _finishImage.rectTransform.DOAnchorPosX(0f, _delay);
        yield return new WaitForSeconds(_delay + 3f);
        _finishImage.gameObject.SetActive(false);
        Result.ResultUI();
    }
}
