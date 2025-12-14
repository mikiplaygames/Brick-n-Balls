public class UiBallsLeftDisplay : NumberDisplay {
    protected override string BaseText => "Balls left";
    protected override int GetNumber() => GameManager.BallsLeft;
    protected override void Awake(){
        PlayerShootingSystem.OnShoot.AddListener(UpdateDisplay);
        GameManager.OnGameStart.AddListener(UpdateDisplay);
    }
}