using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexTile : MonoBehaviour {

	public static readonly string DEFAULT_NAME = "hexagon";
	public static readonly string DEFAULT_DELIMITER = ",";

	public int x;
	public int y;
	public bool isTraversable = true;

	public HexTile[] GetWalkableTilesByRadius(int radius){
		ArrayList tiles = new ArrayList();
		

		HexTile rightTile =  HexTile.FindByCoordinate(this.x + radius, this.y);
		HexTile leftTile = HexTile.FindByCoordinate(this.x - radius, this.y);
		HexTile topRight;
		HexTile topLeft;
		HexTile bottomRight;
		HexTile bottomLeft;

		if(this.y % 2 == 0){
			topRight = HexTile.FindByCoordinate(this.x, this.y + radius);
			topLeft = HexTile.FindByCoordinate(this.x - radius, this.y + radius);
			bottomRight = HexTile.FindByCoordinate(this.x, this.y - radius);
			bottomLeft = HexTile.FindByCoordinate(this.x - radius, this.y - radius);
		}else{
			topRight = HexTile.FindByCoordinate(this.x + radius, this.y + radius);
			topLeft = HexTile.FindByCoordinate(this.x, this.y + radius);
			bottomRight = HexTile.FindByCoordinate(this.x + radius, this.y - radius);
			bottomLeft = HexTile.FindByCoordinate(this.x, this.y - radius);
		}

		if(rightTile != null && rightTile.isTraversable) tiles.Add(rightTile);
		if(leftTile != null && leftTile.isTraversable) tiles.Add(leftTile);
		if(topRight != null && topRight.isTraversable) tiles.Add(topRight);
		if(topLeft != null && topLeft.isTraversable) tiles.Add(topLeft);
		if(bottomRight != null && bottomRight.isTraversable) tiles.Add(bottomRight);
		if(bottomLeft != null && bottomLeft.isTraversable) tiles.Add(bottomLeft);

		return tiles.ToArray(typeof(HexTile)) as HexTile[];
	}

	public HexTile[] GetNeighborsInRange(int radius){
		HashSet<HexTile> tiles = new HashSet<HexTile>();

		for(int dx = -radius; dx <= radius; ++dx){
			for(int dy = Mathf.Max(-radius, -dx - radius); dy <= Mathf.Min(radius, -dx + radius); ++dy){
				int dz = -dx - dy;

				var center = new OffsetCoordinate(this.x, this.y);
				var offsetCube = new CubeCoordinate(dx, dy, dz);
				tiles.Add(HexTile.FindByOffset(center, HexUtils.CubeCoordToOffsetCoord(offsetCube, center.IsEven())));
			}
		}

		HexTile[] results = new HexTile[tiles.Count];
		tiles.CopyTo(results);
		return results;
	}

	/// returns null if not found
	public static HexTile FindByCoordinate(int x, int y){
		string name = HexTile.DEFAULT_NAME + x + HexTile.DEFAULT_DELIMITER + y;
		var tile = GameObject.Find(name);
		
		return tile != null ? tile.GetComponent<HexTile>() : null;
	}

	public static HexTile FindByOffset(OffsetCoordinate center, OffsetCoordinate offset){
		int x = center.x + offset.x;
		int y = center.y + offset.y;
		return FindByCoordinate(x, y);
	}

	public void SetColor(UnityEngine.Color color){
		GetComponentInChildren<MeshRenderer>().material.color = color;
	}

	public void HighlightWalkableTiles(int radius)
    {
		HexTile[] hexTiles = GetNeighborsInRange(radius);
        foreach(HexTile tile in hexTiles){
			tile.SetColor(Color.green);
		}
    }
}
