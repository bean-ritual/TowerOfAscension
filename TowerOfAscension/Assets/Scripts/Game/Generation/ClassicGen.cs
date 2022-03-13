using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ClassicGen : 
	Unit,
	Unit.IProcessable{
	[Serializable]
	public abstract class Spawner{
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
	private List<Spawner> _spawners;
	public bool Process(Level level){
		return true;
	}
	public override Unit.IProcessable GetProcessable(){
		return this;
	}
}
