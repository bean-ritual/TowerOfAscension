using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LevelMap : Map.BaseMap{
	private static readonly Vector3 _LEVEL_ORIGIN_POSITION = new Vector3(0,0);
	private static readonly Vector3 _LEVEL_CELL_DIMENSIONS = new Vector3(1,1);
	private const float _LEVEL_CELL_SIZE = 1f;
	private const float _LEVEL_CELL_OFFSET = 0.5f;
	public LevelMap(int width, int height) : base(
		width, 
		height,
		_LEVEL_CELL_SIZE,
		_LEVEL_CELL_OFFSET,
		_LEVEL_ORIGIN_POSITION,
		_LEVEL_CELL_DIMENSIONS,
		CreateTile
	){}
	private static Map.Tile CreateTile(int x, int y){
		return new DataTile(x, y);
	}
}
