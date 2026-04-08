using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Data")]
    [SerializeField] private AudioDataSO audioData;
    [Header("Music & Ambiance")]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource ambianceAudioSource;
    [Header("SFX")]
    [Header("Player SFX")]
    [SerializeField] private AudioSource playerFootsteps;
    [SerializeField] private AudioSource playerSFXAudioSource;
    [Header("NPC SFX")]
    [SerializeField] private AudioSource npcFootsteps;
    [SerializeField] private AudioSource npcSFXAudioSource;
    [Header("Environment SFX")]
    [SerializeField] private AudioSource environmentSFXAudioSource;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (musicAudioSource != null)
        {
            PlayMusic(0.5f);
        }
        if (ambianceAudioSource != null)
        {
            PlayAmbiance(0.5f);
        }
    }



    #region music & ambiance methods

    public void PlayMusic(float volume)
    {
        if (audioData.musicArray.Length > 0)
        {
            AudioClip clip = audioData.musicArray[Random.Range(0, audioData.musicArray.Length)];
            musicAudioSource.clip = clip;
            musicAudioSource.volume = volume;
            musicAudioSource.Play();
        }
    }

    public void PlayAmbiance(float volume)
    {
        if (audioData.ambianceArray.Length > 0)
        {
            AudioClip clip = audioData.ambianceArray[Random.Range(0, audioData.ambianceArray.Length)];
            ambianceAudioSource.clip = clip;
            ambianceAudioSource.volume = volume;
            ambianceAudioSource.Play();
        }
    }

    public void ChangeMusicVolume(float volume)
    {
        musicAudioSource.volume = volume;
    }

    public void ChangeAmbianceVolume(float volume)
    {
        ambianceAudioSource.volume = volume;
    }

    public void StopAmbiance()
    {
        ambianceAudioSource.Stop();
    }

    public void StopMusic()
    {
        musicAudioSource.Stop();
    }

    #endregion

    #region sfx methods

    public void PlayPlayerFootsteps(float volume)
    {
        if (playerFootsteps.isPlaying) return; 

        if (audioData.footstepsArray.Length > 0)
        {
            AudioClip clip = audioData.footstepsArray[Random.Range(0, audioData.footstepsArray.Length)];
            PlayerFootsteps(clip, volume, true, false);
        }
    }

    public void PlayNPCFootsteps(float volume)
    {
        if (playerFootsteps.isPlaying) return;

        if (audioData.footstepsArray.Length > 0)
        {
            AudioClip clip = audioData.footstepsArray[Random.Range(0, audioData.footstepsArray.Length)];
            NPCFootsteps(clip, volume, true, false);
        }
    }

    private void PlayerFootsteps(AudioClip clip, float volume, bool randomPitch, bool loop)
    {
        playerFootsteps.pitch = randomPitch ? Random.Range(0.8f, 1.2f) : 1f;
        playerFootsteps.volume = volume;
        if (loop)
        {
            playerFootsteps.loop = true;
            playerFootsteps.clip = clip;
            playerFootsteps.Play();
        }
        else
        {
            playerFootsteps.PlayOneShot(clip);
        }
    }

    private void NPCFootsteps(AudioClip clip, float volume, bool randomPitch, bool loop)
    {
        npcFootsteps.pitch = randomPitch ? Random.Range(0.8f, 1.2f) : 1f;
        npcFootsteps.volume = volume;
        if (loop)
        {
            npcFootsteps.loop = true;
            npcFootsteps.clip = clip;
            npcFootsteps.Play();
        }
        else
        {
            npcFootsteps.PlayOneShot(clip);
        }
    }

    private void PlayPlayerSFX(AudioClip clip, float volume, bool randomPitch)
    {
        playerSFXAudioSource.pitch = randomPitch ? Random.Range(0.8f, 1.2f) : 1f;
        playerSFXAudioSource.volume = volume;
        playerSFXAudioSource.PlayOneShot(clip);
    }

    private void PlayNPCSFX(AudioClip clip, float volume, bool randomPitch)
    {
        npcSFXAudioSource.pitch = randomPitch ? Random.Range(0.8f, 1.2f) : 1f;
        npcSFXAudioSource.volume = volume;
        npcSFXAudioSource.PlayOneShot(clip);
    }

    private void PlayEnvironmentSFX(AudioClip clip, float volume, bool randomPitch, bool loop)
    {
        environmentSFXAudioSource.pitch = randomPitch ? Random.Range(0.8f, 1.2f) : 1f;
        environmentSFXAudioSource.volume = volume;
        if (loop)
        {
            environmentSFXAudioSource.loop = true;
            environmentSFXAudioSource.clip = clip;
            environmentSFXAudioSource.Play();
        }
        else
        {
            environmentSFXAudioSource.PlayOneShot(clip);
        }
    }

    #endregion
}
