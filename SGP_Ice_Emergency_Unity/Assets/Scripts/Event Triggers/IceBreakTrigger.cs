using UnityEngine;
using UnityEngine.Events;

public class IceBreakTrigger : ColliderEventTrigger
{
    // ONKO JOKU TEXTURE VAI MESHI, ENTIÄ NYT GO, VAIHTOON KU TIETÄÄ
    [SerializeField] private GameObject iceNoHole;
    [SerializeField] private GameObject iceWithHole;
    [Space]
    [SerializeField] private UnityEvent onIceBreak; // EN USKO ET TARVII NYT TEHÄ MITÄÄN CUSTOM EVENTTEJÄ JOTEN UNITYN DEFAULT RIITTÄÄ
                                                    // VÄHÄN RISTIRIIDASSA KUN AUDIO KUTSUTAAN NYT TOLLEEN TOIMIS TOSSA INVOKESSA KANS

    private bool oneShot = true;
    protected override void Start()
    {
        base.Start();
        oneShot = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        NPCManager npc = other.GetComponent<NPCManager>();

        if (npc != null)
        {
            TriggerEvent();
            npc.FallInIce();
        }
    }
    public override void TriggerEvent()
    {
        if (!oneShot) return;

        base.TriggerEvent();

        // REPLACE ICE WITH HOLE

        // EHKÄ ANIMAATIO TAI PARTICLE EFFECT JOTAIN

        // AUDIO

        // GAMESTATE UPDATE
        
        if (iceNoHole != null && iceWithHole != null)
        {
            iceNoHole.SetActive(false);
            iceWithHole.SetActive(true);
        }

        onIceBreak?.Invoke();
        oneShot = false;
    }
}
