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
        GridController.ResetAllTiles();
    }

    private void OnTileClickedDown(HexTile hexTile)
    {
        //TODO implement actions
		//Debug.Log("Tile Clicked: " + hexTile.transform.name);
		hexTile.HighlightWalkableTiles(1);
    }

    private void OnTileHover(HexTile hexTile)
    {
        //TODO implement actions
		//Debug.Log("Tile Hover: " + hexTile.transform.name);
    }

}
