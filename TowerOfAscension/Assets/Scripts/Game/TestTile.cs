using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TestTile : 
	Map.Tile.BaseTile,
	TestTile.ITestTile,
	MapMeshManager.ITileMeshData
	{
	public interface ITestTile{
		void SetPath(Map map, bool path);
		bool GetPath();
		void SetAltasIndex(Map map, int index);
	}
	private int _index;
	private bool _path;
	public TestTile(int x, int y) : base(x, y){
		_index = 1;
		_path = true;
	}
	public void SetPath(Map map, bool path){
		_path = path;
		FireTileUpdateEvent(map);
	}
	public bool GetPath(){
		return _path;
	}
	public void SetAltasIndex(Map map, int index){
		_index = index;
		FireTileUpdateEvent(map);
	}
	public int GetAtlasIndex(Game game){
		if(_path){
			return _index;
		}else{
			return 3;
		}
	}
	public int GetUVFactor(Game game){
		return 1;
	}
	public override TestTile.ITestTile GetITestTile(){
		return this;
	}
	public override MapMeshManager.ITileMeshData GetITileMeshData(){
		return this;
	}
}

