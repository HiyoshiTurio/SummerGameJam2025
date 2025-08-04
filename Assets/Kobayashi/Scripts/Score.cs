using UnityEngine;
using TMPro;
/// <summary>
/// 得点のUI
/// </summary>
public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    private int _currentscore = 0;
 
    void Start()
    {
        _score.text = _currentscore.ToString("000000");
    }

    /// <summary>
    /// 得点の更新
    /// </summary>
    void UpdateText(int score)
    {
        _currentscore = score;
        _score.text = _currentscore.ToString("000000");
    }
}
