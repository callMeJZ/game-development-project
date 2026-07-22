namespace Platformer.Model
{
    /// <summary>
    /// Stores score for the current run while Unity loads the next level scene.
    /// This intentionally resets when the first build scene is entered.
    /// </summary>
    public static class RunState
    {
        public static int Score { get; private set; }

        public static void StartNewRun()
        {
            Score = 0;
        }

        public static void SetScore(int score)
        {
            Score = score < 0 ? 0 : score;
        }
    }
}
