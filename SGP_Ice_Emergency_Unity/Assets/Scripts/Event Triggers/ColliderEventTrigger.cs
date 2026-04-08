using UnityEngine;


[RequireComponent(typeof(Collider))]
public class ColliderEventTrigger : MonoBehaviour, IEventTrigger
{
    public virtual void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        TriggerEvent();
    }

    public virtual void TriggerEvent()
    {
        Debug.Log("Collider event triggered!");
    }
}
