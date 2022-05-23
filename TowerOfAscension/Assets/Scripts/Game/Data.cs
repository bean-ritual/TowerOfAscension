using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Data{
	public static class DATA{
		public static Data CreatePlayer(Game game){
			Data data = game.GetGameData().Create();
			data.AddBlock(game, 1, new WorldPosition());
			data.AddBlock(game, 2, new WorldVisual(3, 15));
			return data;
		}
		public static Data CreateGroundTile(Game game, int x, int y){
			Data data = game.GetGameData().Create();
			data.AddBlock(game, 0, new Ground(x, y));
			return data;
		}
		public static Data CreateWallTile(Game game, int x, int y){
			Data data = game.GetGameData().Create();
			data.AddBlock(game, 0, new Wall(x, y));
			return data;
		}
		public static Data CreateEnterTile(Game game, int x, int y){
			Data data = game.GetGameData().Create();
			data.AddBlock(game, 0, new Ground(x, y));
			data.AddBlock(game, 1, new PermaPosition(x, y));
			data.AddBlock(game, 2, new WorldVisual(1, 0));
			return data;
		}
		public static Data CreateExitTile(Game game, int x, int y){
			Data data = game.GetGameData().Create();
			data.AddBlock(game, 0, new Ground(x, y));
			data.AddBlock(game, 1, new PermaPosition(x, y));
			data.AddBlock(game, 2, new WorldVisual(2, 0));
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
	[field:NonSerialized]public event EventHandler<BlockUpdateEventArgs> OnBlockUpdate;
	public abstract int GetID();
	public abstract void AddBlock(Game game, int blockID, Block block);
	public abstract Block GetBlock(Game game, int blockID);
	public abstract void Disassemble(Game game);
	public abstract bool IsNull();
	//
	public virtual void FireBlockUpdateEvent(int blockID){
		OnBlockUpdate?.Invoke(this, new BlockUpdateEventArgs(GetID(), blockID));
	}
	//
	private static NullData _NULL_DATA = new NullData();
	public static Data GetNullData(){
		return _NULL_DATA;
	}
}
