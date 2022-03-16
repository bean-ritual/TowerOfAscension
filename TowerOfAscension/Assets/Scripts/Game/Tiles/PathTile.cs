using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PathTile : 
	Tile,
	LevelMeshManager.ITileMeshData,
	Tile.IPrintable,
	Tile.IConnectable,
	Tile.IWalkable
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
	public void Print(Level level, Unit master, BluePrint.Print print, Tile tile, int x, int y){}
	public bool CanPrint(Level level, Unit master, int x, int y){
		return false;
	}
	public bool CanConnect(Level level, Unit master, int x, int y){
		return true;
	}
	public void Walk(Level level, Unit unit){
		if(CanWalk(level, unit)){
			unit.GetMoveable().OnMove(level, this);
		}
	}
	public bool CanWalk(Level level, Unit unit){
		return false;
	}
	public override LevelMeshManager.ITileMeshData GetTileMeshData(){
		return this;
	}
	public override Tile.IPrintable GetPrintable(){
		return this;
	}
	public override Tile.IConnectable GetConnectable(){
		return this;
	}
}
