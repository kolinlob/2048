using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Security.Policy;

namespace Game2048
{
    public class Game
    {
        private Board _board;
        private Random _random;

        public void Start()
        {
            var arr = new[,]
            {
                {new Tile(0), new Tile(2), new Tile(4), new Tile(0)},
                {new Tile(2), new Tile(0), new Tile(2), new Tile(0)},
                {new Tile(0), new Tile(0), new Tile(2), new Tile(0)},
                {new Tile(2), new Tile(2), new Tile(0), new Tile(0)},
            };

            _board = new Board(arr);
            _random = new Random();

            while (!IsGameOver())
            {
                Turn();
                Console.Clear();
            }
        }

        private void Turn()
        {
            //CreateNewTile();
            for (int i = 0; i < _board.Size; i++)
                for (int j = 0; j < _board.Size; j++)
                    _board[i, j].ResetFlag();

            _board.Display();
            Console.ReadKey();
            SumUp();
        }

        private void CreateNewTile()
        {
            var x = _random.Next(0, 4);
            var y = _random.Next(0, 4);

            while (_board[x, y] != 0)
            {
                x = _random.Next(0, 4);
                y = _random.Next(0, 4);
            }
            _board[x, y] = 2 + _random.Next(0, 1) * 2;
        }

        private void Sum()
        {
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow: SumUp(); break;
                case ConsoleKey.DownArrow: SumDown(); break;
                case ConsoleKey.LeftArrow: SumLeft(); break;
                case ConsoleKey.RightArrow: SumRight(); break;
            }
        }

        //private void SumUp()
        //{
        //    for (var col = 0; col < _board.Size; col++)
        //        for (var row = 0; row < _board.Size; row++)
        //            if (_board[row, col] == 0)
        //                if (row < _board.Size - 1)
        //                {
        //                    _board[row, col] = _board[row + 1, col];
        //                    _board[row + 1, col] = 0;
        //                }
        //            
        //    for (var row = 0; row < _board.Size -1; row++)
        //        for (var col = 0; col < _board.Size; col++)
        //            if (row < _board.Size - 1 && _board[row, col] == _board[row + 1, col])
        //            {
        //                _board[row, col] *= 2;
        //                _board[row + 1, col] = 0;
        //            }
        //}

        private void SumUp()
        {
            for (var col = 0; col < _board.Size; col++)
            {
                var list = new List<Tile>();

                for (var row = 0; row < _board.Size; row++)
                    list.Add(_board[row, col]);

                var empty = list.Count(tile => tile == 0);
                list.RemoveAll(i => i == 0);
                list.InsertRange(list.IndexOf(list.Last()), new Tile[empty]);
            }
        }
        //for (var col = 0; col < _board.Size; col++)
        //    for (var row = 0; row < _board.Size - 1; row++)
        //        if (_board[row, col] == _board[row + 1, col])
        //        {
        //            if (_board[row, col].IsChanged) continue;
        //            _board[row, col].Double();
        //            _board[row + 1, col].Clear();
        //        }

        //for (var row = _board.Size - 1; row > 0; row--)
        //{
        //    for (var col = 0; col < _board.Size; col++)
        //    {
        //        if (_board[row, col] == 0) continue;
        //
        //        if (_board[row - 1, col] == 0)
        //        {
        //            _board[row - 1, col] = _board[row, col];
        //            _board[row, col] = 0;
        //            continue;
        //        }
        //
        //        if (_board[row, col] == _board[row - 1, col])
        //        {
        //            if (_board[row, col].IsChanged) continue;
        //            _board[row - 1, col].Double();
        //            _board[row, col] = 0;
        //        }
        //    }
        //    Console.ReadLine();
        //    Console.Clear();
        //    _board.Display();
        //}


        private void SwitchRows(int row, int col)
        {
            if (row < _board.Size - 1)
            {
                var temp = _board[row, col];
                _board[row, col] = _board[row + 1, col];
                _board[row + 1, col] = temp;
            }
        }

        private void SumDown()
        {
            for (int row = 0; row < _board.Size; row++)
                for (int col = 0; col < _board.Size; col++)
                {
                    if (row < _board.Size - 1)
                    {
                        if (_board[row + 1, col] == _board[row, col])
                        {
                            _board[row + 1, col] += _board[row, col];
                            _board[row, col] = 0;
                        }
                    }
                }
        }

        private void SumLeft()
        {
            for (int row = _board.Size - 1; row >= 0; row--)
                for (int col = 0; col < _board.Size; col++)
                {
                    if (row > 0)
                    {
                        if (_board[row - 1, col] == _board[row, col])
                        {
                            _board[row - 1, col] += _board[row, col];
                            _board[row, col] = 0;
                        }
                    }
                }
        }

        private void SumRight()
        {
            for (int row = _board.Size - 1; row >= 0; row--)
                for (int col = 0; col < _board.Size; col++)
                {
                    if (row > 0)
                    {
                        if (_board[row - 1, col] == _board[row, col])
                        {
                            _board[row - 1, col] += _board[row, col];
                            _board[row, col] = 0;
                        }
                    }
                }
        }

        private bool IsGameOver()
        {
            for (int i = 0; i < _board.Size; i++)
                for (int j = 0; j < _board.Size; j++)
                    if (_board[i, j] == 2048)
                        return true;
            return !_board.HasEmptyCells();
        }
    }
}