using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public float score { get; private set; } = 0f;

    private float rescueTimer = 0f;

    private bool rescueTimerActive = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        score = 0f;
        rescueTimer = 0f;
    }

    private void Update()
    {
        if (rescueTimerActive)
        {
            rescueTimer += Time.deltaTime;
        }
    }

    public float GetTotalScore()
    {
        return score / (rescueTimer / 100);
    }


    public void AddPoint()
    {
        score += 1f;
    }

    public void StartRescueTimer()
    {
        rescueTimerActive = true;
    }

    public void StopRescueTimer()
    {
        rescueTimerActive = false;
    }
}
