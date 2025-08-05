using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [Header("1P�̏����摜"),SerializeField] private Image _1PwinImage;
    [Header("2P�̏����摜"), SerializeField] private Image _2PwinImage;

    private void Start()
    {
        _1PwinImage.gameObject.SetActive(false);
        _2PwinImage.gameObject.SetActive(false);
    }
    /// <summary>
    /// ���s�̔��f�A�\��
    /// </summary>
    void ResultUI()
    {
        var playerOneResultScore = GameManager.Instance.GetScore(Player.One);
        var playerTwoResultScore = GameManager.Instance.GetScore(Player.Two);
        if (playerOneResultScore > playerTwoResultScore)
        {
            //1P�̏���
            _1PwinImage.gameObject.SetActive(true);
        }
        else if (playerTwoResultScore > playerOneResultScore)
        {
            //2P�̏���
            _2PwinImage.gameObject.SetActive(true);
        }
    }
}
