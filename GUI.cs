using System;

namespace CardGame
{
    public static class GUIManager
    {
        public const int SCREEN_X = 120;
        public const int SCREEN_Y = 26;
        public const int CARD_DISTANCE = 21;
        public const int CARD_X_SIZE = 20;
        public const int CARD_Y_SIZE = 9;

        private static char[,] frame = new char[SCREEN_X, SCREEN_Y];

        public static void DrawWindowBorder(int _leftX, int _topY, int _rightX, int _bottomY)
        {
            if (_leftX >= 0 && _topY >= 0 && _rightX < SCREEN_X && _bottomY < SCREEN_Y)
            {
                frame[_leftX, _topY] = '╔';
                frame[_rightX, _topY] = '╗';
                frame[_rightX, _bottomY] = '╝';
                frame[_leftX, _bottomY] = '╚';
                for (int i = _leftX + 1; i < _rightX; i++)
                {
                    frame[i, _topY] = '═';
                    frame[i, _bottomY] = '═';
                }
                for (int i = _topY+1; i < _bottomY; i++)
                {
                    frame[_leftX, i] = '║';
                    frame[_rightX, i] = '║';
                }
                frame[_leftX, _topY + 2] = '╟';
                frame[_rightX, _topY + 2] = '╢';
                for (int i = _leftX + 1; i < _rightX; i++)
                {
                    frame[i, _topY + 2] = '─';
                }
            }
        }
        public static void DrawPlayerCard(int _positionX, int _positionY, Card _card)
        {
            DrawWindowBorder(_positionX, _positionY, _positionX + CARD_X_SIZE - 1, _positionY + CARD_Y_SIZE - 1);
            DrawText(_positionX + 3, _positionY + 1, _card.Name);
            DrawText(_positionX + 1, _positionY + 3, $"Стихия {GameSettings.GetNatureNameById(_card.Nature)}");
            DrawText(_positionX + 1, _positionY + 4, $"Сила {_card.Power}");
            DrawText(_positionX + 1, _positionY + 6, $"Мировоззрение ({_card.Alignment.GetAlignment()}):");
            DrawText(_positionX + 1, _positionY + 7, $"{_card.GetAlignmentString()}");            
        }
        public static void DrawEnemyCard(int _positionX, int _positionY, Card _card)
        {
            DrawWindowBorder(_positionX, _positionY, _positionX + CARD_X_SIZE - 1, _positionY + CARD_Y_SIZE - 1);
            DrawText(_positionX + 3, _positionY + 1, _card.Name);
            DrawText(_positionX + 1, _positionY + 3, $"Стихия {GameSettings.GetNatureNameById(_card.Nature)}");
            DrawText(_positionX + 1, _positionY + 4, $"Здоровье {_card.Power}");
            DrawText(_positionX + 1, _positionY + 6, $"Мировоззрение ({_card.Alignment.GetAlignment()}):");
            DrawText(_positionX + 1, _positionY + 7, $"{_card.GetAlignmentString()}");
        }
        public static void DrawText(int _positionX, int _positionY, string _text)
        {
            for (int i = 0; i < _text.Length; i++)
            {
                if (_positionX >= 0 && _positionX + i < SCREEN_X) frame[_positionX + i, _positionY] = _text[i];
            }
        }
        public static void ClearFrame()
        {
            for (int j = 0; j < SCREEN_Y; j++)
            {
                for (int i = 0; i < SCREEN_X; i++)
                {
                    frame[i, j] = ' ';
                }
            }
        }
        public static void DrawFrame()
        {
            Console.Clear();
            string textToDraw = "";
            for (int j = 0; j < SCREEN_Y; j++)
            {
                textToDraw = "";
                for (int i = 0; i < SCREEN_X; i++)
                {
                    textToDraw += frame[i, j];
                }
                Console.WriteLine(textToDraw);
            }
        }
    }
}
