using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class GameWorld{
	[Serializable]
	public class NullGameWorld : GameWorld{
		public override bool Tick(Game game){
			return true;
		}
		public override void EndTurn(Game game){}
		public override void Sort(){}
		public override void Reset(){}
		public override void Spawn(Game game, int id){}
		public override void Despawn(Game game, int id){}
		public override Data GetData(Game game, int index){
			return Data.GetNullData();
		}
		public override Block GetBlock(Game game, int index){
			return Block.GetNullBlock();
		}
		public override Data GetCurrentData(Game game){
			return Data.GetNullData();
		}
		public override int GetCurrentDataID(Game game){
			return -1;
		}
		public override int GetCount(){
			return 0;
		}
		public override bool IsNull(){
			return true;
		}
	}
	[Serializable]
	public class TOAGameWorld : GameWorld{
		private int _index;
		private List<int> _data;
		public TOAGameWorld(){
			_index = 0;
			_data = new List<int>();
		}
		public override bool Tick(Game game){
			Block block = GetBlock(game, _index);
			if(block.GetIDoTurn().DoTurn(game)){
				block.GetIEndTurn().EndTurn(game);
				int count = _data.Count;
				if(count > 0){
					_index = (_index + 1) % count;
					FireNextTurnEvent(_data[_index]);
				}
			}
			return !(_index > 0);
		}
		public override void EndTurn(Game game){
			GetBlock(game, _index).GetIEndTurn().EndTurn(game);
			int count = _data.Count;
			if(count > 0){
				_index = (_index + 1) % count;
				FireNextTurnEvent(_data[_index]);
			}
		}
		public override void Sort(){
			_data.Sort();
		}
		public override void Reset(){
			_index = -1;
		}
		public override void Spawn(Game game, int id){
			_data.Add(id);
			FireSpawnEvent(id);
		}
		public override void Despawn(Game game, int id){
			if(_data.Remove(id)){
				FireDespawnEvent(id);
			}
		}
		public override Data GetData(Game game, int index){
			if(index < 0 || index >= _data.Count){
				return Data.GetNullData();
			}else{
				return game.GetGameData().Get(_data[index]);
			}
		}
		public override Block GetBlock(Game game, int index){
			if(index < 0 || index >= _data.Count){
				return Block.GetNullBlock();
			}else{
				return game.GetGameBlocks(Game.TOAGame.BLOCK_DOTURN).Get(game, _data[index]);
			}
		}
		public override Data GetCurrentData(Game game){
			if(_index < 0 || _index >= _data.Count){
				return Data.GetNullData();
			}else{
				return game.GetGameData().Get(_data[_index]);
			}
		}
		public override int GetCurrentDataID(Game game){
			if(_index < 0 || _index >= _data.Count){
				return -1;
			}else{
				return _data[_index];
			}
		}
		public override int GetCount(){
			return _data.Count;
		}
		public override bool IsNull(){
			return false;
		}
	}
	//
	[field:NonSerialized]public event EventHandler<Data.DataUpdateEventArgs> OnSpawn;
	[field:NonSerialized]public event EventHandler<Data.DataUpdateEventArgs> OnDespawn;
	[field:NonSerialized]public event EventHandler<Data.DataUpdateEventArgs> OnNextTurn;
	public abstract bool Tick(Game game);
	public abstract void EndTurn(Game game);
	public abstract void Sort();
	public abstract void Reset();
	public abstract void Spawn(Game game, int id);
	public abstract void Despawn(Game game, int id);
	public abstract Data GetData(Game game, int index);
	public abstract Block GetBlock(Game game, int index);
	public abstract Data GetCurrentData(Game game);
	public abstract int GetCurrentDataID(Game game);
	public abstract int GetCount();
	public abstract bool IsNull();
	//
	protected void FireSpawnEvent(int id){
		OnSpawn?.Invoke(this, new Data.DataUpdateEventArgs(id));
	}
	protected void FireDespawnEvent(int id){
		OnDespawn?.Invoke(this, new Data.DataUpdateEventArgs(id));
	}
	protected void FireNextTurnEvent(int id){
		OnNextTurn?.Invoke(this, new Data.DataUpdateEventArgs(id));
	}
	//
	private static NullGameWorld _NULL_GAME_WORLD = new NullGameWorld();
	public static GameWorld GetNullGameWorld(){
		return _NULL_GAME_WORLD;
	}
}
