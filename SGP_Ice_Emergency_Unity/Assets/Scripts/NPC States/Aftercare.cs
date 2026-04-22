using UnityEngine;

public class Aftercare : State
{
    public Transform aftercareSpot;

    public override void Enter()
    {
        core.transform.position = aftercareSpot.position;
        core.StartAftercareDialogue();
    }
}
