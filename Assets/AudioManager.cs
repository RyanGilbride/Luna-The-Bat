using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton instance

    [SerializeField] private AudioSource bgmSource; // AudioSource for BGM
    private AudioClip currentBGM; // Track the currently playing BGM

    private void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this GameObject persistent
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Play BGM (call this method to start or change the BGM)
    public void PlayBGM(AudioClip bgmClip)
    {
        if (bgmSource == null)
        {
            Debug.LogError("BGM AudioSource is not assigned!");
            return;
        }

        // Only change the BGM if the new clip is different or no BGM is playing
        if (bgmClip != currentBGM || !bgmSource.isPlaying)
        {
            bgmSource.clip = bgmClip;
            bgmSource.Play();
            currentBGM = bgmClip; // Update the current BGM track
        }
    }

    // Stop BGM (call this method to stop the BGM)
    public void StopBGM()
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Stop();
            currentBGM = null; // Reset the current BGM track
        }
    }

    // Pause BGM (optional, for pausing/resuming BGM)
    public void PauseBGM()
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Pause();
        }
    }

    // Resume BGM (optional, for pausing/resuming BGM)
    public void ResumeBGM()
    {
        if (bgmSource != null && !bgmSource.isPlaying)
        {
            bgmSource.UnPause();
        }
    }
}