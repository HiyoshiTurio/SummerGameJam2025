using UnityEngine;
using TMPro;
/// <summary>
/// ���_��UI
/// </summary>
public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [Header("���_�̑�����"), SerializeField] private int _plusscore;
    private int _currentscore = 0;
    Timer _timer;
 
    // Start is called before the first frame update
    void Start()
    {
        _timer = FindObjectOfType<Timer>();
        _score.text = _currentscore.ToString("000000");
    }
    /// <summary>
    /// ���_��������
    /// </summary>
    public void IncreaseScore()
    {
        _currentscore += _plusscore;
        _score.text = _currentscore.ToString("000000");
    }
    // Update is called once per frame
    void Update()
    {
        if (!_timer.finish)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                IncreaseScore();
            }
        }  
    }
}
