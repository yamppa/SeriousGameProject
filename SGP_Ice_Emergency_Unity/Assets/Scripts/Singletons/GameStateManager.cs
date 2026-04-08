using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    [SerializeField] private GameState currentGameState;
    [Space]
    [SerializeField] private InGameState currentInGameState;

    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        currentGameState = GameState.Menu;
        currentInGameState = InGameState.None;
    }

    private void Update()
    {
        
    }

    #region utility
    public GameState GetCurrentGameState()
    {
        return currentGameState;
    }
    public InGameState GetCurrentInGameState()
    {
        return currentInGameState;
    }
    #endregion

    #region state management
    private void UpdateGameState(GameState newState)
    {
        currentGameState = newState;
    }

    private void UpdateInGameState(InGameState newState)
    {
        currentInGameState = newState;
    }

    public void StartGame()
    {
        UpdateGameState(GameState.Playing);
        UpdateInGameState(InGameState.Start);
    }

    public void PauseGame()
    {
        UpdateGameState(GameState.Paused);
    }

    public void ResumeGame()
    {
        UpdateGameState(GameState.Playing);
    }

    public void EndGame()
    {
        // VÄHÄN JOTAIN SPESIFIMPÄÄ TÄHÄN , MUTTA NYT VAIN GAMEOVER
        UpdateGameState(GameState.GameOver);
    }

    public void StartFirstDialogue()
    {
        UpdateInGameState(InGameState.FirstDialogueActive);
    }

    public void CompleteFirstDialogue()
    {
        UpdateInGameState(InGameState.FistDialogueCompleted);
    }

    public void IceBreak()
    {
        UpdateInGameState(InGameState.IceBroken);
    }

    public void NpcRescued()
    {
        UpdateInGameState(InGameState.NpcRescued);
    }

    public void StartSecondDialogue()
    {
        UpdateInGameState(InGameState.SecondDialogueActive);
    }

    public void CompleteSecondDialogue()
    {
        UpdateInGameState(InGameState.SecondDialogueCompleted);
    }
    #endregion

}
