using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class NPCManager : MonoBehaviour
{
    [Header("Components")]
    public GameObject[] dialogueCanvas;
    public GameObject[] aftercareCanvas;
    public GameObject scoreCanvas;
    public TMP_Text correctAnswersText;
    public TMP_Text scoreText;
    public Rigidbody body;
    public Collider bodyCollider;
    public Animator animator;
    public StateMachine machine;
    public Transform model;

    [Header("Language")]
    public Language language;

    [Header("Stats")]
    public float movementSpeed = 5f;

    [Header("States")]
    public Idle idle;
    public Navigate navigate;
    public Rescue rescue;
    public Crawl crawl;
    public Aftercare aftercare;

    private int currentDialogueIndex = 0;
    private int currentAftercareIndex = 0;

    private State state => machine.state;

    public List<Transform> spots = new List<Transform>();

    private int correctAnswers = 0;



    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        // Animator.
        currentDialogueIndex = 0;
        currentAftercareIndex = 0;
        correctAnswers = 0;
    }

    private void Start()
    {
        SetupInstances();
        dialogueCanvas[currentDialogueIndex].SetActive(false);
        aftercareCanvas[currentAftercareIndex].SetActive(false);
        scoreCanvas.SetActive(false);
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
                Set(crawl, true);
            }
            else if (state == crawl)
            {
                Set(aftercare, true);
            }
        }

        state.DoAll();

        if (body.linearVelocity.magnitude > 0.1f)
        {
            AudioManager.Instance.PlayNPCFootsteps(0.5f);
        }
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

    public void CorrectAnswer()
    {
        ScoreManager.Instance.AddPoint();
        correctAnswers++;
    }

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void ScoreBoard()
    {
        CloseAftercare();
        CalculateScore();
        scoreCanvas.SetActive(true);
    }

    private void CalculateScore()
    {
        float totalScore = ScoreManager.Instance.GetTotalScore();

        if (language == Language.English)
        {
            scoreText.text = $"Final Score: {totalScore:F2}";
            correctAnswersText.text = $"Correct Answers: {correctAnswers}";
        }
        else if (language == Language.Suomi)
        {
            scoreText.text = $"Pisteet: {totalScore:F2}";
            correctAnswersText.text = $"Oikeat Vastaukset: {correctAnswers}";
        }
        
    }

    public void ResetGame()
    {
        Debug.Log("Resetting game...");
        ScoreManager.Instance.Reset();
        currentDialogueIndex = 0;
        currentAftercareIndex = 0;
        correctAnswers = 0;
        scoreCanvas.SetActive(false);
        SceneManager.Instance.RestartScene();
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
