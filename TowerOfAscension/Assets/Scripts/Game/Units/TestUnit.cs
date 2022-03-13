using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TestUnit : Unit, Unit.ISpawnable{
	private int _x;
	private int _y;
	private Register<Unit>.ID _id;
	public void Spawn(Level level, int x, int y){
		Unit.Default_Spawn(this, level, x, y);
	}
	public void Despawn(Level level){
		
	}
	public void SetPosition(Level level, int x, int y){
		Unit.Default_SetPosition(this, level, x, y, ref _x, ref _y);
	}
	public void GetPosition(out int x, out int y){
		x = _x;
		y = _y;
	}
	public void AddToRegister(Register<Unit> register){
		register.Add(this, ref _id);
	}
	public Register<Unit>.ID GetID(){
		return _id;
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
