using UnityEngine;
using TMPro;
using DG.Tweening;
/// <summary>
/// 得点のUI
/// </summary>
public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    private int _currentScore = 0;
 
    void Start()
    {
        _score.text = _currentScore.ToString("000000");
    }

    /// <summary>
    /// 得点の更新
    /// </summary>
    void UpdateText(int score)
    {
        var beforeScore = _currentScore;
        var afterScore = _currentScore + score;
        DOTween.To(() => beforeScore, value =>
        {
            _score.text = value.ToString("000000");
        }, afterScore, 1f).SetEase(Ease.OutExpo);
        var defaultScale = _score.transform.localScale;
        _score.transform.localScale = defaultScale * 1.2f;
        _score.transform.DOScale(defaultScale, 0.2f);
    }

    public void AddScore(int score)
    {
        UpdateText(score);
        _currentScore += score;
        Debug.Log(_currentScore);
    }
}
