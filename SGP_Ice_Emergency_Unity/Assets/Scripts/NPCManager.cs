using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueCanvas;
    public Rigidbody body;
    public Animator animator;
    public StateMachine machine;



    [Header("Stats")]
    public float movementSpeed = 5f;

    [Header("States")]
    public Idle idle;
    public Navigate navigate;

    private State state => machine.state;

    public List<Transform> spots = new List<Transform>();



    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        // Animator.
    }

    private void Start()
    {
        SetupInstances();
        CloseDialogue();
        Set(idle);
    }

    private void Update()
    {
        if (state.isComplete)
        {
            if (state == navigate)
            {
                Set(idle);
            }
        }

        state.DoAll();
    }

    private void FixedUpdate()
    {
        state.FixedDoAll();
    }

    public void GoToNextSpot()
    {
        CloseDialogue();
        if (spots.Count > 0)
        {
            navigate.target = spots[0];
            Set(navigate);
            spots.RemoveAt(0);
        }
    }

    #region Dialogue
    public void ShowDialogue()
    {
        dialogueCanvas.SetActive(true);
    }
    public void CloseDialogue()
    {
        dialogueCanvas.SetActive(false);
    }
    #endregion

    protected void Set(State newState, bool forceReset = false)
    {
        machine.Set(newState, forceReset);
    }

    public virtual void SetupInstances()
    {
        machine = new StateMachine();

        State[] allChildStates = GetComponentsInChildren<State>();
        foreach (State state in allChildStates)
        { state.SetCore(this); }
    }
}
