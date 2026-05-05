using UnityEngine;

public class Aftercare : State
{
    public Transform recoverySpot;
    public Transform aftercareSpot;
    public Navigate navigate;
    public Idle idle;

    public override void Enter()
    {
        core.transform.position = recoverySpot.position;
        core.body.useGravity = true;

        navigate.target = aftercareSpot;
        Set(navigate);
    }

    override public void Do()
    {
        if (navigate.isComplete)
        {
            Set(idle);
        }
    }
}
