using UnityEngine;
using TMPro;
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
    }

    /// <summary>
    /// ���_�̍X�V
    /// </summary>
    void UpdateText(int score)
    {
        _currentscore = score;
        _score.text = _currentscore.ToString("000000");
    }
}
