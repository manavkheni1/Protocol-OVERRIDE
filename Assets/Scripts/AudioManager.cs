using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Source")]
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip gameStart;
    public AudioClip gameOver;
    public AudioClip playerShoot;
    public AudioClip enemyShoot;
    public AudioClip enemyDead;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Instantly play the startup sound when the arena loads!
        if (gameStart != null)
        {
            PlaySound(gameStart);
        }
    }

    // Other scripts will call this function to play a specific sound
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            // PlayOneShot allows multiple sounds to overlap without cutting each other off!
            sfxSource.PlayOneShot(clip); 
        }
    }
}