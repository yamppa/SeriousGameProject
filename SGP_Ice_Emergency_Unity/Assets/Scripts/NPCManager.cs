using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour
{
    [Header("Components")]
    public GameObject[] dialogueCanvas;
    public GameObject[] aftercareCanvas;
    public Rigidbody body;
    public Animator animator;
    public StateMachine machine;
    public Transform model;



    [Header("Stats")]
    public float movementSpeed = 5f;

    [Header("States")]
    public Idle idle;
    public Navigate navigate;
    public Rescue rescue;
    public Aftercare aftercare;

    private int currentDialogueIndex = 0;
    private int currentAftercareIndex = 0;

    private State state => machine.state;

    public List<Transform> spots = new List<Transform>();



    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        // Animator.
        currentDialogueIndex = 0;
        currentAftercareIndex = 0;
    }

    private void Start()
    {
        SetupInstances();
        dialogueCanvas[currentDialogueIndex].SetActive(false);
        aftercareCanvas[currentAftercareIndex].SetActive(false);
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
            else if (state == rescue)
            {
                Set(aftercare,true);
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

    #region Dialogue / Aftercare
    public void ShowDialogue()
    {
        if (state == aftercare)
        {
            StartAftercareDialogue();
            return;
        }

        dialogueCanvas[currentDialogueIndex].SetActive(true);
    }
    public void CloseDialogue()
    {
        dialogueCanvas[currentDialogueIndex].SetActive(false);
        currentDialogueIndex++;
    }

    public void ShowAftercare()
    {
        aftercareCanvas[currentAftercareIndex].SetActive(true);
    }

    public void CloseAftercare()
    {
        aftercareCanvas[currentAftercareIndex].SetActive(false);
        currentAftercareIndex++;
    }

    public void NextAftercare()
    {
        CloseAftercare();
        ShowAftercare();
    }
    #endregion


    public void FallInIce()
    {
        Set(rescue,true);
    }

    public void StartAftercareDialogue()
    {
        aftercareCanvas[currentAftercareIndex].SetActive(true);
    }

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
