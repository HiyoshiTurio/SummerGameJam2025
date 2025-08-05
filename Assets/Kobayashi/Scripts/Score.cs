using System;
using UnityEngine;
using TMPro;
using DG.Tweening;
/// <summary>
/// 得点のUI
/// </summary>
public class Score : MonoBehaviour
{
    private int before = 0;

    [SerializeField] private TextMeshProUGUI[] playerScoreText = new TextMeshProUGUI[2];
 
    void Awake()
    {
        GameManager.Instance.OnScoreChanged += UpdateText;
    }
    void UpdateText(Player player, int after)
    {
        Debug.Log("Update ScoreText");
        var textUI = playerScoreText[(int)player];
        DOTween.To(() => before, value =>
        {
            textUI.text = value.ToString("000000");
        }, after, 1f).SetEase(Ease.OutExpo);
        before = after;
        // var defaultScale = textUI.transform.localScale;
        // textUI.transform.localScale = defaultScale * 1.2f;
        // textUI.transform.DOScale(defaultScale, 0.2f);
    }
}
