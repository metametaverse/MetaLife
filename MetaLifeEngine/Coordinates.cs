using System;
using System.Collections.Generic;

namespace MetaLifeEngine 
{
    // We need to encapsulate the management of the coordinates.
    public class MetaCoordinate
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }

        // for Hilbert curve
        public int Order { get; private set; }
        public MetaCoordinate(int x, int y, int z, int order = 0) => (X, Y, Z, Order) = (x, y, z, order);

        public override bool Equals(object other) => other is MetaCoordinate c && (c.X, c.Y, c.Z, c.Order).Equals((X, Y, Z, Order));

        public override int GetHashCode() => (X, Y, Z, Order).GetHashCode();
    }
}