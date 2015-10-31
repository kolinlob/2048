using System;
using System.Collections;
using System.Collections.Generic;

namespace Game2048
{
    public struct Tile : IEquatable<Tile>
    {
        public int Value { get; set; }


        public override string ToString() => Value.ToString();

        public Tile(int param) { Value = param; }

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