using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using DG.Tweening;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private string SceneName = "Test";
    [SerializeField] Button _button;
    [SerializeField] Image _image;
    [Header("フェードスピード"), SerializeField] float fadespeed = 1f;
    public bool Fadeset = false;
    void Start()
    {
        _image.gameObject.SetActive(false);
        _button.onClick.AddListener(a);
    }
    void a()
    {
        Fade(0, 1);
    }

    private void Fade(int startAlpha, int endAlpha)
    {
        _image.gameObject.SetActive(true);
        var c = _image.color;
        c.a = startAlpha;
        _image.color = c;
        _image.DOFade(endAlpha, fadespeed).OnComplete(() => SceneManager.LoadScene(SceneName));
    }
}
