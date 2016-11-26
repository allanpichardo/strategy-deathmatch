﻿using UnityEngine;
using System.Collections;
using System;

public class GridController : MonoBehaviour {

	public Transform hexPrefab;

	public int gridWidth = 11;
	public int gridHeight = 11;

	private float hexWidth = 25.0f;
	private float hexHeight = 25.0f;
	public float gap = 0.0f;

	public Vector3 startPosition;


	// Use this for initialization
	void Start () {
		AddGapOffset();
		CalculateStartPosition();
		CreateGrid();
	}

	private void AddGapOffset(){
		hexWidth += hexWidth * gap;
		hexHeight += hexHeight * gap;
	}

	private void CalculateStartPosition(){
		float offset = 0;
		if(gridHeight / 2 % 2 != 0){
			offset = gridWidth / 2;
		}

		float x = -hexWidth * (gridWidth / 2) - offset;
		float z = hexHeight * 0.75f * (gridHeight / 2);

		startPosition = new Vector3(x,0,z);
	}

	private void CreateGrid(){
		for(int y = 0; y < gridHeight; ++y){
			for(int x = 0; x < gridWidth; ++x){
				Transform hex = Instantiate(hexPrefab) as Transform;
				Vector2 gridPosition = new Vector2(x,y);
				
				hex.position = CalculateWorldPosition(gridPosition);
				hex.parent = this.transform;
				hex.name = "hexagon" + x + "|" + y;

			}
		}
	}

    private Vector3 CalculateWorldPosition(Vector2 gridPosition)
    {
        float offset = 0.0f;
		if(gridPosition.y % 2 != 0){
			offset = hexWidth / 2;
		}

		float x = startPosition.x + gridPosition.x * hexWidth + offset;
		float z = startPosition.z + gridPosition.y * hexHeight * 0.75f;

		return new Vector3(x,0,z);
    }


    // Update is called once per frame
    void Update () {
	
	}
}