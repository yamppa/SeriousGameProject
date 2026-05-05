using UnityEngine;

public class Idle : State
{

    public override void Enter()
    {
        core.animator.Play(anim.name);
    }
}
