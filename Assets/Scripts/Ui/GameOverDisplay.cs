public class GameOverDisplay : NumberDisplay
{
    protected override string BaseText => "Final Score";
    protected override int GetNumber() => GameManager.Score;
    protected override void Awake()
    {
        base.Awake();
        GameManager.OnGameOver.AddListener(UpdateDisplay);
        GameManager.OnGameOver.AddListener(() => gameObject.SetActive(true));
        gameObject.SetActive(false);
    }
}
