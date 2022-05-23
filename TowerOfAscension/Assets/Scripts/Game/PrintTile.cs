using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class PrintTile : 
	Map.Tile.BlankTile,
	PrintTile.IPrintTile
	{
	public interface IPrintTile{
		void Spawn(Game game, int x, int y);
		bool Check(Game game, int x, int y);
	}
	public static class PRINT_TILE_DATA{
		private static readonly Map.Tile[] _PRINTS;
		static PRINT_TILE_DATA(){
			_PRINTS = new Map.Tile[]{
				Map.Tile.GetNullTile(),
				new WallPrint(),
				new GroundPrint(),
				new WallPrint(new int[]{0,1}),
				new GroundPrint(new int[]{3}),
				new GroundPrint(new int[]{4}),
				new GroundPrint(new int[]{5}),
			};
		}
		public static Map.Tile GetPrint(int index){
			if(index < 0 || index >= _PRINTS.Length){
				return Map.Tile.GetNullTile();
			}
			return _PRINTS[index];
		}
		public static bool CheckBounds(int index){
			if(index < 0 || index >= _PRINTS.Length){
				return false;
			}
			return true;
		}
	}
	//
	[Serializable]
	public class BlankPrint : PrintTile{
		public BlankPrint(){}
		public override void Spawn(Game game, int x, int y){
			game.GetMap().Get(x, y).GetIDataTile().SetData(game, Data.GetNullData());
		}
		public override bool Check(Game game, int x, int y){
			Map.Tile tile = game.GetMap().Get(x, y);
			return (!tile.IsNull()) && tile.GetIDataTile().GetData(game).GetBlock(game, 0).GetICanPrint().CanPrint(game);
		}
	}
	[Serializable]
	public class GroundPrint : PrintTile{
		private int[] _spawns;
		public GroundPrint(int[] spawns){
			_spawns = spawns;
		}
		public GroundPrint(){
			_spawns = new int[0];
		}
		public override void Spawn(Game game, int x, int y){
			game.GetMap().Get(x, y).GetIDataTile().SetData(game, Data.DATA.CreateGroundTile(game, x, y));
			for(int  i = 0; i < _spawns.Length; i++){
				Spawner.SPAWNER_DATA.GetSpawner(_spawns[i], x, y).Add(game);
			}
		}
		public override bool Check(Game game, int x, int y){
			Map.Tile tile = game.GetMap().Get(x, y);
			return (!tile.IsNull()) && tile.GetIDataTile().GetData(game).GetBlock(game, 0).GetICanPrint().CanPrint(game);
		}
	}
	[Serializable]
	public class WallPrint : PrintTile{
		private int[] _spawns;
		public WallPrint(int[] spawns){
			_spawns = spawns;
		}
		public WallPrint(){
			_spawns = new int[0];
		}
		public override void Spawn(Game game, int x, int y){
			game.GetMap().Get(x, y).GetIDataTile().SetData(game, Data.DATA.CreateWallTile(game, x, y));
			for(int  i = 0; i < _spawns.Length; i++){
				Spawner.SPAWNER_DATA.GetSpawner(_spawns[i], x, y).Add(game);
			}
		}
		public override bool Check(Game game, int x, int y){
			Map.Tile tile = game.GetMap().Get(x, y);
			return (!tile.IsNull()) && tile.GetIDataTile().GetData(game).GetBlock(game, 0).GetICanPrint().CanPrint(game);
		}
	}
	//
	public abstract void Spawn(Game game, int x, int y);
	public abstract bool Check(Game game, int x, int y);
	//
	public override PrintTile.IPrintTile GetIPrintTile(){
		return this;
	}
	/*
	public static void Default_OnSpawn(Game game, Unit master, Tile tile, int x, int y, int[] spawns){
		for(int  i = 0; i < spawns.Length; i++){
			ClassicGen.Spawner.SPAWNER_DATA.GetSpawner(spawns[i], x, y).AddToMaster(game, master);
		}
	}
	*/
}