using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class GameData{
	[Serializable]
	public class NullGameData : GameData{
		public override Data Get(int id){
			return Data.GetNullData();
		}
		public override Data Create(){
			return Data.GetNullData();
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
	public class UnlimitedGameData : GameData{
		private List<Data> _data;
		private Queue<int> _empty;
		public UnlimitedGameData(){
			_data = new List<Data>();
			_empty = new Queue<int>();
		}
		public UnlimitedGameData(int init){
			_data = new List<Data>(init);
			_empty = new Queue<int>(init);
			for(int i = 0; i < (init - 1); i++){
				_data.Add(new Data.RealData(i));
				_empty.Enqueue(i);
			}
		}
		public override Data Get(int id){
			if(id < 0 || id >= _data.Count){
				return Data.GetNullData();
			}else{
				return _data[id];
			}
		}
		public override Data Create(){
			if(_empty.Count > 0){
				return _data[_empty.Dequeue()];
			}else{
				int id = _data.Count;
				_data.Add(new Data.RealData(id));
				return _data[id];
			}
		}
		public override void Clear(Game game, int id){
			if(CheckBounds(id)){
				for(int i = 0; i < game.GetGameBlocksCount(); i++){
					game.GetGameBlocks(i).Remove(game, id);
				}
				_empty.Enqueue(id);
			}
		}
		public override bool CheckBounds(int index){
			return !(index < 0 || index >= _data.Count);
		}
		public override int GetCount(){
			return (_data.Count - _empty.Count);
		}
		public override bool IsNull(){
			return false;
		}
	}
	//
	public abstract Data Get(int id);
	public abstract Data Create();
	public abstract void Clear(Game game, int id);
	public abstract bool CheckBounds(int index);
	public abstract int GetCount();
	public abstract bool IsNull();
	//
	private static NullGameData _NULL_GAME_DATA = new NullGameData();
	public static GameData GetNullGameData(){
		return _NULL_GAME_DATA;
	}
	public static int GetNullID(){
		const int NULL_ID = -1;
		return NULL_ID;
	}
}
