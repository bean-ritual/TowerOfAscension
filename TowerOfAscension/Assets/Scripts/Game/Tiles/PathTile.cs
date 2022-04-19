using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PathTile : 
	Tile,
	LevelMeshManager.ITileMeshData,
	LightMeshManager.ILightMeshData,
	Tile.IWalkable,
	Tile.IAttackable,
	Tile.IPrintable,
	Tile.IConnectable,
	Tile.ILightable,
	Level.ILightControl,
	Tile.IDiscoverable,
	Tile.IInteractable,
	Tile.IHostileTarget
	{
	[field:NonSerialized]public event EventHandler<Register<Unit>.OnObjectChangedEventArgs> OnUnitAdded;
	[field:NonSerialized]public event EventHandler<Register<Unit>.OnObjectChangedEventArgs> OnUnitRemoved;
	private int _light;
	private bool _discovered;
	private List<Register<Unit>.ID> _ids;
	public PathTile(int x, int y) : base(x, y){
		_light = 0;
		_discovered = false;
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
	public int GetLightAtlasIndex(){
		const int DARKNESS = 0;
		const int SHADE = 1;
		if(_discovered){
			return SHADE;
		}
		return DARKNESS;
	}
	public int GetLightUVFactor(){
		const int LIT_FACTOR = 0;
		const int UNLIT_FACTOR = 1;
		if(_light > 0){
			return LIT_FACTOR;
		}
		return UNLIT_FACTOR;
	}
	public void AddUnit(Game game, Register<Unit>.ID id){
		_ids.Add(id);
		OnUnitAdded?.Invoke(this, new Register<Unit>.OnObjectChangedEventArgs(id, game.GetLevel().GetUnits().Get(id)));
	}
	public bool RemoveUnit(Game game, Register<Unit>.ID id){
		if(_ids.Remove(id)){
			OnUnitRemoved?.Invoke(this, new Register<Unit>.OnObjectChangedEventArgs(id, game.GetLevel().GetUnits().Get(id)));
			return true;
		}
		return false;
	}
	public void DoUnits(Game game, Func<Tile, Unit, bool> DoUnit){
		Inventory inventory = game.GetLevel().GetUnits();
		for(int i = 0; i < _ids.Count; i++){
			DoUnit(this, inventory.Get(_ids[i]));
		}
	}
	public List<Unit> GetUnits(Game game){
		return game.GetLevel().GetUnits().GetMultiple(_ids);
	}
	public void Walk(Game game, Unit unit){
		List<Unit> units = game.GetLevel().GetUnits().GetMultiple(_ids);
		for(int i = 0; i < units.Count; i++){
			units[i].GetTaggable().GetTag(game, Tag.ID.Tripwire).GetIInputUnit().Input(game, units[i], unit);
		}
	}
	public bool CanWalk(Game game, Unit unit){
		List<Unit> units = game.GetLevel().GetUnits().GetMultiple(_ids);
		Tag.Collider collider = unit.GetTaggable().GetTag(game, Tag.ID.Collision).GetIGetCollider().GetCollider(game, unit); 
		for(int i = 0; i < units.Count; i++){
			if(units[i].GetTaggable().GetTag(game, Tag.ID.Collision).GetIConditionCollider().Check(game, units[i], collider)){
				return false;
			}
		}
		return true;
	}
	public void Attack(Game game, Unit skills, Unit attack){}
	public void Print(Game game, Unit master, BluePrint.Print print, Tile tile, int x, int y){}
	public bool CanPrint(Game game, Unit master, int x, int y){
		return false;
	}
	public bool CanConnect(Game game, Unit master, int x, int y){
		return true;
	}
	public void SetLight(int light){
		_light = light;
	}
	public int GetLight(){
		return _light;
	}
	public bool CheckTransparency(Game game){
		List<Unit> units = game.GetLevel().GetUnits().GetMultiple(_ids);
		for(int i = 0; i < units.Count; i++){
			if(units[i].GetTaggable().GetTag(game, Tag.ID.Opacity).GetICondition().Check(game, units[i])){
				return false;
			}
		}
		return true;
	}
	public void Discover(Game game, Unit unit){
		_discovered = true;
	}
	public void Interact(Game game, Unit unit){
		List<Unit> units = game.GetLevel().GetUnits().GetMultiple(_ids);
		for(int i = 0; i < units.Count; i++){
			units[i].GetTaggable().GetTag(game, Tag.ID.Interactable).GetIInputUnit().Input(game, units[i], unit);
		}
	}
	public bool CheckHostility(Game game, Unit unit){
		List<Unit> units = game.GetLevel().GetUnits().GetMultiple(_ids);
		for(int i = 0; i < units.Count; i++){
			if(units[i].GetTaggable().GetTag(game, Tag.ID.Hostility).GetICondition().Check(game, units[i])){
				return true;
			}
		}
		return false;
	}
	public override LevelMeshManager.ITileMeshData GetTileMeshData(){
		return this;
	}
	public override LightMeshManager.ILightMeshData GetLightMeshData(){
		return this;
	}
	public override Tile.IWalkable GetWalkable(){
		return this;
	}
	public override Tile.IAttackable GetAttackable(){
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
	public override Tile.ILightable GetLightable(){
		return this;
	}
	public override Level.ILightControl GetLightControl(){
		return this;
	}
	public override Tile.IDiscoverable GetDiscoverable(){
		return this;
	}
	public override Tile.IInteractable GetInteractable(){
		return this;
	}
	public override IHostileTarget GetHostileTarget(){
		return this;
	}
}
