using UnityEngine;

public class Navigate : State
{
    public Transform target;
    private float movementSpeed => core.movementSpeed;
    [SerializeField] private float rotationSpeed = 10f;

    private Vector3 direction;
    private GameObject player => UtilityManager.instance.GetPlayer();
    private bool reachedTarget => Vector3.Distance(transform.position, target.position) < 0.5f;
    private bool hasRotatedTowardsPlayer = false;
    private bool hasRotatedTowardsTarget = false;

    public override void Enter()
    {
        
        Vector3 rawDirection = (target.position - transform.position);
        direction = new Vector3(rawDirection.x, 0, rawDirection.z).normalized;

        hasRotatedTowardsPlayer = false;
        hasRotatedTowardsTarget = false;

        Debug.Log("Entering Navigate state. Heading to: " + target.name);
    }

    

    public override void FixedDo()
    {
        if (!hasRotatedTowardsTarget)
        {
            RotateTowardsTarget();
        }
        else if (!reachedTarget)
        {
            
            core.body.linearVelocity = direction * movementSpeed;
        }
        else
        {
            core.body.linearVelocity = Vector3.zero;
            RotateTowardsPlayer();
        }
    }

    private void RotateTowardsTarget()
    {
        if (direction == Vector3.zero) return; 

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 2f)
        {
            hasRotatedTowardsTarget = true;
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 rawDirToPlayer = (player.transform.position - transform.position);
        // Flatten the direction to player
        Vector3 flatDirToPlayer = new Vector3(rawDirToPlayer.x, 0, rawDirToPlayer.z).normalized;

        if (flatDirToPlayer == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(flatDirToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 2f)
        {
            hasRotatedTowardsPlayer = true;
        }
    }


    public override void Do()
    {
        if (hasRotatedTowardsPlayer)
        {
            core.ShowDialogue();
            isComplete = true;  
        }
    }

   
    
}
