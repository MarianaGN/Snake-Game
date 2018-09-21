using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snake
{
    public class SnakeGameField
    {
        private LinkedList<Snake> SnakeNodes { get; set; } = new LinkedList<Snake>();
        private List<FoodPoint> FoodPoints { get;  } = new List<FoodPoint>();
        private List<Snake> SnakeObjects { get;  } = new List<Snake>();

        private LinkedListNode<Snake> Head { get; set; }
        private LinkedListNode<Snake> Tail { get; set; }

        private static int _counter;

        public void SetGameField(int amountOfSnakesFood, int snakeLength)
        {
            Console.SetWindowSize(StaticInfo._maxFieldWidth, StaticInfo._maxFieldHeight);
            var random = new Random();
            var index = 0;

            Console.ForegroundColor = StaticInfo._foodColor;

            while (index < amountOfSnakesFood)
            {
                var pointColumn = random.Next(StaticInfo._minFieldBorder, StaticInfo._maxFieldWidth -5);
                var pointRow = random.Next(StaticInfo._minFieldBorder, StaticInfo._maxFieldHeight -5);

                FoodPoints.Add(new FoodPoint(pointRow, pointColumn));

                Console.SetCursorPosition(pointColumn, pointRow);
                Console.Write(StaticInfo._foodCharacter);
                ++index;
            }

            _counter = -1;
            IncrementCounter();
            CreateSnakeBody(snakeLength);
        }

        private static void IncrementCounter()
        {
            Console.SetCursorPosition(StaticInfo._maxFieldWidth - 15, StaticInfo._minFieldBorder + 1);
            Console.ForegroundColor = StaticInfo._counterColor;
            Console.Write($"Counter : {++_counter}");
        }

        private void CreateSnakeBody(int snakeLength)
        {
            Console.SetCursorPosition(StaticInfo._minFieldBorder, StaticInfo._minFieldBorder);
            Console.ForegroundColor = StaticInfo._snakeBodyColor;

            ClearSnakeNodes();

            for (var i = 0; i < snakeLength; i++)
            {
                SnakeObjects.Add(new Snake(StaticInfo._minFieldBorder, i));
                Console.Write(StaticInfo._snakeCharacter);
            }
            
            var key = Console.ReadKey();

            if (key.Key != ConsoleKey.Enter) return;

            SnakeNodes = new LinkedList<Snake>(SnakeObjects);
            Head = SnakeNodes.Last;
            Tail = SnakeNodes.First;
        }

        private void ClearSnakeNodes()
        {
            if (SnakeNodes.Any())
                SnakeNodes.Clear();

            if (SnakeObjects.Any())
                SnakeObjects.Clear();
        }
        
        public void RunSnake()
        {
            var exit = false;
            var key = ConsoleKey.RightArrow;

            Task.Factory.StartNew(() =>
            {
                key = Console.ReadKey().Key;
                while (key != ConsoleKey.Enter)
                    key = Console.ReadKey().Key;

                exit = true;
            });

            while (!exit)
            {
                CheckIfThereSomeFood();
                MoveSnake(key);
            }
        }

        private void CheckIfThereSomeFood()
        {
            var foodPoint = FoodPoints.FirstOrDefault(f => f.PointColumn == Console.CursorLeft
                                                            && f.PointRow == Console.CursorTop);

            if (foodPoint == null) return;

            FoodPoints.Remove(foodPoint);
            IncrementCounter();

            if (!FoodPoints.Any())
                WinGame();
        }
        
        private void MoveSnake(ConsoleKey key)
        {
            if (key == ConsoleKey.RightArrow)
            {
                if (Head.Value.Column + 1 == StaticInfo._maxFieldWidth)
                    LoseGame();
                else
                    Head.Value.MoveRight(Head.Value, Tail.Value);
            }
            else if (key == ConsoleKey.LeftArrow)
            {
                if (Head.Value.Column - 1 < StaticInfo._minFieldBorder)
                    LoseGame();
                else
                    Head.Value.MoveLeft(Head.Value, Tail.Value);
            }
            else if (key == ConsoleKey.UpArrow)
            {
                if (Head.Value.Row - 1 < StaticInfo._minFieldBorder)
                    LoseGame();
                else
                    Head.Value.MoveUp(Head.Value, Tail.Value);
            }
            else if (key == ConsoleKey.DownArrow)
            {
                if (Head.Value.Row + 1 > StaticInfo._maxFieldHeight)
                    LoseGame();
                else
                    Head.Value.MoveDown(Head.Value, Tail.Value);
            }

            UpdateSnakeNodes();
        }

        private void LoseGame()
        {
            Console.ForegroundColor = StaticInfo._winOrLoseForegroundColor;
            Console.SetCursorPosition(StaticInfo._maxFieldWidth - 55, StaticInfo._maxFieldHeight - 17);
            Console.Write("Game Over!");

            RestartOrEndGame();
        }

        private void WinGame()
        {
            Console.ForegroundColor = StaticInfo._winOrLoseForegroundColor;
            Console.SetCursorPosition(StaticInfo._maxFieldWidth - 55, StaticInfo._maxFieldHeight - 17);
            Console.Write("Congratulations!");
            Console.SetCursorPosition(StaticInfo._maxFieldWidth - 50, StaticInfo._maxFieldHeight - 16);
            Console.Write("You win!");

            RestartOrEndGame();
        }

        private void RestartOrEndGame()
        {
            Console.ForegroundColor = StaticInfo._textColor;
            Console.SetCursorPosition(StaticInfo._maxFieldWidth -70, StaticInfo._maxFieldHeight -14);
            Console.WriteLine("If you want to start again press - ENTER: ");

            Console.SetCursorPosition(StaticInfo._maxFieldWidth -70, StaticInfo._maxFieldHeight -12);
            Console.WriteLine("If you want exit press - Esc: ");
            
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            else if(Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Console.Clear();

                Console.WriteLine("Change amount of food items for game field: ");
                var amountOfFoodNodes = Console.ReadLine();

                while (string.IsNullOrEmpty(amountOfFoodNodes))
                    amountOfFoodNodes = Console.ReadLine();
                
                Console.WriteLine("Change snake length: ");
                var snakeLength = Console.ReadLine();

                while (string.IsNullOrEmpty(snakeLength))
                    snakeLength = Console.ReadLine();

                Console.Clear();

                SetGameField(Convert.ToInt32(amountOfFoodNodes), Convert.ToInt32(snakeLength));
                RunSnake();
            }
        }

        private void UpdateSnakeNodes()
        {
            foreach (var snakeNode in SnakeNodes)
            {
                if (snakeNode == Head.Value) continue;

                var currentNode = SnakeNodes.Find(snakeNode);
                var nextNode = currentNode?.Next?.Value;

                if (snakeNode.Column < nextNode?.Column)
                    snakeNode.Column += 1;
                if (snakeNode.Column > nextNode?.Column)
                    snakeNode.Column -= 1;
                if (snakeNode.Row < nextNode?.Row)
                    snakeNode.Row += 1;
                if (snakeNode.Row > nextNode?.Row)
                    snakeNode.Row -= 1;
            }
        }
    }
}