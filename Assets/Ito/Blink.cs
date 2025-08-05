using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    [SerializeField] GameObject _gameObject;
    [Header("“_–Å‚³‚¹‚½‚¢Image"), SerializeField] Image targetImage;
    [SerializeField] float blinkSpeed = 2f;
    public bool isSelected = true;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        _gameObject.transform.position = mousePos;
        if (!isSelected)
        {
            float alpha = (Mathf.Sin(Time.time * blinkSpeed) + 1f) / 2f;
            targetImage.color = new Color(1, 1, 1, alpha);
        }
        else
        {
            targetImage.color = new Color(1, 1, 1, 1);
        }
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
