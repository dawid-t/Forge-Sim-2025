namespace Critsoft.ForgeSim2025
{
    public static class GameConfig
    {
        #region Zenject Memory Pool

        public const string FallingResourcesMemoryPoolId = "FallingResources";
        public const int FallingResourcesMemoryPoolSize = 10;

        #endregion

        #region Items

        public const int IronOreInitRandomMin = 3;
        public const int IronOreInitRandomMax = 5;

        public const int GoldOreInitRandomMin = 1;
        public const int GoldOreInitRandomMax = 3;

        public const int FireShardInitRandomMin = 0;
        public const int FireShardInitRandomMax = 2;

        public const int EmberDustInitRandomMin = 0;
        public const int EmberDustInitRandomMax = 2;

        public const int DragonScaleInitRandomMin = 0;
        public const int DragonScaleInitRandomMax = 1;

        public const float LuckyCharmInitPercentageChance = 0.25f;
        public const float TimeAmuletInitPercentageChance = 0.25f;

        #endregion

        #region Falling Resource

        public const float IronOreSpawnPercentageChance = 0.35f;
        public const float GoldOreSpawnPercentageChance = 0.30f;
        public const float FireShardSpawnPercentageChance = 0.15f;
        public const float EmberDustSpawnPercentageChance = 0.15f;
        public const float DragonScaleSpawnPercentageChance = 0.05f;

        #endregion
    }
}
