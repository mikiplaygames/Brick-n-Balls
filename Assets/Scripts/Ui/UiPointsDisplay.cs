public class UiPointsDisplay : NumberDisplay
{
    protected override string BaseText => "Points";
    protected override int GetNumber() => GameManager.Score;
    protected override void Awake(){
        BrickCollisionSystem.OnBrickHit.AddListener(UpdateDisplay);
    }
}