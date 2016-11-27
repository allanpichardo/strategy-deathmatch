using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexTile : MonoBehaviour {

	public static readonly string DEFAULT_NAME = "hexagon";
	public static readonly string DEFAULT_DELIMITER = ",";

	public int x;
	public int y;
	public bool isTraversable = true;

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
        foreach(List<HexTile> tiles in GetReachableTiles(radius)){
			foreach(HexTile tile in tiles){
				tile.SetColor(Color.green);
			}
		}
    }

	//TODO: convert this to a Set or 1-dimensional collection
	public List<List<HexTile>> GetReachableTiles(int movements){
		HashSet<HexTile> visited = new HashSet<HexTile>();
		visited.Add(this);
		List<List<HexTile>> fringes = new List<List<HexTile>>();
		List<HexTile> start = new List<HexTile>();
		start.Add(this);
		fringes.Add(start);

		for(int k = 1; k <= movements; ++k){
			fringes.Add(new List<HexTile>());
			foreach(HexTile tile in fringes[k-1]){
				foreach(HexTile neighbor in tile.GetNeighborsInRange(1)){
					if(!visited.Contains(neighbor) && neighbor.isTraversable){
						visited.Add(neighbor);
						fringes[k].Add(neighbor);
					}
				}
			}
		}

		return fringes;
	}
}
