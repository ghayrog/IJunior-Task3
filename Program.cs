using System;
using System.Collections.Generic;

namespace CardGame
{
    class Program
    {
        public const int MAX_PLAYER_CARDS = 5;
        public const int MIN_PLAYER_POWER = 3;
        public const int MAX_PLAYER_POWER = 20;
        public const int MAX_ENEMY_CARDS = 1;
        public const int MIN_ENEMY_POWER = 1;
        public const int ENEMY_POWER_INTERVAL = 3;
        public const int SWAP_COST = 100;
        public const int WIN_BASE_COST = 10;
        public const int MAX_ROUNDS = 10;
        
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            float difficulty = 0;
            float difficultyIncrement = 1.5f;
            bool isExiting = false;
            bool isInputOk = false;
            string userInput;
            int roundCount = 0;

            Character player = new Character();
            Character enemy= new Character();
            for (int i = 0; i < MAX_PLAYER_CARDS; i++)
            {
                player.AddCardToDeck(CardFactory.GenerateCard(MIN_PLAYER_POWER, MAX_PLAYER_POWER));
            }
            while (!isExiting)
            {
                if (enemy.Deck.Count < MAX_ENEMY_CARDS)
                {
                    enemy.AddCardToDeck(CardFactory.GenerateCard((int)(MIN_ENEMY_POWER + difficulty), (int)(MIN_ENEMY_POWER + difficulty + ENEMY_POWER_INTERVAL)));
                    roundCount++;
                    difficulty+=difficultyIncrement;
                    if (roundCount > MAX_ROUNDS)
                    {
                        break;
                    }
                }

                GUIManager.ClearFrame();
                GUIManager.DrawText(0, 0, $"Раунд номер {roundCount} из {MAX_ROUNDS}. У вас {player.Wallet} золота.");
                GUIManager.DrawText(0, 1, $"Ваши карты ({player.Deck.Count}):");
                for (int i = 0; i < player.Deck.Count; i++)
                {
                    GUIManager.DrawPlayerCard(i * GUIManager.CARD_DISTANCE, 2, player.Deck[i]);
                }
                GUIManager.DrawText(0, 11, $"Противник:");
                for (int i = 0; i < enemy.Deck.Count; i++)
                {
                    GUIManager.DrawEnemyCard(i * GUIManager.CARD_DISTANCE, 12, enemy.Deck[i]);
                }
                GUIManager.DrawText(0, 21, $"Список команд:");
                GUIManager.DrawText(0, 22, $"kill N - напасть картой с порядковым номером N");
                GUIManager.DrawText(0, 23, $"talk N - уговорить картой с порядковым номером N");
                GUIManager.DrawText(0, 24, $"swap N - поменять карту с номером N за {SWAP_COST} золотых");
                GUIManager.DrawText(0, 25, $"q или quit - выйти из игры");
                GUIManager.DrawFrame();
                Console.WriteLine($"Введите команду:");
                isInputOk = false;
                while (!isInputOk)
                {
                    userInput = Console.ReadLine();

                    InputState inputState = UserInput.GetInputState(userInput);
                    int inputParam = UserInput.GetInputParam(userInput);

                    switch (inputState)
                    {
                        case InputState.Unknown:
                            Console.WriteLine("Команда не распознана, введите команду из списка выше");
                            break;
                        case InputState.Quit:
                            isExiting = true;
                            isInputOk = true;
                            break;
                        case InputState.Kill:
                            if (inputParam > 0 && inputParam <= player.Deck.Count)
                            {
                                isInputOk = true;
                                if (CheckAttackResult(player.Deck[inputParam - 1], enemy.Deck[0]))
                                {
                                    enemy.RemoveCardFromDeck(0);
                                    player.AddMoney((int)(WIN_BASE_COST * (1 + difficulty)));
                                    Console.WriteLine("Ура! Вы победили в этом раунде путем грубой силы (нажмите любую клавишу).");
                                    Console.ReadKey();
                                }
                                else
                                { 
                                    isExiting = true;
                                    Console.WriteLine($"Вы проиграли бой! Вам удалось набрать лишь {player.Wallet} монет.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Некорректно введен номер карты для атаки (введите корректную команду)");
                            }
                            break;
                        case InputState.Talk:
                            if (inputParam > 0 && inputParam <= player.Deck.Count)
                            {
                                isInputOk = true;
                                if (CheckTalkResult(player.Deck[inputParam - 1], enemy.Deck[0]))
                                {
                                    enemy.RemoveCardFromDeck(0);
                                    player.AddMoney((int)(WIN_BASE_COST * (1 + difficulty)));
                                    Console.WriteLine("Ура! Вы победили в этом раунде путем дипломатии (нажмите любую клавишу).");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    isExiting = true;
                                    Console.WriteLine($"Вы проиграли! Вам удалось набрать лишь {player.Wallet} монет.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Некорректно введен номер карты для убеждения (введите корректную команду)");
                            }
                            break;
                        case InputState.Swap:
                            if (inputParam > 0 && inputParam <= player.Deck.Count)
                            {
                                if (player.Wallet>=SWAP_COST)
                                {
                                    isInputOk = true;
                                    player.SpendMoney(SWAP_COST);
                                    player.RemoveCardFromDeck(inputParam-1);
                                    player.AddCardToDeck(CardFactory.GenerateCard(MIN_PLAYER_POWER, MAX_PLAYER_POWER));
                                }
                                else
                                {
                                    Console.WriteLine($"Не достаточно монет для обмена, нужно хотя бы {SWAP_COST} монет. Введите корректную команду.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Некорректно введен номер карты для обмена (введите корректную команду).");
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
            if (roundCount > MAX_ROUNDS) Console.WriteLine($"Вы победили! Вам удалось набрать {player.Wallet} монет (нажмите любую клавишу).");
            Console.ReadKey();
        }

        static bool CheckAttackResult(Card playerCard, Card enemyCard)
        {
            float debuff = 1;
            if (playerCard.Nature == enemyCard.Nature) debuff = 0.7f;
            if (playerCard.Power * debuff >= enemyCard.Power)
            {
                return true;
            }
            return false;
        }

        static bool CheckTalkResult(Card playerCard, Card enemyCard)
        {
            float maxDiff = 9;
            float diff = Math.Abs(playerCard.Alignment.GetAlignment()-enemyCard.Alignment.GetAlignment());
            double chance = 1 - diff / maxDiff;
            double randomDouble = rnd.NextDouble();
            if (randomDouble < chance)
            {
                return true;
            }
            return false;
        }

    }
}
