using System;

namespace Game2048
{
    public class Tile : IEquatable<Tile>
    {
        public int Value { get; set; }
        public void Clear() => Value = 0;

        public bool IsChanged { get; set; }

        public void ResetFlag() => IsChanged = false;

        public void Double()
        {
            Value *= 2;
            IsChanged = true;
        }

        public override string ToString() => Value + ", " + IsChanged;

        public Tile(int param)
        {
            Value = param;
            IsChanged = false;
        }

        public static bool operator ==(Tile x, int y) { return x.Value == y; }

        public static bool operator ==(Tile x, Tile y) { return x.Value == y.Value; }

        public static bool operator !=(Tile x, int y) { return x.Value != y; }

        public static bool operator !=(Tile x, Tile y) { return x.Value != y.Value; }

        public bool Equals(Tile other) { return Value == other.Value; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Tile && Equals((Tile)obj);
        }

        public override int GetHashCode() { unchecked { return Value * 397; } }

        public static implicit operator int(Tile argument) { return argument.Value; }

        public static implicit operator Tile (int argument) { return new Tile(argument); }
    }
}