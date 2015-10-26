using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace Game2048
{
    public class Board
    {
        private Tile[,] _board;
        public int Size => _board.GetLength(0);

        public Tile this[int row, int col]
        {
            get { return _board[row, col]; }
            set
            {
                _board[row, col].Value = value.Value;
                _board[row, col].IsChanged = value.IsChanged;    
            }
        }

        public Board(Tile[,] array)
        {
            _board = array;

            //new Tile[array.GetLength(0),array.GetLength(1)];
            //
            // for (int i = 0; i < _board.GetLength(0); i++)
            //     for (int j = 0; j < _board.GetLength(1); j++)                
            //         _board[i, j] = array[i, j];    
        }

        public Board() : this(new Tile[4,4]) { }

        public void Display()
        {
            Console.WriteLine();
            for (var row = 0; row < _board.GetLength(0); row++)
            {
                for (var col = 0; col < _board.GetLength(1); col++)
                    Console.Write(_board[row, col] == 0 ? "_".PadLeft(5) : _board[row, col].Value.ToString().PadLeft(5));
                Console.WriteLine("\r\n");
            }
        }

        public bool HasEmptyCells()
        {
            for (var row = 0; row < _board.GetLength(0); row++)
                for (var col = 0; col < _board.GetLength(1); col++)
                    if (_board[row, col] == 0) return true;
            return false;
        }
    }
}