using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance => _instance;
    
    [SerializeField] AudioSource bgmSource;
    
    public List<AudioClip> audioClips;
    private AudioSource[] _audioSources = new AudioSource[16];
    public AudioClip bgmAudioClip;
    private AudioSource _bgmAudioSource;
    private AudioSource _drumRollAudioSource;
    private int _playBackCount = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (_instance == null) _instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        for (int i = 0; i < 16; i++)
        {
            _audioSources[i] = gameObject.AddComponent<AudioSource>();
        }

        _bgmAudioSource = gameObject.AddComponent<AudioSource>();
        _drumRollAudioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Play(SoundKey soundKey)
    {
        // _audioSources[_playBackCount].clip = audioClips[(int)soundKey];
        _audioSources[0].PlayOneShot(audioClips[(int)soundKey]);
        _playBackCount = (_playBackCount++) % 16;
    }

    public void PlayBgm()
    {
        // _bgmAudioSource.clip = bgmAudioClip;
        // _bgmAudioSource.loop = true;
        // _bgmAudioSource.Play(); 
    }
    public void StopBgm()
    {
        bgmSource.Stop();
    }

    public void PlayDrumRoll()
    {
        _drumRollAudioSource.clip = audioClips[(int)SoundKey.DrumRoll];
        _drumRollAudioSource.loop = true;
        _drumRollAudioSource.Play();
    }
    public void StopDrumRoll()
    {
        _drumRollAudioSource.clip = audioClips[(int)SoundKey.DrumRollEnd];
        _drumRollAudioSource.loop = false;
        _drumRollAudioSource.Play();
    }
}

public enum SoundKey
{
    ClapHunds,
    DrumRoll,
    DrumRollEnd,
    Okawari,
    PlayKoto,
    PutGruss,
    RestockSoba,
    ScoreDown,
    ScoreUp,
    ShowResult,
    StartCountDown,
    TimeUp,
    Title,
    FootStep1,
    FootStep2,
    Dash
}