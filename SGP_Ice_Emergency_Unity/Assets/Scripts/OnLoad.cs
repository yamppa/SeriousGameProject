using UnityEngine;

public class OnLoad : MonoBehaviour
{
    [SerializeField] private bool dontDestroy;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject npc;

    private void Awake()
    {
        if (dontDestroy)
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {
        if (player != null)
        {
            UtilityManager.instance.SetPlayer(player);
        }
        else
        {
            Debug.LogWarning("Player reference is not set in OnLoad script.");
        }
        if (npc != null)
        {
            UtilityManager.instance.SetNPC(npc);
        }
        else
        {
            Debug.LogWarning("NPC reference is not set in OnLoad script.");
        }
    }
}
