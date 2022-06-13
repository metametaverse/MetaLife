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

        public List<MetaCoordinate> GetAdjacentCoordinates(bool isVon = true)
        {
            var res = new List<MetaCoordinate>();
            // liimit to max value of coordinates, if needed
            int maxValue = 1000000;

            for (int i = -1; i < 2; i++) 
            {
                for (int j = -1; j < 2; j++)
                {
                    for (int k = -1; k < 2; k++)
                    {
                        if (i == 0 && j == 0 && k == 0) continue;
                        if (isVon) 
                        {
                            if (Math.Abs(i) + Math.Abs(j) + Math.Abs(k) != 1) continue;
                        }
                        if (Math.Abs(this.X + i) > maxValue || Math.Abs(this.Y + j) > maxValue || Math.Abs(this.Z + k) > maxValue) continue;

                        res.Add(new MetaCoordinate(this.X + i, this.Y + j, this.Z + k, this.Order));
                    }
                }
            }
            return res;
        }
    }
}