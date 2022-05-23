using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TestMap : Map.BaseMap{
	private static readonly Vector3 _TEST_ORIGIN_POSITION = new Vector3(0,0);
	private static readonly Vector3 _TEST_CELL_DIMENSIONS = new Vector3(1,1);
	private const float _TEST_CELL_SIZE = 1f;
	private const float _TEST_CELL_OFFSET = 0.5f;
	public TestMap(int width, int height) : base(
		width, 
		height,
		_TEST_CELL_SIZE,
		_TEST_CELL_OFFSET,
		_TEST_ORIGIN_POSITION,
		_TEST_CELL_DIMENSIONS,
		CreateTile
	){}
	private static Map.Tile CreateTile(int x, int y){
		return new TestTile(x, y);
	}
}
