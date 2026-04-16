using UnityEngine;


[RequireComponent(typeof(Collider))]
public class ColliderEventTrigger : MonoBehaviour, IEventTrigger
{
    protected virtual void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        // EHK VIEL PLAYER CHECKKI, on child classeis
        TriggerEvent();
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        // PÖÖ
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        // PÄÄ
    }

    public virtual void TriggerEvent()
    {
        Debug.Log("Collider event triggered!");
    }
}
