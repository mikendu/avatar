using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Floorplan
{
	public enum Direction { North, East, South, West };

	private Dictionary<int, Dictionary<int, Tile>> plan;
	private int count;
	private Tile firstTile = null;
	private int minX;
	private int minY;
	private int maxX;
	private int maxY;

	public Floorplan()
	{
		plan = new Dictionary<int, Dictionary<int, Tile>>();
		maxX = int.MinValue;
		maxY = int.MinValue;
		minX = int.MaxValue;
		minY = int.MaxValue;
		count = 0;
	}

	public void AddTile(int x, int y)
	{
		if (plan.ContainsKey(x)) {
			if (plan[x].ContainsKey (y))
				return;
		}
		else
			plan[x] = new Dictionary<int, Tile>();

		Tile square = new Tile(x, y);
		plan[x][y] = square;

		if(firstTile == null)
			firstTile = square;

		maxX = Mathf.Max (maxX, x);
		maxY = Mathf.Max (maxY, y);
		minX = Mathf.Min (minX, x);
		minY = Mathf.Min (minY, y);

		count += 1;
	}

	public Tile GetTile(int x, int y)
	{
		if (!plan.ContainsKey(x))
			return null;
		else
			return (plan[x].ContainsKey(y)) ? plan[x][y] : null;
	}

	public bool HasNeighbor(Tile tile, Direction dir)
	{
		Tile neighbor = null;
		switch(dir)
		{
			case Direction.North:
				neighbor = GetTile(	tile.HorizontalIndex, 
			                   		tile.VerticalIndex + 1);
			break;

			case Direction.East:
				neighbor = GetTile(	tile.HorizontalIndex + 1, 
				                   	tile.VerticalIndex);
			break;

			case Direction.South:
				neighbor = GetTile(	tile.HorizontalIndex, 
				                   	tile.VerticalIndex - 1);
			break;

			case Direction.West:
				neighbor = GetTile(	tile.HorizontalIndex - 1, 
			                   		tile.VerticalIndex);
			break;

			default:
			break;
		}

		return (neighbor != null);
	}

	public Iterator GetIterator()
	{
		return new Iterator(this);
	}

	// Properties
	private  Tile First { get { return firstTile; } }
	public int Count { get { return count; } }
	public Vector2 Min { get { return new Vector2 (minX, minY); } }
	public Vector2 Max { get { return new Vector2 (maxX, maxY); } }
	public Vector2 Bounds { get { return new Vector2 (maxX - minX, maxY - minY); } }



	/// ---- Public Internal/Nested Classes --- ///


	// Iterates over a floor plan using 
	// breadth first search
	public class Iterator
	{
		HashSet<Tile> visited;
		Queue<Tile> queue;
		Floorplan plan;
		
		public Iterator(Floorplan plan)
		{
			this.plan = plan;
			queue = new Queue<Tile>();
			visited = new HashSet<Tile>();

			// Initialize the search
			queue.Enqueue (plan.First);
			visited.Add(plan.First);
		}
		
		public Tile NextTile()
		{
			Tile tile = (queue.Count > 0) ? queue.Dequeue() : null;

			if(tile != null)
			{
				int x = tile.HorizontalIndex;
				int y = tile.VerticalIndex;

				checkAndEnqueue(x - 1, y);
				checkAndEnqueue(x + 1, y);
				checkAndEnqueue(x, y - 1);
				checkAndEnqueue(x, y + 1);
			}

			return tile;
		}

		private void checkAndEnqueue(int x, int y)
		{
			Tile tile = plan.GetTile(x, y);
			if(tile != null && !visited.Contains(tile))
			{
				queue.Enqueue(tile);
				visited.Add(tile);
			}
		}
	}
	
	// Identifieds a tile in the floor plan
	public class Tile
	{
		private int xIndex;
		private int yIndex;
		
		public int HorizontalIndex { get { return xIndex; } }
		public int VerticalIndex { get { return yIndex; } }
		
		public Tile(int x, int y)
		{
			this.xIndex = x;
			this.yIndex = y;
		}
	}
}


