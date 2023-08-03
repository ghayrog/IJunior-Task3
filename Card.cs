using System;

namespace CardGame
{
    public enum Nature
    {
        Fire,
        Water,
        Earth,
        Air
    }
    public enum ChaosAxis
    {
        Lawful,
        Neutral,
        Chaotic,
    }
    public enum EvilAxis
    {
        Good,
        Neutral,
        Evil,
    }
    public class Card
    {
        public const int MAX_POWER = 20;
        public string Name { get; private set; }
        public Nature Nature { get; private set; }
        public int Power { get; private set; }
        public Alignment Alignment { get; private set; }

        public Card(string _name, Nature _nature, int _power, ChaosAxis _chaosAxis, EvilAxis _evilAxis)
        {
            if (_name.Length == 0) throw new ArgumentException("Card Name cannot be empty");
            if (_power < 1 || _power > MAX_POWER) throw new ArgumentException("Card Power cannot be less than 1 or greater than 20");
            Name = _name;
            Nature = _nature;
            Power = _power;
            Alignment = new Alignment(_chaosAxis, _evilAxis);
        }

        public string GetAlignmentString()
        {
            string _firstAlignment = GameSettings.GetChaosNameById(Alignment.ChaosAxis);
            string _lastAlignment = GameSettings.GetEvilNameById(Alignment.EvilAxis);
            return $"{_firstAlignment} {_lastAlignment}";

        }
    }
    public struct Alignment
    {
        public ChaosAxis ChaosAxis { get; private set; }
        public EvilAxis EvilAxis { get; private set; }
        public Alignment(ChaosAxis _chaosAxis, EvilAxis _evilAxis)
        {
            ChaosAxis = _chaosAxis;
            EvilAxis = _evilAxis;
        }
        public int GetAlignment()
        {
            return ((int)ChaosAxis + 1) * ((int)EvilAxis + 1);
        }
    }
    public static class CardFactory
    {
        private static Random rnd = new Random();
        public static Card GenerateCard(int _powerRangeMin, int _powerRangeMax)
        {
            string _name = GenerateName();
            int _power = rnd.Next(Math.Max(_powerRangeMin, 1), Math.Min(_powerRangeMax, Card.MAX_POWER) + 1);
            Nature _nature = (Nature)rnd.Next(0, GameSettings.MAX_NATURES);
            ChaosAxis _chaosAxis = (ChaosAxis)rnd.Next(0, GameSettings.MAX_CHAOS);
            EvilAxis _evilAxis = (EvilAxis)rnd.Next(0, GameSettings.MAX_EVIL);

            var _card = new Card(_name, _nature, _power, _chaosAxis, _evilAxis);
            return _card;
        }

        public static string GenerateName()
        {
            string[] _firstNamePart = new string[10] { "Го", "Бу", "Жа", "Ду", "Та", "По", "Бе", "Ра", "Ка", "Ху" };
            string[] _middleNamePart = new string[10] { "ро", "на", "бо", "де", "ту", "пы", "би", "ру", "пи", "ли" };
            string[] _lastNamePart = new string[10] { "дор", "дан", "кряк", "хор", "мир", "гон", "мор", "лот", "бар", "хан" };

            string _resultName = _firstNamePart[rnd.Next(0, 10)] + _middleNamePart[rnd.Next(0, 10)] + _lastNamePart[rnd.Next(0, 10)];
            return _resultName;
        }
    }
}
