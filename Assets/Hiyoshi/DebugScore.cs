using UnityEngine;

public class DebugScore : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameManager.Instance.AddScore(5,Player.One);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameManager.Instance.AddScore(5,Player.Two);
        }
    }
}
