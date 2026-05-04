using UnityEngine;

public class Navigate : State
{
    public Transform target;
    private float movementSpeed => core.movementSpeed;
    [SerializeField] private float rotationSpeed = 10f;

    private Vector3 direction;
    private GameObject player => UtilityManager.instance.GetPlayer();
    private bool reachedTarget => Vector3.Distance(core.model.position, target.position) < 0.5f;
    private bool hasRotatedTowardsPlayer = false;
    private bool hasRotatedTowardsTarget = false;

    public override void Enter()
    {
        // 1. Calculate direction and immediately strip the Y component
        Vector3 rawDirection = (target.position - core.model.position);
        direction = new Vector3(rawDirection.x, 0, rawDirection.z).normalized;

        hasRotatedTowardsTarget = false;
        hasRotatedTowardsPlayer = false;

        Debug.Log("Entering Navigate state. Heading to: " + target.name);
    }

    public override void FixedDo()
    {
        if (!hasRotatedTowardsTarget)
        {
            RotateTowards(direction, ref hasRotatedTowardsTarget);
        }
        else if (!reachedTarget)
        {
            core.body.linearVelocity = direction * movementSpeed;

        }
        else
        {
            // Face player once arrived
            Vector3 rawDirToPlayer = (player.transform.position - core.model.position);
            Vector3 flatDirToPlayer = new Vector3(rawDirToPlayer.x, 0, rawDirToPlayer.z).normalized;

            
            RotateTowards(flatDirToPlayer, ref hasRotatedTowardsPlayer);
        }
    }

    private void RotateTowards(Vector3 targetDir, ref bool completionFlag)
    {
        if (targetDir == Vector3.zero) return;

        // Explicitly use Vector3.up to lock the rotation to the Y axis
        Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);

        // Slerp towards the target rotation
        core.model.rotation = Quaternion.Slerp(core.model.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        // Check if we are "close enough" to stop rotating and start moving
        if (Quaternion.Angle(core.model.rotation, targetRotation) < 1f)
        {
            // Snap to exact rotation to prevent minor drifting
            core.model.rotation = targetRotation;
            completionFlag = true;
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
