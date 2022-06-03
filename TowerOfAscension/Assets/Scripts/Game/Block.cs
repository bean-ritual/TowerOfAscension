using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Block{
	[Serializable]
	public class NullBlock : 
		Block,
		IProcess,
		IStats,
		IWorldPosition,
		IListData,
		ICanPrint,
		ICanConnect,
		ICanWalk,
		IVisual,
		IDoTurn,
		IEndTurn,
		IMovement,
		IConclude,
		ICanOpaque,
		ITileLight,
		ITile,
		IKillable,
		IActive,
		ITripwire,
		MapMeshManager.ITileMeshData
		{
		//
		public void Process(Game game){}
		//
		public int GetHealth(Game game){
			return 0;
		}
		public int GetMaxHealth(Game game){
			return 0;
		}
		//
		public void Spawn(Game game, int x, int y){}
		public void Despawn(Game game){}
		public void SetPosition(Game game, int x, int y){}
		public void ClearPosition(Game game){}
		public Map.Tile GetTile(Game game){
			return Map.Tile.GetNullTile();
		}
		public Vector3 GetPosition(Game game){
			return Vector3.zero;
		}
		//
		public bool CanPrint(Game game){
			return true;
		}
		//
		public bool CanConnect(Game game){
			return false;
		}
		//
		public bool CanWalk(Game game){
			return false;
		}
		//
		public void PlayAnimation(Game game, WorldAnimation animation){}
		public int GetSprite(Game game){
			return 0;
		}
		public int GetSortingOrder(Game game){
			return 0;
		}
		public bool GetRender(Game game){
			return false;
		}
		public int GetAtlasIndex(Game game){
			return 0;
		}
		public int GetUVFactor(Game game){
			return 0;
		}
		//
		public bool DoTurn(Game game){
			return true;
		}
		//
		public void EndTurn(Game game){}
		//
		public void Move(Game game, Direction direction){}
		//
		public void Conclude(Game game){}
		//
		public bool CanOpaque(Game game){
			return false;
		}
		//
		public void SetLight(Game game, int lightLevel){}
		public int GetLight(Game game){
			return 0;
		}
		public void Discover(Game game){}
		public bool GetDiscovered(Game game){
			return false;
		}
		//
		/*
		public Map.Tile GetTile(Game game){
			return Map.Tile.GetNullTile();
		}
		*/
		public void Kill(Game game){}
		public void SetKiller(Game game, Data killer){}
		//
		public void Trigger(Game game, Direction direction){}
		//
		public void Trip(Game game, Data data){}
		//
		public void AddData(Game game, Data data){}
		public void RemoveData(Game game, Data data){}
		public Data GetData(Game game, int index){
			return Data.GetNullData();
		}
		public int GetDataCount(){
			return 0;
		}
		//
		public override void SetIDs(int dataID, int blockID){}
		public override Data GetSelf(Game game){
			return Data.GetNullData();
		}
		public override GameBlocks GetGameBlocks(Game game){
			return GameBlocks.GetNullGameBlocks();
		}
		public override void Disassemble(Game game){}
		public override bool IsNull(){
			return true;
		}
	}
	[Serializable]
	public abstract class BaseBlock : Block{
		private int _dataID;
		private int _blockID;
		public override void SetIDs(int dataID, int blockID){
			_dataID = dataID;
			_blockID = blockID;
		}
		public override Data GetSelf(Game game){
			return game.GetGameData().Get(_dataID);
		}
		public override GameBlocks GetGameBlocks(Game game){
			return game.GetGameBlocks(_blockID);
		}
		public override bool IsNull(){
			return false;
		}
		protected void FireDataUpdateEvent(Game game){
			GetSelf(game).FireDataUpdateEvent();
		}
		protected void FireBlockUpdateEvent(Game game){
			GetSelf(game).FireBlockUpdateEvent(_blockID);
		}
		protected void FireBlockDataAddEvent(Game game, int newDataID){
			GetSelf(game).FireBlockDataAddEvent(_blockID, newDataID);
		}
		protected void FireBlockDataRemoveEvent(Game game, int newDataID){
			GetSelf(game).FireBlockDataRemoveEvent(_blockID, newDataID);
		}
		protected void FireGameBlockUpdateEvent(Game game){
			GetGameBlocks(game).FireGameBlockUpdateEvent();
		}
	}
	public abstract void SetIDs(int dataID, int blockID);
	public abstract Data GetSelf(Game game);
	public abstract GameBlocks GetGameBlocks(Game game);
	public abstract void Disassemble(Game game);
	public abstract bool IsNull();
	//
	public virtual IProcess GetIProcess(){
		return _NULL_BLOCK;
	}
	public virtual IStats GetIStats(){
		return _NULL_BLOCK;
	}
	public virtual IWorldPosition GetIWorldPosition(){
		return _NULL_BLOCK;
	}
	public virtual IListData GetIListData(){
		return _NULL_BLOCK;
	}
	public virtual ICanPrint GetICanPrint(){
		return _NULL_BLOCK;
	}
	public virtual ICanConnect GetICanConnect(){
		return _NULL_BLOCK;
	}
	public virtual ICanWalk GetICanWalk(){
		return _NULL_BLOCK;
	}
	public virtual IVisual GetIVisual(){
		return _NULL_BLOCK;
	}
	public virtual IDoTurn GetIDoTurn(){
		return _NULL_BLOCK;
	}
	public virtual IEndTurn GetIEndTurn(){
		return _NULL_BLOCK;
	}
	public virtual IMovement GetIMovement(){
		return _NULL_BLOCK;
	}
	public virtual IConclude GetIConclude(){
		return _NULL_BLOCK;
	}
	public virtual ICanOpaque GetICanOpaque(){
		return _NULL_BLOCK;
	}
	public virtual ITileLight GetITileLight(){
		return _NULL_BLOCK;
	}
	public virtual ITile GetITile(){
		return _NULL_BLOCK;
	}
	public virtual IKillable GetIKillable(){
		return _NULL_BLOCK;
	}
	public virtual IActive GetIActive(){
		return _NULL_BLOCK;
	}
	public virtual ITripwire GetITripwire(){
		return _NULL_BLOCK;
	}
	public virtual MapMeshManager.ITileMeshData GetITileMeshData(){
		return _NULL_BLOCK;
	}
	//
	private static NullBlock _NULL_BLOCK = new NullBlock();
	public static Block GetNullBlock(){
		return _NULL_BLOCK;
	}
}
