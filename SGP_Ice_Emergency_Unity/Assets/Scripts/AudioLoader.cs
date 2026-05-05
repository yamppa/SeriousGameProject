using UnityEngine;

public class AudioLoader : MonoBehaviour
{
    //7 fukin mint
    [SerializeField] private AudioSource playerFootsteps;
    [SerializeField] private AudioSource NPCFootsteps;
    [SerializeField] private AudioSource EnviroSource;

    private void Awake()
    {
        

    }

    private void Start()
    {
        if (playerFootsteps != null)
        {
            AudioManager.Instance.SetPlayerFootSource(playerFootsteps);
        }
        if (NPCFootsteps != null)
        {
            AudioManager.Instance.SetNPCFootSource(NPCFootsteps);
        }
        if (EnviroSource != null)
        {
            AudioManager.Instance.SetEnviroSFXSource(EnviroSource);
        }
    }
}
