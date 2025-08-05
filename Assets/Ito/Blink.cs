using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    [SerializeField] GameObject _gameObject;
    [Header("“_–Å‚³‚¹‚½‚¢Image"), SerializeField] Image targetImage;
    [SerializeField] float blinkSpeed = 1f;
    public bool isSelected = true;
    private float alpha;
    private float timer = Mathf.PI/2;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        _gameObject.transform.position = mousePos;   
        if (!isSelected)
        {
            alpha = (Mathf.Sin(timer * blinkSpeed) + 1f) / 2f;
            targetImage.color = new Color(1, 1, 1, alpha);
        }
        else
        {
            targetImage.color = new Color(1, 1, 1, 1);
            timer = Mathf.PI/2;
        }
        timer += 0.01f;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        isSelected = true;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        isSelected = false;
    }
}
