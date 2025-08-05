using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using DG.Tweening;

public class Timer : MonoBehaviour
{
    [Header("カウントダウン画像"), SerializeField] private Image[] _countdown;
    [Header("スタートの画像"), SerializeField] private Image _startImage;
    [Header("終了の画像"), SerializeField] private Image _finishImage;
    [Header("終了の画像のアニメーション時間"),SerializeField] private float _delay = 4f;
    [Header("サドンデス突入の画像"), SerializeField] private Image _overtimeImage;
    [Header("制限時間"),SerializeField]private int _limit = 100;
    [Header("リザルト"), SerializeField] private Image _result;
    [SerializeField]private TextMeshProUGUI _timerUI;
    Result Result;
    private bool _overtime;

    void Start()
    {
        Result = FindObjectOfType<Result>();
        GameManager.Instance.OnTimerChanged += TimerTextUpdate;
        _result.gameObject.SetActive(false);
        _finishImage.gameObject.SetActive(false);
        _overtimeImage.gameObject.SetActive(false);
        _startImage.gameObject.SetActive(false);
        _overtime = true;
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
        _startImage.gameObject.SetActive(true);
        _startImage.rectTransform.DOAnchorPosX(-1500f, 5f);
        yield return new WaitForSeconds(5f);
        _startImage.gameObject.SetActive(false);
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
            else
            {
                if (_overtime)
                {
                    StartCoroutine(OvertimeUI());
                    _overtime = false;
                }
            }
        }
    }
    /// <summary>
    /// サドンデスを延長するか判断する
    /// </summary>
    /// <returns></returns>
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
    /// サドンデス突入のアニメーション
    /// </summary>
    /// <returns></returns>
    IEnumerator OvertimeUI()
    {
        //終了が右から流れてくる
        _finishImage.gameObject.SetActive(true);
        _finishImage.rectTransform.DOAnchorPosX(0f, _delay);
        yield return new WaitForSeconds(_delay + 3f);
        _finishImage.gameObject.SetActive(false);
        _finishImage.rectTransform.localPosition = new Vector3(1200, 0, 0);//再度流れてくるように位置を戻す

        //ボーナスタイムがでてきて、フェードアウトする
        _overtimeImage.gameObject.SetActive(true);
        _overtimeImage.rectTransform.localScale = new Vector3(10, 10, 1);
        _overtimeImage.rectTransform.DOScale(Vector3.one, 3);
        yield return new WaitForSeconds(5f);
        _overtimeImage.DOFade(0f, 1f);
        yield return new WaitForSeconds(1f);
        _overtimeImage.gameObject.SetActive(false);
    }
    /// <summary>
    /// 終了の表示、アニメーション
    /// </summary>
    /// <returns></returns>
    IEnumerator FinishUI()
    {
        //終了が横から流れてくる
        _finishImage.gameObject.SetActive(true);
        _finishImage.rectTransform.DOAnchorPosX(0f, _delay);
        yield return new WaitForSeconds(_delay + 3f);
        _finishImage.gameObject.SetActive(false);
        _timerUI.gameObject.SetActive(false);

        //リザルト画面の表示
        Result.ResultUI();
    }
}