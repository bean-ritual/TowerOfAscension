using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class LevelUnit : 
	Unit,
	WorldUnit.IWorldUnit,
	Unit.ISpawnable,
	Unit.IProcessable,
	Unit.ICollideable
	{
	[field:NonSerialized]public event EventHandler<EventArgs> OnWorldUnitUpdate;
	protected SpriteSheet.SpriteID _spriteID = SpriteSheet.SpriteID.Misc;
	protected int _spriteIndex = 0;
	protected int _sortingOrder = 10;
	protected int _x = Unit.NullUnit.GetNullX();
	protected int _y = Unit.NullUnit.GetNullY();
	protected Register<Unit>.ID _id = Register<Unit>.ID.GetNullID();
	public LevelUnit(){}
	public void SetSpriteID(SpriteSheet.SpriteID spriteID){
		_spriteID = spriteID;
		OnWorldUnitUpdate?.Invoke(this, EventArgs.Empty);
	}
	public void SetSpriteIndex(int spriteIndex){
		_spriteIndex = spriteIndex;
		OnWorldUnitUpdate?.Invoke(this, EventArgs.Empty);
	}
	public Sprite GetSprite(){
		return SpriteSheet.SPRITESHEET_DATA.GetSprite(_spriteID, _spriteIndex);
	}
	public void SetSortingOrder(int sortingOrder){
		_sortingOrder = sortingOrder;
		OnWorldUnitUpdate?.Invoke(this, EventArgs.Empty);
	}
	public int GetSortingOrder(){
		return _sortingOrder;
	}
	public virtual bool GetWorldVisibility(Level level){
		return level.Get(_x, _y).GetLightable().GetLight() > 0;
	}
	public void Spawn(Level level, int x, int y){
		Unit.Default_Spawn(this, level, x, y);
	}
	public void Despawn(Level level){
		Unit.Default_Despawn(this, level);
	}
	public void SetPosition(Level level, int x, int y){
		Unit.Default_SetPosition(this, level, x, y, ref _x, ref _y);
		OnWorldUnitUpdate?.Invoke(this, EventArgs.Empty);
	}
	public void GetPosition(out int x, out int y){
		x = _x;
		y = _y;
	}
	public void RemovePosition(Level level){
		Unit.Default_RemovePosition(this, level, _x, _y);
	}
	public Vector3 GetPosition(GridMap<Tile> map){
		return map.GetWorldPosition(_x, _y);
	}
	public Tile GetTile(Level level){
		return level.Get(_x, _y);
	}
	public void AddToRegister(Register<Unit> register){
		register.Add(this, ref _id);
	}
	public void RemoveFromRegister(Register<Unit> register){
		register.Remove(_id);
	}
	public Register<Unit>.ID GetID(){
		return _id;
	}
	public virtual bool Process(Level level){
		return level.NextTurn();
	}
	public virtual bool CheckCollision(Level level, Unit check){
		return true;
	}
	public override WorldUnit.IWorldUnit GetWorldUnit(){
		return this;
	}
	public override Unit.ISpawnable GetSpawnable(){
		return this;
	}
	public override Unit.IPositionable GetPositionable(){
		return this;
	}
	public override Register<Unit>.IRegisterable GetRegisterable(){
		return this;
	}
	public override IProcessable GetProcessable(){
		return this;
	}
	public override Unit.ICollideable GetCollideable(){
		return this;
	}
}
