using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Data{
	public static class DATA{
		public static Data CreatePlayer(Game game){
			Data data = game.GetGameData().Create();
			data.AddBlock(game, 1, new MovePosition(1));
			data.AddBlock(game, 2, new WorldVisual(3, 15));
			data.AddBlock(game, 3, new PlayerControl());
			data.AddBlock(game, 4, new Discoverer(3));
			data.AddBlock(game, Game.TOAGame.BLOCK_INVENTORY, new Inventory());
			data.AddBlock(game, 8, new Murderer());
			return data;
		}
		public static Data CreateGroundTile(Game game, int x, int y){
			Data data = game.GetGameData().Create();
			data.AddBlock(game, 0, new Ground(x, y));
			data.AddBlock(game, 4, new TileLight());
			return data;
		}
		public static Data CreateWallTile(Game game, int x, int y){
			Data data = game.GetGameData().Create();
			data.AddBlock(game, 0, new Wall(x, y));
			data.AddBlock(game, 4, new OpaqueTile());
			return data;
		}
		public static Data CreateEnterTile(Game game, int x, int y){
			Data data = game.GetGameData().Create();
			data.AddBlock(game, 0, new Ground(x, y));
			data.AddBlock(game, 1, new PermaPosition(x, y));
			data.AddBlock(game, 2, new WorldVisual(1, 0));
			data.AddBlock(game, 4, new TileLight());
			return data;
		}
		public static Data CreateExitTile(Game game, int x, int y){
			Data data = game.GetGameData().Create();
			data.AddBlock(game, 0, new Ground(x, y));
			data.AddBlock(game, 1, new PermaPosition(x, y));
			data.AddBlock(game, 2, new WorldVisual(2, 0));
			data.AddBlock(game, 4, new TileLight());
			data.AddBlock(game, Game.TOAGame.BLOCK_TRIP, new Stairs());
			return data;
		}
		public static Data CreateTile(Game game, int tile, int x, int y){
			switch(tile){
				default: return Data.GetNullData();
				case 0: return CreateGroundTile(game, x, y);
				case 1: return CreateWallTile(game, x, y);
				case 2: return CreateEnterTile(game, x, y);
				case 3: return CreateExitTile(game, x, y);
			}
		}
		public static Data CreateRat(Game game){
			Data data = game.GetGameData().Create();
			data.AddBlock(game, 1, new MovePosition(1));
			data.AddBlock(game, 2, new LightVisual(4, 10));
			data.AddBlock(game, 3, new MonsterAI());
			data.AddBlock(game, 9, new Stats(10, 50));
			return data;
		}
	}
	[Serializable]
	public class NullData : Data{
		public override int GetID(){
			const int NULL_ID = -1;
			return NULL_ID;
		}
		public override void AddBlock(Game game, int blockID, Block block){}
		public override Block GetBlock(Game game, int blockID){
			return Block.GetNullBlock();
		}
		public override void Disassemble(Game game){}
		public override bool IsNull(){
			return true;
		}
		//
		public override void FireDataUpdateEvent(){}
		public override void FireBlockUpdateEvent(int blockID){}
		public override void FireBlockDataAddEvent(int blockID, int newDataID){}
		public override void FireBlockDataRemoveEvent(int blockID, int newDataID){}
	}
	[Serializable]
	public class RealData : Data{
		private int _id;
		public RealData(int id){
			_id = id;
		}
		public override int GetID(){
			return _id;
		}
		public override void AddBlock(Game game, int blockID, Block block){
			game.GetGameBlocks(blockID).Add(game, _id, block);
		}
		public override Block GetBlock(Game game, int blockID){
			return game.GetGameBlocks(blockID).Get(game, _id);
		}
		public override void Disassemble(Game game){
			game.GetGameData().Clear(game, _id);
		}
		public override bool IsNull(){
			return false;
		}
	}
	public class DataUpdateEventArgs : EventArgs{
		public int dataID;
		public DataUpdateEventArgs(int dataID){
			this.dataID = dataID;
		}
	}
	public class BlockUpdateEventArgs : EventArgs{
		public int dataID;
		public int blockID;
		public BlockUpdateEventArgs(int dataID, int blockID){
			this.dataID = dataID;
			this.blockID = blockID;
		}
	}
	public class BlockDataUpdateEventArgs : EventArgs{
		public int dataID;
		public int blockID;
		public int newDataID;
		public BlockDataUpdateEventArgs(int dataID, int blockID, int newDataID){
			this.dataID = dataID;
			this.blockID = blockID;
			this.newDataID = newDataID;
		}
	}
	[field:NonSerialized]public event EventHandler<DataUpdateEventArgs> OnDataUpdate;
	[field:NonSerialized]public event EventHandler<BlockUpdateEventArgs> OnBlockUpdate;
	[field:NonSerialized]public event EventHandler<BlockDataUpdateEventArgs> OnBlockDataAdd;
	[field:NonSerialized]public event EventHandler<BlockDataUpdateEventArgs> OnBlockDataRemove;
	public abstract int GetID();
	public abstract void AddBlock(Game game, int blockID, Block block);
	public abstract Block GetBlock(Game game, int blockID);
	public abstract void Disassemble(Game game);
	public abstract bool IsNull();
	//
	public virtual void FireDataUpdateEvent(){
		OnDataUpdate?.Invoke(this, new DataUpdateEventArgs(GetID()));
	}
	public virtual void FireBlockUpdateEvent(int blockID){
		OnBlockUpdate?.Invoke(this, new BlockUpdateEventArgs(GetID(), blockID));
	}
	public virtual void FireBlockDataAddEvent(int blockID, int newDataID){
		OnBlockDataAdd?.Invoke(this, new BlockDataUpdateEventArgs(GetID(), blockID, newDataID));
	}
	public virtual void FireBlockDataRemoveEvent(int blockID, int newDataID){
		OnBlockDataRemove?.Invoke(this, new BlockDataUpdateEventArgs(GetID(), blockID, newDataID));
	}
	//
	private static NullData _NULL_DATA = new NullData();
	public static Data GetNullData(){
		return _NULL_DATA;
	}
}
