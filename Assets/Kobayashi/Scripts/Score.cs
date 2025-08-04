using UnityEngine;
using TMPro;
using DG.Tweening;
/// <summary>
/// ���_��UI
/// </summary>
public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    private int _currentscore = 0;
 
    void Start()
    {
        _score.text = _currentscore.ToString("000000");
        GameManager manager = GameManager.Instance;
        manager.OnScoreChanged += UpdateText;
    }

    /// <summary>
    /// ���_�̍X�V
    /// </summary>
    void UpdateText(int score)
    {
        DOTween.To(() => _currentscore, value =>
        {
            _currentscore = value;
            _score.text = value.ToString("000000");
        }, _currentscore + score, 1f);
    }
}
