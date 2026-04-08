using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : ColliderEventTrigger
{
    [SerializeField] private GameObject dialogueBox;
    [Space]
    [SerializeField] private UnityEvent onStartDialogue;

    protected override void Start()
    {
        base.Start();
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (dialogueBox != null)
            {
                dialogueBox.SetActive(false);
            }
        }
    }

    public override void TriggerEvent()
    {
        base.TriggerEvent();
        
        Debug.Log("Dialogue triggered!");
        // VOIS LAITTAA VAAN UNITY EVENTTIIN TÄMÄNKIN
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(true);
        }
        // TÄHÄN VAAN KYTKEE MITÄ IKINÄ
        onStartDialogue?.Invoke();
    }

    
}
