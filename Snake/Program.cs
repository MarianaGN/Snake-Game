namespace Snake
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var gameField = new SnakeGameField();
            gameField.SetGameField(15, 5);
            gameField.RunSnake();
        }
    }
}
