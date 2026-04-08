using UnityEngine;

public class IceBreakTrigger : ColliderEventTrigger
{
    [SerializeField] private GameObject iceNoHole;
    [SerializeField] private GameObject iceWithHole;

    public override void TriggerEvent()
    {
        base.TriggerEvent();
        
        
        // REPLACE ICE WITH HOLE

        if (AudioManager.Instance != null)
        {
            // PLAY ICE BREAK SOUND EFFECT
            Debug.Log("Playing ice break sound effect.");
        }
    }
}
