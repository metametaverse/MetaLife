using System;
using System.Collections.Generic;

namespace MetaLifeEngine.Engine
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("!!! Welcome to the MetLife !!!");

			var game = new MetaGame();
			game.MaxIteratonNumber = 400;

            game.DropEnergy();

            game.Create(
                /* x, y, z */ 25, 0, 0,
                /* lambda  */ (cell, x, y, z, lambda, color, energy) => 
                    {
                        Random r = new Random();
                        var i = r.Next(1000);

                        var energyToPass = cell.Energy / 4 - 5;
                        
                        if (i % 5 == 0)cell.Create(x-1, y+1, z, lambda, color, energyToPass);
                        if (i % 5 == 1)cell.Create(x-1, y-1, z, lambda, color, energyToPass);
                        if (i % 5 == 2)cell.Create(x-1, y, z-1, lambda, color, energyToPass);
                        if (i % 5 == 3)cell.Create(x-1, y, z+1, lambda, color, energyToPass);
                        if (i % 5 == 4)cell.Move(x-1, y, z);
                        return null;

                    },
                /* color   */ 0,
                /* energy  */ 1000000000
            );

            game.Create(
                /* x, y, z */ -25, 0, 0,
                /* lambda  */ (cell, x, y, z, lambda, color, energy) => 
                    {
                        Random r = new Random();
                        var i = r.Next(100);

                        var energyToPass = cell.Energy / 4 - 5;
                        if (i * i % 4 == 0)cell.Create(x+1, y+1, z, lambda, color, energyToPass);
                        if (i * i % 4 == 1)cell.Create(x+1, y-1, z, lambda, color, energyToPass);
                        if (i * i % 4 == 2)cell.Create(x+1, y, z-1, lambda, color, energyToPass);
                        if (i * i % 4 == 3)cell.Create(x+1, y, z+1, lambda, color, energyToPass);

                        return null;

                    },
                /* color   */ 6,
                /* energy  */ 1000000000
            );

			game.Start();
		}
	}
}
