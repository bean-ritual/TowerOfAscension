using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Stairs :
	Unit,
	WorldUnit.IWorldUnit,
	Unit.ISpawnable
	{
    public event EventHandler<EventArgs> OnWorldUnitUpdate;
	private int _x = Unit.NullUnit.GetNullX();
	private int _y = Unit.NullUnit.GetNullY();
	private Register<Unit>.ID _id = Register<Unit>.ID.GetNullID();
	public Stairs(){}
	public Sprite GetSprite(){
		return SpriteSheet.SPRITESHEET_DATA.GetSprite(SpriteSheet.SpriteID.Stairs, 0);
	}
	public int GetSortingOrder(){
		return 10;
	}
	public void Spawn(Level level, int x, int y){
		Unit.Default_Spawn(this, level, x, y);
	}
	public void Despawn(Level level){
		
	}
	public void SetPosition(Level level, int x, int y){
		Unit.Default_SetPosition(this, level, x, y, ref _x, ref _y);
		OnWorldUnitUpdate?.Invoke(this, EventArgs.Empty);
	}
	public void GetPosition(out int x, out int y){
		x = _x;
		y = _y;
	}
	public Vector3 GetPosition(GridMap<Tile> map){
		return map.GetWorldPosition(_x, _y);
	}
	public void AddToRegister(Register<Unit> register){
		register.Add(this, ref _id);
	}
	public Register<Unit>.ID GetID(){
		return _id;
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
}