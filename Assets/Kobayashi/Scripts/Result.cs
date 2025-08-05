using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [Header("1P‚ÌŸ—˜‰æ‘œ"),SerializeField] private Image _1PwinImage;
    [Header("2P‚ÌŸ—˜‰æ‘œ"), SerializeField] private Image _2PwinImage;

    private void Start()
    {
        _1PwinImage.gameObject.SetActive(false);
        _2PwinImage.gameObject.SetActive(false);
    }
    /// <summary>
    /// Ÿ”s‚Ì”»’fA•\¦
    /// </summary>
    void ResultUI()
    {
        var playerOneResultScore = GameManager.Instance.GetScore(Player.One);
        var playerTwoResultScore = GameManager.Instance.GetScore(Player.Two);
        if (playerOneResultScore > playerTwoResultScore)
        {
            //1P‚ÌŸ‚¿
            _1PwinImage.gameObject.SetActive(true);
        }
        else if (playerTwoResultScore > playerOneResultScore)
        {
            //2P‚ÌŸ‚¿
            _2PwinImage.gameObject.SetActive(true);
        }
    }
}
