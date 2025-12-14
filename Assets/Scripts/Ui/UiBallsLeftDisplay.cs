public class UiBallsLeftDisplay : NumberDisplay {
    protected override string BaseText => "Balls left";
    protected override int GetNumber() => GameManager.BallsLeft;
    protected override void Awake(){
        base.Awake();
        PlayerShootingSystem.OnShoot.AddListener(UpdateDisplay);
        GameManager.OnGameStart.AddListener(UpdateDisplay);
    }
    private void OnDestroy(){
        PlayerShootingSystem.OnShoot.RemoveListener(UpdateDisplay);
        GameManager.OnGameStart.RemoveListener(UpdateDisplay);
    }
}