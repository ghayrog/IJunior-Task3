namespace CardGame
{
    public static class GameSettings
    {
        public const int MAX_NATURES = 4;
        public const int MAX_CHAOS = 3;
        public const int MAX_EVIL = 3;

        private static string[] _natureNamesLocal = new string[MAX_NATURES] { "огонь", "вода", "земля", "воздух" };
        private static string[] _chaosNamesLocal = new string[MAX_CHAOS] { "законопосл.", "нейтр.", "хаотич." };
        private static string[] _evilNamesLocal = new string[MAX_EVIL] { "добрый", "нейтр.", "злой" };
        public static string GetNatureNameById(Nature _id)
        {
            return _natureNamesLocal[(int)_id];
        }
        public static string GetChaosNameById(ChaosAxis _id)
        {
            return _chaosNamesLocal[(int)_id];
        }
        public static string GetEvilNameById(EvilAxis _id)
        {
            return _evilNamesLocal[(int)_id];
        }
    }
}
