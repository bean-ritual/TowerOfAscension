using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ClassicGen : 
	Unit,
	Register<Unit>.IRegisterable,
	Unit.IProcessable{
	[Serializable]
	public abstract class Spawner{
		public static class SPAWNER_DATA{
			public static Spawner GetSpawner(int index, int x, int y){
				switch(index){
					default: return GetNullSpawner();
					case 0: return GetRandomBluePrintSpawner(x, y);
				}
			}
			public static Spawner GetRandomBluePrintSpawner(int x, int y){
				return new BluePrintSpawner(x, y, BluePrint.BLUEPRINT_DATA.GetRandomIndex());
			}
		}
		[Serializable]
		public class NullSpawner : Spawner{
			public NullSpawner(){}
			public override void Spawn(Level level, ClassicGen master){}
			public override bool Check(Level level, ClassicGen master){
				return false;
			}
			public override Tile GetTile(Level level, ClassicGen master){
				return Tile.GetNullTile();
			}
		}
		[field:NonSerialized]private static readonly NullSpawner _NULL_SPAWNER = new NullSpawner();
		protected int _x;
		protected int _y;
		public Spawner(int x, int y){
			_x = x;
			_y = y;
		}
		public Spawner(){}
		public abstract void Spawn(Level level, ClassicGen master);
		public abstract bool Check(Level level, ClassicGen master);
		public abstract Tile GetTile(Level level, ClassicGen master);
		public static Spawner GetNullSpawner(){
			return _NULL_SPAWNER;
		}
	}
	[Serializable]
	public class BluePrintSpawner : Spawner{
		private readonly int _index;
		public BluePrintSpawner(int x, int y, int index) : base(x, y){
			_index = index;
		}
		public override void Spawn(Level level, ClassicGen master){
			BluePrint.BLUEPRINT_DATA.GetBluePrint(_index).Spawn(level, master, _x, _y);
		}
		public override bool Check(Level level, ClassicGen master){
			return true;
			//return BluePrint.BLUEPRINT_DATA.GetBluePrint(_index).Check(level, master, _x, _y);
		}
		public override Tile GetTile(Level level, ClassicGen master){
			return level.Get(_x, _y);
		}
	}
	private Register<Unit>.ID _id;
	public ClassicGen(Level level){
		_id = Register<Unit>.ID.GetNullID();
		_spawners = new Queue<Spawner>();
		level.GetMidPoint(out int x, out int y);
		AddSpawner(Spawner.SPAWNER_DATA.GetRandomBluePrintSpawner(x, y));
	}
	private Queue<Spawner> _spawners;
	public virtual void AddSpawner(Spawner spawner){
		_spawners.Enqueue(spawner);
	}
	public void AddToRegister(Register<Unit> register){
		register.Add(this, ref _id);
	}
	public Register<Unit>.ID GetID(){
		return _id;
	}
	public bool Process(Level level){
		if(_spawners.Count > 0){
			_spawners.Dequeue().Spawn(level, this);
		}
		return level.NextTurn();
	}
	public override Register<Unit>.IRegisterable GetRegisterable(){
		return this;
	}
	public override Unit.IProcessable GetProcessable(){
		return this;
	}
}
