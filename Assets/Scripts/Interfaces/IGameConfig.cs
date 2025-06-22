namespace Interfaces
{
    public interface IGameConfig
    {
        float LaneWidth { get; }
        float LaneHeight { get; }
        float TopBoundary { get; }
        float BottomBoundary { get; }
        float LeftBoundary { get; }
        float RightBoundary { get; }
        int MaxLives { get; }
        float InputThreshold { get; }
    }
}