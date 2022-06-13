using System;

namespace MetaLifeEngine 
{
    public class MetaCell
    {
        // 
        // Make composite of the lambda functions - objects - so that we can campare them 
        // 
        public MetaCoordinate Coordiantes { get; private set; }
        public MetaLambda Lambda { get; set; }
        public int ComputationComplexity { get; set; }
        public CELL_STATE State { get; set; }
        public MetaCell(MetaCoordinate coordinates, MetaLambda lambda, int complexity) 
            => (Coordiantes, Lambda, ComputationComplexity, State) = (coordinates, lambda, complexity, CELL_STATE.NONE);

        public void Absorb(MetaCell otherCell) 
        {
            // merge or aggragate the lambdas
            this.Lambda = cell => otherCell.Lambda(this.Lambda(cell));
            // increase the complexity
            this.ComputationComplexity += 1;
        }
    }
}