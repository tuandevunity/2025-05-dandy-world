
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("SOUND")]
    [SerializeField] AudioClip footSound;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip groundHitSound;

    [Header("SOURCE")]
    [SerializeField] AudioSource audioBehaviourSource;
    [SerializeField] AudioSource AudioBGSource;

    AudioClip _currentSound;

    private void Start() {
        AudioBGSource.loop = true;
        AudioBGSource.Play();
    }

    public void SetBackgroundMute(bool isMute) {
        AudioBGSource.mute = isMute;
    }

    public void PlayPlayerAudio(PlayerSound sound) {
        switch (sound)
        {
            case PlayerSound.Jump: {
                audioBehaviourSource.loop = false;
                _currentSound = jumpSound;
                PlaySound();
                break;
            }

            case PlayerSound.GroudHit: {
                audioBehaviourSource.loop = false;
                _currentSound = groundHitSound;
                PlaySound();
                break;
            }

            case PlayerSound.FootStep: { // loopp
                if (_currentSound != footSound) {
                    audioBehaviourSource.loop = true;
                    _currentSound = footSound;
                    PlaySound();
                }
                break;
            }

            case PlayerSound.Idle: {
                if (_currentSound != groundHitSound) {
                    _currentSound = null;
                    audioBehaviourSource.Stop();
                }
                break;
            }
        }
    }

    private void PlaySound() {
        audioBehaviourSource.Stop();
        audioBehaviourSource.PlayOneShot(_currentSound);
    }

}

public enum PlayerSound {
    Idle, // stop sound
    Jump,
    FootStep,
    GroudHit

}
