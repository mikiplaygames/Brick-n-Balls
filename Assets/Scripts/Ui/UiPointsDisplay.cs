public class UiPointsDisplay : NumberDisplay
{
    protected override string BaseText => "Points";
    protected override int GetNumber() => GameManager.Score;
    protected override void Awake(){
        base.Awake();
        BrickCollisionSystem.OnBrickHit.AddListener(UpdateDisplay);
    }
    void OnDestroy()
    {
        BrickCollisionSystem.OnBrickHit.RemoveListener(UpdateDisplay);
    }
}