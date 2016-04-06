using UnityEngine;
using System.Collections;

public class RoomGenerator : MonoBehaviour {

	private FloorplanGenerator planGenerator;
	private Vector3 floorBounds;
	private Vector3 wallBounds;
	private Vector3 cornerBounds;

	public bool GenerateOnPlay = false;
	public int MaxWidth = 5;
	public int MaxHeight = 5;

	public GameObject Floor;
	public GameObject Wall;
	public GameObject Corner;
    public GameObject Obstacle;

	// Use this for initialization
	void Start () {
		planGenerator = new FloorplanGenerator(MaxWidth, MaxHeight);

		if(GenerateOnPlay)
			GenerateRoom();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GenerateRoom()
	{
		if (planGenerator == null)
			return;

		ClearRoom();
		RefreshBounds();

		Floorplan plan = planGenerator.generatePlan();
		CreateRoom(plan);
	}

	private void RefreshBounds()
	{
		floorBounds = Floor.GetComponent<Renderer>().bounds.size;
		wallBounds = Wall.GetComponent<Renderer>().bounds.size;
		cornerBounds = Corner.GetComponent<Renderer>().bounds.size;
	}

	private void ClearRoom()
	{
		planGenerator.setBounds (MaxWidth, MaxHeight);
		foreach (Transform childTransform in gameObject.transform) 
			Destroy(childTransform.gameObject);
	}

	private void CreateRoom(Floorplan floorPlan)
	{
		Floorplan.Iterator iterator = floorPlan.GetIterator();
		Floorplan.Tile tile = iterator.NextTile();

		while(tile != null)
		{
			CreateFloor(tile);
			CreateWalls(floorPlan, tile);
			CreateCorners(floorPlan, tile);

			// Iterate to the next
			tile = iterator.NextTile();
		}
	}

	private void CreateFloor(Floorplan.Tile tile)
	{
		Vector3 position = new Vector3(	(tile.HorizontalIndex * floorBounds.x), 
		                               	(tile.VerticalIndex * floorBounds.y), 0);
		
		// Create the new floor tile
		GameObject floorTile = (GameObject)Instantiate(Floor, position, Quaternion.identity);
		floorTile.transform.parent = gameObject.transform;

        // Create an obstacle in the middle of the floor
        GameObject obstacle = (GameObject)Instantiate(Obstacle, position, Quaternion.identity);
        obstacle.transform.parent = gameObject.transform;
    }

	private void CreateWalls(Floorplan plan, Floorplan.Tile tile)
	{
		// Get the tile directions
		bool tileNorth = plan.HasNeighbor(tile, Floorplan.Direction.North);
		bool tileEast = plan.HasNeighbor(tile, Floorplan.Direction.East);
		bool tileSouth = plan.HasNeighbor(tile, Floorplan.Direction.South);
		bool tileWest = plan.HasNeighbor(tile, Floorplan.Direction.West);


		Vector3 offsetHoriz = new Vector3((floorBounds.x + wallBounds.y) / 2.0f, 0, 0);
		Vector3 offsetVert = new Vector3(0, (floorBounds.y + wallBounds.y) / 2.0f, 0);
		Vector3 tilePosition = new Vector3(	(tile.HorizontalIndex * floorBounds.x), 
		                                   (tile.VerticalIndex * floorBounds.y), 0);

		// Add the walls in each direction
		if(!tileNorth)
			CreateChild(Wall, tilePosition + offsetVert, Quaternion.AngleAxis(90, Vector3.forward));

		if(!tileEast)
			CreateChild(Wall, tilePosition + offsetHoriz, Quaternion.AngleAxis(0, Vector3.forward));

		if(!tileSouth)
			CreateChild(Wall, tilePosition - offsetVert, Quaternion.AngleAxis(90, Vector3.forward));

		if(!tileWest)
			CreateChild(Wall, tilePosition - offsetHoriz, Quaternion.AngleAxis(0, Vector3.forward));
	}

	private void CreateCorners(Floorplan plan, Floorplan.Tile tile)
	{
		// Get the tile directions
		bool tileNorth = plan.HasNeighbor(tile, Floorplan.Direction.North);
		bool tileEast = plan.HasNeighbor(tile, Floorplan.Direction.East);
		bool tileSouth = plan.HasNeighbor(tile, Floorplan.Direction.South);
		bool tileWest = plan.HasNeighbor(tile, Floorplan.Direction.West);
		
		
		Vector3 offsetHoriz = new Vector3((floorBounds.x + cornerBounds.x) / 2.0f, 0, 0);
		Vector3 offsetVert = new Vector3(0, (floorBounds.y + cornerBounds.y) / 2.0f, 0);
		Vector3 tilePosition = new Vector3(	(tile.HorizontalIndex * floorBounds.x), 
		                                   (tile.VerticalIndex * floorBounds.y), 0);

		// Add the corners in each direction
		if(!tileNorth && !tileEast)
			CreateChild(Corner, tilePosition + (offsetVert + offsetHoriz), Quaternion.identity);
		
		if(!tileSouth && !tileEast)
			CreateChild(Corner, tilePosition - offsetVert + offsetHoriz, Quaternion.identity);

		if(!tileSouth && !tileWest)
			CreateChild(Corner, tilePosition - (offsetVert + offsetHoriz), Quaternion.identity);
		
		if(!tileNorth && !tileWest)
			CreateChild(Corner, tilePosition + offsetVert - offsetHoriz, Quaternion.identity);
	}



	public void CreateChild(GameObject obj, Vector3 position, Quaternion rotation)
	{
		GameObject newObj = (GameObject)Instantiate(obj, position, rotation);
		newObj.transform.parent = gameObject.transform;
	}

}



