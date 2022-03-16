using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PathTile : 
	Tile,
	LevelMeshManager.ITileMeshData,
	Tile.IWalkable,
	Tile.IPrintable,
	Tile.IConnectable
	{
	private List<Register<Unit>.ID> _ids;
	public PathTile(int x, int y) : base(x, y){
		_ids = new List<Register<Unit>.ID>();
	}
	public int GetAtlasIndex(){
		const int PATH_INDEX = 1;
		return PATH_INDEX;
	}
	public int GetUVFactor(){
		const int PATH_FACTOR = 1;
		return PATH_FACTOR;
	}
	public void AddUnit(Register<Unit>.ID id){
		_ids.Add(id);
	}
	public bool RemoveUnit(Register<Unit>.ID id){
		return _ids.Remove(id);
	}
	public void Walk(Level level, Unit unit){
		if(CanWalk(level, unit)){
			unit.GetMoveable().OnMove(level, this);
		}
	}
	public bool CanWalk(Level level, Unit unit){
		List<Unit> units = level.GetUnits().GetMultiple(_ids);
		for(int i = 0; i < units.Count; i++){
			if(units[i].GetCollideable().CheckCollision(level, unit)){
				return false;
			}
		}
		return true;
	}
	public void Print(Level level, Unit master, BluePrint.Print print, Tile tile, int x, int y){}
	public bool CanPrint(Level level, Unit master, int x, int y){
		return false;
	}
	public bool CanConnect(Level level, Unit master, int x, int y){
		return true;
	}
	public override LevelMeshManager.ITileMeshData GetTileMeshData(){
		return this;
	}
	public override Tile.IWalkable GetWalkable(){
		return this;
	}
	public override Tile.IHasUnits GetHasUnits(){
		return this;
	}
	public override Tile.IPrintable GetPrintable(){
		return this;
	}
	public override Tile.IConnectable GetConnectable(){
		return this;
	}
}
