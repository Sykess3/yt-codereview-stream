namespace Source.Models.Balls
{
    public interface IFallingAccelerationLevelConfig
    {
        float TimeToSpeedUp { get; }
        float SpeedIncreaseAmountAfterSpeedUp { get; }
    }
}