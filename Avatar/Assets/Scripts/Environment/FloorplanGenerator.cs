using UnityEngine;
using System.Collections;

public class FloorplanGenerator{

	private int maxWidth;
	private int maxHeight;
	private System.Random rand;

	public FloorplanGenerator(int maxWidth, int maxHeight)
	{
		rand = new System.Random();
		setBounds (maxWidth, maxHeight);
	}

	public void setBounds(int width, int height)
	{
		this.maxWidth = width;
		this.maxHeight = height;
	}

	public Floorplan generatePlan()
	{
		Floorplan plan = new Floorplan();
		int x = 0;
		int y = 0;

		while(x < maxWidth && y < maxHeight)
		{
			plan.AddTile(x, y);

			int dir = rand.Next(4);
			switch(dir)
			{
				case 0:
					y += 1;
					break;

				case 1:
					x += 1;
					break;

				case 2:
					y -= 1;
					break;

				case 3:
					x -= 1;
					break;

				default:
					break;
			}


			x = Mathf.Max (0, x);
			y = Mathf.Max(0, y);
		}

		return plan;
	}
}
