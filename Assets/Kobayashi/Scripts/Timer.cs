using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using DG.Tweening;

public class Timer : MonoBehaviour
{
    [Header("�J�E���g�_�E���摜"), SerializeField] private Image[] _countdown;
    [Header("�I���̉摜"), SerializeField] private Image _finishImage;
    [Header("�I���̉摜�̃A�j���[�V��������"),SerializeField] private float _delay = 4f;
    [Header("��������"),SerializeField]private int _limit = 100;
    [Header("���U���g"), SerializeField] private Image _result;
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
    /// �J�E���g�_�E��
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
    /// �^�C�}�[�̍X�V
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
    /// �I���̕\���A�A�j���[�V����
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
