public class GameOverDisplay : NumberDisplay
{
    protected override string BaseText => "Final Score";
    protected override int GetNumber() => GameManager.Score;
    protected override void Awake()
    {
        base.Awake();
        GameManager.OnGameOver.AddListener(UpdateDisplay);
        GameManager.OnGameOver.AddListener(Show);
        gameObject.SetActive(false);
    }
    void Show()
    {
        gameObject.SetActive(true);
    }
    void OnDestroy()
    {
        GameManager.OnGameOver.RemoveListener(UpdateDisplay);
        GameManager.OnGameOver.RemoveListener(Show);
    }
}
