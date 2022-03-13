using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Unit{
	public interface IProcessable{
		bool Process(Level level);
	}
	public interface IPositionable{
		void SetPosition(Level level, int x, int y);
		void GetPosition(out int x, out int y);
	}
	public interface IDirectionable{
		void SetDirection(Direction direction);
		Direction GetDirection();
	}
	public interface ISpawnable : IPositionable, Register<Unit>.IRegisterable{
		void Spawn(Level level, int x, int y);
		void Despawn(Level level);
	}
	public interface ILightControl{
		bool GetTransparent();
	}
	[Serializable]
	public class NullUnit : 
		Unit,
		Register<Unit>.IRegisterable,
		IProcessable,
		IPositionable,
		IDirectionable,
		ISpawnable,
		ILightControl
		{
		private const int _NULL_X = -1;
		private const int _NULL_Y = -1;
		public NullUnit(){}
		public void AddToRegister(Register<Unit> register){}
		public Register<Unit>.ID GetID(){
			return Register<Unit>.ID.GetNullID();
		}
		public bool Process(Level level){
			return level.NextTurn();
			//const bool NULL_PROCESS = true;
			//return NULL_PROCESS;
		}
		public void SetPosition(Level level, int x, int y){}
		public void GetPosition(out int x, out int y){
			x = _NULL_X;
			y = _NULL_Y;
		}
		public void SetDirection(Direction direction){}
		public Direction GetDirection(){
			const Direction NULL_DIRECTION = Direction.Null;
			return NULL_DIRECTION;
		}
		public void Spawn(Level level, int x, int y){}
		public void Despawn(Level level){}
		public bool GetTransparent(){
			const bool NULL_TRANSPARENCY = true;
			return NULL_TRANSPARENCY;
		}
	}
	public enum Direction{
		Null,
		North,
		South,
		East,
		West,
		North_East,
		North_West,
		South_East,
		South_West,
	};
	[field:NonSerialized]private static readonly NullUnit _NULL_UNIT = new NullUnit();
	public Unit(){}
	public virtual Register<Unit>.IRegisterable GetRegisterable(){
		return _NULL_UNIT;
	}
	public virtual IProcessable GetProcessable(){
		return _NULL_UNIT;
	}
	public virtual IPositionable GetPositionable(){
		return _NULL_UNIT;
	}
	public virtual IDirectionable GetDirectionable(){
		return _NULL_UNIT;
	}
	public virtual ISpawnable GetSpawnable(){
		return _NULL_UNIT;
	}
	public virtual ILightControl GetLightControl(){
		return _NULL_UNIT;
	}
	public static void Default_Spawn(Unit self, Level level, int x, int y){
		self.GetRegisterable().AddToRegister(level.GetUnits());
		self.GetPositionable().SetPosition(level, x, y);
	}
	public static void Default_SetPosition(Unit self, Level level, int newX, int newY, ref int x, ref int y){
		self.GetPositionable().GetPosition(out int oldX, out int oldY);
		level.Get(oldX, oldY).RemoveUnit(self.GetRegisterable().GetID());
		level.Get(newX, newY).AddUnit(self.GetRegisterable().GetID());
		x = newX;
		y = newY;
	}
	public static Unit GetNullUnit(){
		return _NULL_UNIT;
	}
	public static bool DirectionToInt(Direction direction, out int x, out int y){
		switch(direction){
			default:{
				x = 0;
				y = 0;
				return false;
			}
			case Direction.Null:{
				x = 0;
				y = 0;
				return false;
			}
			case Direction.North:{
				x = 0;
				y = 1;
				return true;
			}
			case Direction.South:{
				x = 0;
				y = -1;
				return true;
			}
			case Direction.East:{
				x = 1;
				y = 0;
				return true;
			}
			case Direction.West:{
				x = -1;
				y = 0;
				return true;
			}
			case Direction.North_East:{
				x = 1;
				y = 1;
				return true;
			}
			case Direction.North_West:{
				x = -1;
				y = 1;
				return true;
			}
			case Direction.South_East:{
				x = 1;
				y = -1;
				return true;
			}
			case Direction.South_West:{
				x = -1;
				y = -1;
				return true;
			}
		}
	}
	public static Direction IntToDirection(int x, int y){
		if(y > 0){
			if(x > 0){
				return Direction.North_East;
			}
			if(x < 0){
				return Direction.North_West;
			}
			return Direction.North;
		}
		if(y < 0){
			if(x > 0){
				return Direction.South_East;
			}
			if(x < 0){
				return Direction.South_West;
			}
			return Direction.South;
		}
		if(x > 0){
			if(y > 0){
				return Direction.North_East;
			}
			if(y < 0){
				return Direction.South_East;
			}
			return Direction.East;
		}
		if(x < 0){
			if(y > 0){
				return Direction.North_West;
			}
			if(y < 0){
				return Direction.South_West;
			}
			return Direction.West;
		}
		return Direction.Null;
	}
	public static Direction IntToDirection(int unitX, int unitY, int targetX, int targetY){
		return IntToDirection(targetX - unitX, targetY - unitY);
	}
}
