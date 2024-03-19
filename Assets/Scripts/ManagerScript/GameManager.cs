using System;
using UnityEngine;

//this script should assign to GameManager object
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;

    
    public int PlayerHealth;
    public int PlayerScore;
    public int MaxScore;
    public float GlobalTimer;


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }

    private void Start()
    {
        UpdateGameState(GameState.GameInitialize);
    }
    private void Update()
    {
        if(State == GameState.Playing) GlobalTimer += Time.deltaTime;
        UpdateGameState(DecideState());
    }

    public void UpdateGameState(GameState newState)
    {
        //Guard to prevent duplicated call.
        if(newState == State && newState != GameState.GameInitialize) return;
        State = newState;

        switch (newState)
        {
            case GameState.GameInitialize:
                HandleGameInitialize();
                break;
            case GameState.Playing:
                HandlePlaying();
                break;
            case GameState.Paused:
                HandlePaused();
                break;
            case GameState.Victory:
                HandleVictory();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        //Each newState occurs will trigger event
        OnGameStateChanged?.Invoke(newState);
    }


    private void HandleGameInitialize()
    {
        //Initaialize all data
        Time.timeScale = 1;
        PlayerHealth = PlayerMain.PlayerInitialHealth;
        PlayerScore = PlayerMain.PlayerinitialScore;
        GlobalTimer = 0.0f;
        MaxScore = GameObject.FindGameObjectsWithTag("Item").Length; 
        UpdateGameState(GameState.Playing);
    }
    private void HandlePlaying()
    {
        Time.timeScale = 1;
    }

    private void HandlePaused()
    {
        Time.timeScale = 0;
    }
    
    private GameState DecideState()
    {
        if(Input.GetButtonDown("Cancel")) return State == GameState.Paused? GameState.Playing:GameState.Paused;
        if(PlayerHealth <= 0) return GameState.Lose;
        if(PlayerScore == MaxScore) return GameState.Victory; 
        return State;
    }

    private void HandleLose()
    {
        Time.timeScale = 0;
    }

    private void HandleVictory()
    {
        Time.timeScale = 0;
    }

}
public enum GameState{
    GameInitialize,
    Playing,
    Paused,
    Victory,
    Lose
}
