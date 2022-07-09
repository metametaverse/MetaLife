using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MetaLifeEngine
{
	public class MetaGame
	{
		// MetaLife functions
		//
		public Grid Grid { get; private set; }
		public Grid NewGrid { get; private set; }
		public int IterationNumber { get; private set; }
		public int MaxIteratonNumber { get; set; }
		public List<int> AliveToSurvive { get; set; }
		public List<int> EmptyToBorn { get; set; }

        public MetaGame() => this.Grid = new Grid(this);

        public void Start()
		{
			this.IterationNumber = 0;
			this.PlaySingleIteration();

			Console.WriteLine("MetaLife Simulaton Ended!");
		}

		public void PlaySingleIteration()
		{
			Console.WriteLine(string.Format("ITERATION# {0} CELL COUNT: {1}", this.IterationNumber, this.Grid.Cells.Count));
			// Wriite to File
            //this.LogToFile(this.IterationNumber, this.Grid);
            // log into console every 100 iteration
			if (this.IterationNumber % 1 == 0)
			{
				//this.Grid.Log();
            	this.LogToFile(this.IterationNumber, this.Grid);
			}
            // Console.ReadKey();

			this.StartIteration();

			this.IterationNumber++;
			if (this.IterationNumber > this.MaxIteratonNumber) return;

			this.EndIteration();
		}

        public MetaCell Create(int x, int y, int z, MetaLambda lambda, int lifeForm, int energy)
        {
            return this.Grid.AddNewCell(x, y, z, lambda, lifeForm, energy);
        }

		public void DropEnergy()
        {
			Random r = new Random();
			
			for (int i =0; i <= 2000; i++)
            	this.Grid.AddNewCell(r.Next(-50, 50), r.Next(-20, 20), r.Next(-50, 50),
			 	(cell, x, y, z, lambda, color, energy) => { return null; }, 
				7, 1000000);
        }

		public MetaCell Move(MetaCell cell, int x, int y, int z)
        {
            return this.Grid.Move(cell, x, y, z);
        }

        public void StartIteration()
		{
			this.NewGrid = new Grid(this);
			this.NewGrid.Cells = new Dictionary<MetaCoordinate, MetaCell>(this.Grid.Cells);
			Console.WriteLine(this.NewGrid.Cells.Count);

			foreach (var cellCoordinate in this.NewGrid.Cells.Keys)
			{
				var cell = this.Grid.Cells[cellCoordinate];

				cell.Lambda(cell, cell.Coordiantes.X, cell.Coordiantes.Y, cell.Coordiantes.Z, cell.Lambda, cell.LifeForm);
			}
		}

		public void EndIteration()
		{
			// replace the axising grid with new one and filter out dead cells
			//this.NewGrid = null;

			this.PlaySingleIteration();
		}

		private void LogToFile(int iteration, Grid grid)
		{
            using StreamWriter file = new("savedData.txt", append: true);
            file.Write(iteration);
            var dataToSave = new StringBuilder();
            foreach (var cell in grid.Cells.Values)
			{
                dataToSave.AppendFormat("|{0} {1} {2} {3} {4}|", 
					cell.Coordiantes.X, cell.Coordiantes.Y, cell.Coordiantes.Z, cell.LifeForm, cell.Energy);
            }
            file.WriteLine(dataToSave);
		}
	}
}