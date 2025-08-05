using UnityEngine;

public class FootStepController : MonoBehaviour
{
    [SerializeField] private SoundKey[] footStepSounds;
    private int _currentSoundIndex = 0;

    public void PlayFootStepSound()
    {
        var soundKey = footStepSounds[_currentSoundIndex];
        SoundManager.Instance.Play(soundKey);
        _currentSoundIndex = (_currentSoundIndex + 1) % footStepSounds.Length;
    }
}
