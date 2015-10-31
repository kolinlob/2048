using System;

namespace Game2048
{
    public class Board : IEquatable<Board>
    {
        private Tile[,] board;
        public int Size => board.GetLength(0);

        public Tile this[int row, int col]
        {
            get { return board[row, col]; }
            set { board[row, col].Value = value.Value; }
        }

        public Board(Tile[,] array)
        {
            board = array;  
        }

        public Board(Board b)
        {
            board = new Tile[b.Size,b.Size];
            for (var row = 0; row < b.Size; row++)
                for (var col = 0; col < b.Size; col++)
                    board[row, col] = b[row, col];
        }

        public Board() : this(new Tile[4,4]) { }

        public void Display()
        {
            Console.WriteLine("  " + "".PadLeft(5 * Size, '-') + "\r\n");
            for (var row = 0; row < board.GetLength(0); row++)
            {
                for (var col = 0; col < board.GetLength(1); col++)
                    Console.Write(board[row, col] == 0 ? ".".PadLeft(5) : board[row, col].Value.ToString().PadLeft(5));
                Console.WriteLine("\r\n");        
            }
            Console.WriteLine("  " + "".PadLeft(5 * Size, '-') + "\r\n");
            Console.Write("  Score: ");
        }

        public bool HasEmptyCells()
        {
            for (var row = 0; row < board.GetLength(0); row++)
                for (var col = 0; col < board.GetLength(1); col++)
                    if (board[row, col] == 0) return true;
            return false;
        }

        public static bool Equals(Board x, Board y)
        {
            if (x.Size != y.Size)
                return false;

            for (var row = 0; row < x.Size; row++)
                for (var col = 0; col < x.Size; col++)
                    if (x[row, col] != y[row, col])
                        return false;
            return true;
        }

        public bool Equals(Board other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(board, other.board);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Board) obj);
        }

        public override int GetHashCode()
        {
            return board?.GetHashCode() ?? 0;
        }

        public static bool operator ==(Board x, Board y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(Board x, Board y)
        {
            return !Equals(x, y);
        }
    }
}