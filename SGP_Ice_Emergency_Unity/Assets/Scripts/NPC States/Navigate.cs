using UnityEngine;

public class Navigate : State
{
    public Transform target;
    private float movementSpeed => core.movementSpeed;
    [SerializeField] private float rotationSpeed = 3f;

    private Vector3 direction;
    private GameObject player => UtilityManager.instance.GetPlayer();
    private bool reachedTarget => Vector3.Distance(transform.position, target.position) < 0.5f;
    private bool hasRotatedTowardsPlayer = false;
    private bool hasRotatedTowardsTarget = false;

    public override void Enter()
    {
        direction = (target.position - transform.position).normalized;
        hasRotatedTowardsPlayer = false;
        hasRotatedTowardsTarget = false;    

        Debug.Log("Entering Navigate state. Moving towards target: " + target.name);
    }

    public override void Do()
    {
        if (hasRotatedTowardsPlayer)
        {
            core.ShowDialogue();
            isComplete = true;  
        }
    }

    public override void FixedDo()
    {
        if (!hasRotatedTowardsTarget)
        {
            RotateTowardsTarget();
        }
        else if (!reachedTarget)
        {
            core.body.linearVelocity = direction * movementSpeed * Time.deltaTime;
        }
        else
        {
            core.body.linearVelocity = Vector3.zero;
            RotateTowardsPlayer();
        }

    }

    private void RotateTowardsTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (Quaternion.Angle(transform.rotation, targetRotation) < 5f)
        {
            hasRotatedTowardsTarget = true;
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (Quaternion.Angle(transform.rotation, targetRotation) < 5f)
        {
            hasRotatedTowardsPlayer = true;
        }
    }
}
