using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;

namespace MetaLifeEngine
{
	public class Grid
	{
		public Dictionary<MetaCoordinate, MetaCell> Cells { get; set; }

		public MetaGame Game { get; set; }
		public Grid(MetaGame game) 
		{
			this.Game = game;
			Cells = new Dictionary<MetaCoordinate, MetaCell>();
		}
		public MetaCell AddNewCell(int x, int y, int z, MetaLambda lambda, int lifeForm, int energy)
		{
			var coordinates = new MetaCoordinate(x, y, z);
			var anotherCellEnergy = 0; 
			if (this.Cells.ContainsKey(coordinates))
			{
				if (this.Cells[coordinates].LifeForm != lifeForm)
				{
					anotherCellEnergy = this.Cells[coordinates].Energy;
					this.Cells.Remove(coordinates);
				}
				else 
				{
					return null;
				}
			}
			var newCell = new MetaCell(Game, coordinates, lambda, lifeForm, energy + anotherCellEnergy);
			this.Cells.Add(coordinates, newCell);
			return newCell;
		}

		public MetaCell Move(MetaCell cell, int x, int y, int z)
        {
            var coordinates = new MetaCoordinate(x, y, z);
			if (this.Cells.ContainsKey(coordinates))
			{
				return null;
			}
			this.Cells.Remove(cell.Coordiantes);
			cell.Coordiantes = coordinates;
			this.Cells.Add(coordinates, cell);
			return cell;
        }
		public void Log()
		{
			foreach (var cell in this.Cells.Values)
			{
				Console.WriteLine(string.Format("[{0} {1} {2} {3} {4}]", 
					cell.Coordiantes.X, cell.Coordiantes.Y, cell.Coordiantes.Z, cell.LifeForm, cell.Energy));
			}
		}
    }
}
