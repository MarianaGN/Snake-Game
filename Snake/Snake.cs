using System;
using System.Threading;

namespace Snake
{
    public class Snake
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public void SetRow(int row) => Row = row;

        public void SetColumn(int column) => Column = column;

        public Snake(int rowStart, int columnStart)
        {
            SetRow(rowStart);
            SetColumn(columnStart);
        }

        public void MoveRight(Snake head, Snake tail)
        {
            SetColumn(Column + 1);
            ResolveTailAndHead(head, tail);
        }

        public void MoveLeft(Snake head, Snake tail)
        {
            SetColumn(Column - 1);
            ResolveTailAndHead(head, tail);
        }

        public void MoveDown(Snake head, Snake tail)
        {
            SetRow(Row + 1);
            ResolveTailAndHead(head, tail);
        }

        public void MoveUp(Snake head, Snake tail)
        {
            SetRow(Row - 1);
            ResolveTailAndHead(head, tail);
        }

        private static void ResolveTailAndHead(Snake head, Snake tail)
        {
            Console.SetCursorPosition(head.Column, head.Row);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(StaticInfo._snakeCharacter);
            Thread.Sleep(150);
            ClearSnakePoint(head, tail);
        }

        private static void ClearSnakePoint(Snake snakeHead, Snake snakeTail)
        {
            Console.SetCursorPosition(snakeTail.Column, snakeTail.Row);
            Console.Write(' ');
            Console.SetCursorPosition(snakeHead.Column, snakeHead.Row);
        }
    }
}
