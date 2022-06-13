using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MetaLifeEngine
{
	public class MetaGame
	{
		public Grid Grid { get; private set; }
		public Grid NewGrid { get; private set; }
		public int IterationNumber { get; private set; }
		public int MaxIteratonNumber { get; set; }
		public List<int> AliveToSurvive { get; set; }
		public List<int> EmptyToBorn { get; set; }

		public MetaGame()
		{
			this.Grid = new Grid();
		}

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
            this.LogToFile(this.IterationNumber, this.Grid);
            // log into console every 100 iteration
			if (this.IterationNumber % 1 == 0)
			{
			   // this.Grid.Log();
			}
            // Console.ReadKey();

			this.StartIteration();

			this.IterationNumber++;
			if (this.IterationNumber >= this.MaxIteratonNumber) return;

			this.EndIteration();
		}

		public void StartIteration()
		{
			/*
				0. Create new empty grid
				1. For each empty cell in the current grid that has at least one "live" neighbour 
					1.a. Get all "live" neighbours 
					1.b. Create new cell and "Absorb" all neighbours one by one
					1.c. Put into the new grid
				2. For each "live" cells in the current grid
					2.a. Get all "live" neighbours
					2.b. "Absorb" all neighbours one by one by the current cell
					2.c. Put into the new Grid
				3. Apply the "rule" to all cells in the new Grid
					3.a If compuitatoin complexity > max allowed complexity => kill the cell
					3.b If computatioin complexity < min allowed complexity => kill the cell
				4. Make the new grid as current  
			*/
			this.NewGrid = new Grid();

			foreach (var cellCoordinate in this.Grid.Cells.Keys)
			{
				// process existing cell
				var cell = this.Grid.Cells[cellCoordinate];
				var cellNeighbours = this.Grid.GetNeighbours(cell);
				var newCell = new MetaCell(cellCoordinate, x => x, 1);
				newCell.State = CELL_STATE.ALIVE;
				foreach (var neighbour in cellNeighbours) 
				{
					newCell.Absorb(neighbour);
				}

				if (this.AliveToSurvive.Contains(newCell.ComputationComplexity - 1))
				{
					this.NewGrid.Cells.Add(cellCoordinate, newCell);
				}

				// process empty cells that have at least one neighbor
				var cellEmptyNeighbours = this.Grid.GetEmptyNeighbours(cell);
				foreach (var emptyCellCoordnates in cellEmptyNeighbours)
				{
					if (this.NewGrid.Cells.ContainsKey(emptyCellCoordnates)) continue;

					var newEmptyCell = new MetaCell(emptyCellCoordnates, x => x, 1);
					var emptyCellNeighbours = this.Grid.GetNeighbours(newEmptyCell);
					foreach (var neighbour in emptyCellNeighbours) 
					{
						newEmptyCell.Absorb(neighbour);
					}

					if (this.EmptyToBorn.Contains(newEmptyCell.ComputationComplexity - 1))
					{
						this.NewGrid.Cells.Add(emptyCellCoordnates, newEmptyCell);
					}
				}
			}
		}

		public void EndIteration()
		{
			// replace the axising grid with new one and filter out dead cells
			this.Grid.Cells = this.NewGrid.Cells;

			this.PlaySingleIteration();
		}

		private void LogToFile(int iteration, Grid grid)
		{
            using StreamWriter file = new("savedData.txt", append: true);
            file.Write(iteration);
            var dataToSave = new StringBuilder();
            foreach (var cell in grid.Cells.Values)
			{
                dataToSave.AppendFormat("|{0} {1} {2} {3}|", 
					cell.Coordiantes.X, cell.Coordiantes.Y, cell.Coordiantes.Z, cell.ComputationComplexity);
            }
            file.WriteLine(dataToSave);
		}
	}
}