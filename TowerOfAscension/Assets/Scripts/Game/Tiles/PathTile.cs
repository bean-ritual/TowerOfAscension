using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PathTile : 
	Tile,
	LevelMeshManager.ITileMeshData,
	Tile.IPrintable
	{
	public PathTile(int x, int y) : base(x, y){}
	public int GetAtlasIndex(){
		const int PATH_INDEX = 1;
		return PATH_INDEX;
	}
	public int GetUVFactor(){
		const int PATH_FACTOR = 1;
		return PATH_FACTOR;
	}
	public void Print(Level level, ClassicGen master, BluePrint.Print print, Tile tile){}
	public bool Check(Level level, ClassicGen master){
		return false;
	}
	public override LevelMeshManager.ITileMeshData GetTileMeshData(){
		return this;
	}
	public override Tile.IPrintable GetPrintable(){
		return this;
	}
}
