using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Manages the overall game state, including starting and ending the game
/// </summary>
public class GameManager : MonoBehaviour {
    public static UnityEvent OnGameStart = new();
    public static UnityEvent OnGameOver = new();
    public static int Score = 0;
    public static int BallsLeft = 0;
    public static bool ShouldSpawnBalls = false;
    const int InitialBalls = 50;
    public static bool GameInProgress { get; private set; }
    /// <summary>
    /// Starts the game by initializing default values and locking cursor
    /// </summary>
    public void StartGame()
    {
        if (GameInProgress)
            return;
        Score = 0;
        BallsLeft = InitialBalls;
        GameInProgress = true;
        ShouldSpawnBalls = true;
        OnGameStart.Invoke();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    /// <summary>
    /// Ends the game by unlocking cursor and invoking game over event
    /// </summary>
    public static void EndGame()
    {
        if (!GameInProgress)
            return;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameInProgress = false;
        OnGameOver.Invoke();
    }
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}