using System;
using System.Collections.Generic;
using System.Linq;
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
        public TextMeshProUGUI score;
    }
    
    [SerializeField]
    private ScoreUIData[] scoreUIData;
    private Dictionary<Player, TextMeshProUGUI> _scoreUiDictionary = new();
 
    void Start()
    {
        // 各プレイヤーの得点UIを初期化
        _scoreUiDictionary = scoreUIData.ToDictionary(x => x.player, x => x.score);
        foreach (var data in scoreUIData)
        {
            data.score.text = "000000"; // 初期値を設定
        }
        GameManager.Instance.OnScoreChanged += UpdateText;
    }

    /// <summary>
    /// 得点の更新
    /// </summary>
    void UpdateText(Player player, int before, int after)
    {
        var textUI = _scoreUiDictionary[player];
        DOTween.To(() => before, value =>
        {   
            textUI.text = value.ToString("000000");
        }, after, 1f).SetEase(Ease.OutExpo);
        var defaultScale = textUI.transform.localScale;
        textUI.transform.localScale = defaultScale * 1.2f;
        textUI.transform.DOScale(defaultScale, 0.2f);
    }
}
