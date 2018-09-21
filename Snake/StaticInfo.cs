using System;

namespace Snake
{
    public static class StaticInfo
    {
        public static int _maxFieldHeight { get; } = 25;
        public static int _maxFieldWidth { get; } = 100;
        public static int _minFieldBorder { get; } = 0;
        
        public static char _snakeCharacter { get; } = '#';
        public static char _foodCharacter { get; } = 'O';

        public static ConsoleColor _snakeBodyColor { get; } = ConsoleColor.Green;
        public static ConsoleColor _foodColor { get; } = ConsoleColor.Cyan;
        public static ConsoleColor _counterColor { get; } = ConsoleColor.Blue;
        public static ConsoleColor _winOrLoseForegroundColor { get; } = ConsoleColor.DarkRed;
        public static ConsoleColor _textColor { get; } = ConsoleColor.White;
    }
}