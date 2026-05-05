using UnityEngine;

public class Crawl : State
{
    public Transform where;
    public Transform target;

    public float speed = 1f;

    private Vector3 direction;

    Quaternion targetRotation;

    public override void Enter()
    {
        core.transform.position = where.position;
        core.animator.Play(anim.name);
        direction = (target.position - where.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);

        targetRotation = lookRotation * Quaternion.Euler(0, 90, 0);

        
    }

    public override void Do()
    {
        if (Vector3.Distance(core.transform.position, target.position) < 0.1f)
        {
            isComplete = true;
        }
    }

    public override void FixedDo()
    {
        if (!isComplete)
        {
            core.model.transform.rotation = targetRotation;
            core.body.linearVelocity = direction * speed;
        }
            
    }

    public override void Exit()
    {
        core.body.linearVelocity = Vector3.zero;
    }
}
