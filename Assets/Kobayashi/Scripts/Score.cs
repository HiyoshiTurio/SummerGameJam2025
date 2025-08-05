using System;
using UnityEngine;
using TMPro;
using DG.Tweening;
/// <summary>
/// 得点のUI
/// </summary>
public class Score : MonoBehaviour
{
    [Serializable]
    public struct ScoreUIData
    {
        public Player player;
        public TextMeshProUGUI scoreText;
    }
    
    [SerializeField]
    private ScoreUIData[] scoreUIData;

    [SerializeField] private TextMeshProUGUI[] playerScoreText = new TextMeshProUGUI[2];
 
    void Start()
    {
        GameManager.Instance.OnScoreChanged += UpdateText;
    }
    void UpdateText(Player player, int after)
    {
        var textUI = playerScoreText[(int)player];
        int before = int.Parse(textUI.text);
        DOTween.To(() => before, value =>
        {   
            textUI.text = value.ToString("000000");
        }, after, 1f).SetEase(Ease.OutExpo);
        var defaultScale = textUI.transform.localScale;
        textUI.transform.localScale = defaultScale * 1.2f;
        textUI.transform.DOScale(defaultScale, 0.2f);
    }
}
