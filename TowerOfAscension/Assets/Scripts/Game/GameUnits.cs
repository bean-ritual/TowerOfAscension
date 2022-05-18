using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class GameUnits{
	[Serializable]
	public class NullGameUnits : GameUnits{
		public override Unit Get(int id){
			return Unit.GetNullUnit();
		}
		public override Unit Create(){
			return Unit.GetNullUnit();
		}
		public override void Clear(Game game, int id){}
		public override bool CheckBounds(int index){
			return false;
		}
		public override int GetCount(){
			return 0;
		}
		public override bool IsNull(){
			return true;
		}
	}
	//
	[Serializable]
	public class UnlimitedGameUnits : GameUnits{
		private List<Unit> _units;
		private Queue<int> _empty;
		public UnlimitedGameUnits(){
			_units = new List<Unit>();
			_empty = new Queue<int>();
		}
		public UnlimitedGameUnits(int init){
			_units = new List<Unit>(init);
			_empty = new Queue<int>(init);
			for(int i = 0; i < (init - 1); i++){
				_units.Add(new Unit.RealUnit(i));
				_empty.Enqueue(i);
			}
		}
		public override Unit Get(int id){
			if(id < 0 || id >= _units.Count){
				return Unit.GetNullUnit();
			}else{
				return _units[id];
			}
		}
		public override Unit Create(){
			if(_empty.Count > 0){
				return _units[_empty.Dequeue()];
			}else{
				int id = _units.Count;
				_units.Add(new Unit.RealUnit(id));
				return _units[id];
			}
		}
		public override void Clear(Game game, int id){
			if(CheckBounds(id)){
				/*
				for(int i = 0; i < game.GetGameBlocksCount(); i++){
					game.GetGameBlocks(i).Remove(game, id);
				}
				_empty.Enqueue(id);
				*/
			}
		}
		public override bool CheckBounds(int index){
			return !(index < 0 || index >= _units.Count);
		}
		public override int GetCount(){
			return _units.Count;
		}
		public override bool IsNull(){
			return false;
		}
	}
	//
	public abstract Unit Get(int id);
	public abstract Unit Create();
	public abstract void Clear(Game game, int id);
	public abstract bool CheckBounds(int index);
	public abstract int GetCount();
	public abstract bool IsNull();
	//
	private static NullGameUnits _NULL_GAME_UNITS = new NullGameUnits();
	public static GameUnits GetNullGameUnits(){
		return _NULL_GAME_UNITS;
	}
	public static int GetNullID(){
		const int NULL_ID = -1;
		return NULL_ID;
	}
}
