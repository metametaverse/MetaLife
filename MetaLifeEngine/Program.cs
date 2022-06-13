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
			game.MaxIteratonNumber = 10;

            // Onother pattern - Chaos - Moore
			/*game.AliveToSurvive = new List<int>() {13,14,15,16,17,18,19,20,21,22,23,24,25,26};
			game.EmptyToBorn = new List<int>() {13,14,17,18,19};*/

            // 1. An Alive cell would servive if it has 0-6 neighbours 
            // 2. New cell would be created in an empty space if it has 1 or 3 neighbours 
            game.AliveToSurvive = new List<int>() {0,1,2,3,4,5,6};
			game.EmptyToBorn = new List<int>() {1, 3};

            // Choose different initial cells and theirs positions
            for (int i = -1; i < 2; i++) 
            {
                for (int j = -1; j < 2; j++)
                {
                    for (int k = -1; k < 2; k++)
                    {
                        if (i == 0 && j == 0 && k == 0) continue;
			            // game.Grid.AddNewCell(i,j,k);
                    }
                }
            }

            // Start from only 1 cell
            game.Grid.AddNewCell(0,0,0);

            /*game.Grid.AddNewCell(0,0,-1);
            game.Grid.AddNewCell(0,1,0);
            game.Grid.AddNewCell(0,-1,0);s
            game.Grid.AddNewCell(-1,0,0);
            game.Grid.AddNewCell(1,0,0);*/

			game.Start();
		}
	}
}
