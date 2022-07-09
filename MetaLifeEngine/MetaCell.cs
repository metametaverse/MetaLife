using System;

namespace MetaLifeEngine 
{
    public class MetaCell
    {
        // 
        // Make composite of the lambda functions - objects - so that we can campare them 
        // 
        public MetaCoordinate Coordiantes { get; set; }
        public MetaLambda Lambda { get; set; }
        public int Energy { get; set; }

        public int LifeForm { get; set; }
        public CELL_STATE State { get; set; }

        public MetaGame Game { get; set; }
        public MetaCell(MetaGame game, MetaCoordinate coordinates, MetaLambda lambda, int lifeForm, int energy = 0) 
            => (Game, Coordiantes, Lambda, LifeForm, Energy, State) = (game, coordinates, lambda, lifeForm, energy, CELL_STATE.NONE);

        public void Absorb(MetaCell otherCell) 
        {
            // merge or aggragate the lambdas
            // this.Lambda = cell => otherCell.Lambda(this.Lambda(cell));
            // increase the complexity
            this.Energy += 1;
        }

        public MetaCell Create(int x, int y, int z, MetaLambda lambda, int color, int energy)
        {
            //Console.WriteLine(string.Format("|{0}|{1}|{2}|{3}", x,y,z,energy));
            if (energy < 1) return null;
            if (this.Energy > 1) this.Energy--;
            if (this.Energy - energy <= 1) return this;
            var res = this.Game.Create(x, y, z, lambda, color, energy);
            if (res != null) 
            {
               this.Energy -= energy;
            }
            return res;
        }

        public MetaCell Move(int x, int y, int z)
        {
            //Console.WriteLine(string.Format("|{0}|{1}|{2}|{3}", x,y,z,energy));
            if (this.Energy > 1) this.Energy--;
            if (this.Energy <= 1) return this;
            var res = this.Game.Move(this, x, y, z);
            return res;
        }
    }
}