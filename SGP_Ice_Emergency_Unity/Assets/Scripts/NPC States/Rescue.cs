using UnityEngine;

public class Rescue : State
{
    public Transform hole;
    public Collider rescueCollider;

    [SerializeField] private float rescueDuration = 3f;

    private bool flag = false;
    private float timer = 0f;

    private void Awake()
    {
        rescueCollider.isTrigger = true;
        rescueCollider.enabled = false;
    }

    public override void Enter()
    {
        AudioManager.Instance.ToggleDramatic(true);

        core.body.useGravity = false;
        core.body.linearVelocity = Vector3.zero;
        core.transform.position = hole.position;
        rescueCollider.enabled = true;

        Debug.Log("Entering Rescue state. Teleporting to hole");
        flag = false;
        timer = 0f;
        //animaiton
        core.animator.Play(anim.name);
        core.bodyCollider.enabled = false;

        ScoreManager.Instance.StartRescueTimer();
    }

    public override void Do()
    {
        
        if (flag)
        {
            timer += Time.deltaTime;
            
            if (timer >= rescueDuration)
            {
                isComplete = true;
            }
        }
        else
        {
            timer = 0f;
        }
    }

    public override void FixedDo()
    {
       RotateTowards(UtilityManager.instance.GetPlayer().transform.position - core.model.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stick"))
        {
            flag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stick"))
        {
            flag = false;
        }
    }

    public override void Exit()
    {
        rescueCollider.enabled = false;
        ScoreManager.Instance.StopRescueTimer();
    }


    private void RotateTowards(Vector3 targetDir)
    {
        if (targetDir == Vector3.zero) return;

        // Explicitly use Vector3.up to lock the rotation to the Y axis
        Quaternion lookRotation = Quaternion.LookRotation(targetDir, Vector3.up);

        Quaternion targetRotation = lookRotation * Quaternion.Euler(0, 90, 0);

        // Slerp towards the target rotation
        core.model.rotation = Quaternion.Slerp(core.model.rotation, targetRotation, 5 * Time.fixedDeltaTime);

        // Check if we are "close enough" to stop rotating and start moving
        if (Quaternion.Angle(core.model.rotation, targetRotation) < 1f)
        {
            // Snap to exact rotation to prevent minor drifting
            core.model.rotation = targetRotation;
            
        }
    }




}
