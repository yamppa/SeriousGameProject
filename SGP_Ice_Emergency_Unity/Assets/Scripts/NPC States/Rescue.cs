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
        core.body.useGravity = false;
        core.body.linearVelocity = Vector3.zero;
        core.transform.position = hole.position;
        rescueCollider.enabled = true;

        Debug.Log("Entering Rescue state. Teleporting to hole");
        flag = false;
        timer = 0f;
        //animaiton

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




}
