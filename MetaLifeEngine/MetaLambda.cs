using System;

namespace MetaLifeEngine 
{
    public delegate MetaCell MetaLambda(MetaCell cell, int x, int y, int z, MetaLambda lambda, int lifeForm, int energy = 1);
}