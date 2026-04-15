using UnityEngine;

public class UtilityManager : MonoBehaviour
{
    public static UtilityManager instance;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject npc;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple instances of UtilityManager found! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public LayerMask GetWhatIsGround() => whatIsGround;
    public GameObject GetPlayer() => player;
    public GameObject GetNPC() => npc;

}
