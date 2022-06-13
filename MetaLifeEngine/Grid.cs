using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;

namespace MetaLifeEngine
{
	public class Grid
	{
		public Dictionary<MetaCoordinate, MetaCell> Cells { get; set; }
		public Grid() 
		{
			Cells = new Dictionary<MetaCoordinate, MetaCell>();
		}

		public void AddNewCell(int x, int y, int z)
		{
			var coordinates = new MetaCoordinate(x, y, z, 0);
			this.Cells.Add(coordinates, new MetaCell(coordinates, null, 1));
		}
		public List<MetaCell> GetNeighbours(MetaCell cell)
		{
			var res = new List<MetaCell>();
			var keys = cell.Coordiantes.GetAdjacentCoordinates();
			keys.FindAll(k => this.Cells.ContainsKey(k)).ForEach(c => res.Add(this.Cells[c]));
			return res;
		}
		public List<MetaCoordinate> GetEmptyNeighbours(MetaCell cell)
		{
			var res = new List<MetaCoordinate>();
			var keys = cell.Coordiantes.GetAdjacentCoordinates();
			keys.FindAll(k => !this.Cells.ContainsKey(k)).ForEach(c => res.Add(c));
			return res;
		}

		public void Log()
		{
			foreach (var cell in this.Cells.Values)
			{
				Console.WriteLine(string.Format("[{0} {1} {2} #{3}]", 
					cell.Coordiantes.X, cell.Coordiantes.Y, cell.Coordiantes.Z, cell.ComputationComplexity));
			}
		}
	}
}
