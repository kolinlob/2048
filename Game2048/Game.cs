using System;
using System.Linq;
using System.Collections.Generic;

namespace Game2048
{
    public class Game
    {
        private Board beforeChangesBoard;
        private Board previousTurnBoard;
        private Board currentTurnBoard;
        private Random random;
        private static int currentScore;
        private static int previousScore;

        public Game()
        {
            //TEST CASE #1
            //var arr = new[,]
            //{
            //    {new Tile(2), new Tile(0), new Tile(0), new Tile(2)},
            //    {new Tile(0), new Tile(0), new Tile(0), new Tile(2)},
            //    {new Tile(0), new Tile(0), new Tile(0), new Tile(2)},
            //    {new Tile(0), new Tile(0), new Tile(0), new Tile(2)},
            //};
            //currentTurnBoard = new Board(arr);

            random = new Random();
            currentTurnBoard = new Board();

            currentScore = 0;
            previousScore = currentScore;

            beforeChangesBoard = new Board(currentTurnBoard);
            previousTurnBoard = new Board(beforeChangesBoard);
        }

        public void Start()
        {
            CreateNewTile();

            currentTurnBoard.Display();
            Console.WriteLine(currentScore);

            do { Turn(); }
            while (!IsGameOver());
        }

        private void Turn()
        {
            previousTurnBoard = new Board(currentTurnBoard);

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow: SumUp(); break;
                case ConsoleKey.DownArrow: SumDown(); break;
                case ConsoleKey.LeftArrow: SumLeft(); break;
                case ConsoleKey.RightArrow: SumRight(); break;
                case ConsoleKey.Backspace: Undo(); break;
            }

            if (currentTurnBoard == previousTurnBoard)
                return;

            CreateNewTile();

            ClearScreen();
            currentTurnBoard.Display();
            Console.WriteLine(currentScore);
        }

        private static void ClearScreen() { Console.Clear(); }

        private void CreateNewTile()
        {
            var x = random.Next(0, 4);
            var y = random.Next(0, 4);

            while (currentTurnBoard[x, y] != 0)
            {
                x = random.Next(0, 4);
                y = random.Next(0, 4);
            }
            currentTurnBoard[x, y] = 2 + random.Next(0, 1) * 2;
        }

        private void Undo()
        {
            currentTurnBoard = new Board(beforeChangesBoard);
            ClearScreen();
            currentTurnBoard.Display();

            currentScore = previousScore;
            Console.WriteLine(currentScore);
        }

        private void ShiftTiles(List<Tile> list)
        {
            beforeChangesBoard = new Board(currentTurnBoard);

            if (list.All(i => i == 0)) return;

            list.RemoveAll(i => i == 0);
            var emptyTiles = currentTurnBoard.Size - list.Count;

            for (var i = 0; i < emptyTiles; i++)
                list.Add(new Tile(0));

            previousScore = currentScore;
            SumTiles(list);
        }

        private static void SumTiles(List<Tile> tilesList)
        {
            for (var i = 0; i < tilesList.Count - 1; i++)
            {
                if (tilesList[i] != tilesList[i + 1]) continue;

                tilesList[i] += tilesList[i + 1];

                currentScore += tilesList[i];

                tilesList.RemoveAt(i + 1);

                tilesList.Add(new Tile(0));
            }
        }

        private void SumUp()
        {
            for (var col = 0; col < currentTurnBoard.Size; col++)
            {
                var tilesList = new List<Tile>();

                for (var row = 0; row < currentTurnBoard.Size; row++)
                    tilesList.Add(currentTurnBoard[row, col]);

                ShiftTiles(tilesList);

                for (var row = 0; row < currentTurnBoard.Size; row++)
                    currentTurnBoard[row, col] = tilesList[row];
            }
        }

        private void SumDown()
        {
            for (var col = 0; col < currentTurnBoard.Size; col++)
            {
                var tilesList = new List<Tile>();
                for (var row = currentTurnBoard.Size - 1; row >= 0; row--)
                    tilesList.Add(currentTurnBoard[row, col]);

                ShiftTiles(tilesList);
                tilesList.Reverse();

                for(var row = 0; row < currentTurnBoard.Size; row++)
                    currentTurnBoard[row, col] = tilesList[row];
            }
        }

        private void SumLeft()
        {
            for (var row = 0; row < currentTurnBoard.Size; row++)
            {
                var tilesList = new List<Tile>();
                for (var col = 0; col < currentTurnBoard.Size; col++)
                    tilesList.Add(currentTurnBoard[row, col]);

                ShiftTiles(tilesList);

                for (var col = 0; col < currentTurnBoard.Size; col++)
                    currentTurnBoard[row, col] = tilesList[col];
            }
        }

        private void SumRight()
        {
            for (var row = 0; row < currentTurnBoard.Size; row++)
            {
                var tilesList = new List<Tile>();
                for (var col = 0; col < currentTurnBoard.Size; col++)
                    tilesList.Add(currentTurnBoard[row, col]);

                ShiftTiles(tilesList);
                tilesList.Reverse();

                for (var col = 0; col < currentTurnBoard.Size; col++)
                    currentTurnBoard[row, col] = tilesList[col];
            }
        }

        private bool IsGameOver()
        {
            for (var i = 0; i < currentTurnBoard.Size; i++)
                for (var j = 0; j < currentTurnBoard.Size; j++)
                    if (currentTurnBoard[i, j] == 2048)
                        return true;
            return !currentTurnBoard.HasEmptyCells();
        }
    }
}