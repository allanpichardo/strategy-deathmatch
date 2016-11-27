using UnityEngine;
using System.Collections;
using System;

public class MouseController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if(Physics.Raycast(ray, out hitInfo)){
			HexTile hexTile = hitInfo.collider.gameObject.
									  transform.parent.GetComponent<HexTile>();

			OnTileHover(hexTile);

			if(Input.GetMouseButtonDown(0)){			
				OnTileClickedDown(hexTile);
			}
			if(Input.GetMouseButtonUp(0)){
				OnTileclickedUp(hexTile);
			}
		}
	}

    private void OnTileclickedUp(HexTile hexTile)
    {
		Debug.Log("mouse up");
        ResetAllTiles();
    }

    private void ResetAllTiles()
    {
        GameObject grid = GameObject.Find(GridController.BASE_NAME);
		if(grid != null){
			MeshRenderer[] renderers = grid.GetComponentsInChildren<MeshRenderer>();
			foreach(MeshRenderer renderer in renderers){
				renderer.material.color = Color.white;
			}
		}
    }

    private void OnTileClickedDown(HexTile hexTile)
    {
        //TODO implement actions
		//Debug.Log("Tile Clicked: " + hexTile.transform.name);
		HexTile[] tiles = hexTile.GetWalkableTilesFromRadius(1);
        HighlightWalkableTiles(ref tiles);
    }

    private void OnTileHover(HexTile hexTile)
    {
        //TODO implement actions
		//Debug.Log("Tile Hover: " + hexTile.transform.name);
    }

    private void HighlightWalkableTiles(ref HexTile[] hexTiles)
    {
        foreach(HexTile tile in hexTiles){
			ColorTile(tile, Color.green);
		}
    }

	private void ColorTile(HexTile tile, UnityEngine.Color color){
		tile.GetComponentInChildren<MeshRenderer>().material.color = color;
	}
}
