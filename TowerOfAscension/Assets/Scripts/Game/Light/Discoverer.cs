using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Discoverer : 
	Block.BaseBlock,
	IProcess
	{
	private int _range;
	public Discoverer(int range){
		_range = range;
	}
	public void Process(Game game){
		GameBlocks tiles = game.GetGameBlocks(Game.TOAGame.BLOCK_LIGHT);
		for(int i = 0; i < tiles.GetCount(); i++){
			tiles.GetIndex(game, i).GetITileLight().SetLight(game, 0);
		}
		/*
		for(int x = 0; x < map.GetWidth(); x++){
			for(int y = 0; y < map.GetHeight(); y++){
				map.Get(x, y).GetIDataTile().GetBlock(game, 4).GetITileLight().SetLight(game, 0);
			}
		}
		*/
		Map map = game.GetMap();
		Map.Tile origin = GetSelf(game).GetBlock(game, Game.TOAGame.BLOCK_WORLD).GetIWorldPosition().GetTile(game);
		Block block = origin.GetIDataTile().GetBlock(game, Game.TOAGame.BLOCK_LIGHT);
		block.GetITileLight().Discover(game);
		block.GetITileLight().SetLight(game, _range);
		origin.GetXY(out int sourceX, out int sourceY);
		map.GetIMapmatics().CalculateFov(sourceX, sourceY, _range, (int range, Map.Tile tile) => {
			Block block = tile.GetIDataTile().GetBlock(game, Game.TOAGame.BLOCK_LIGHT);
			block.GetITileLight().Discover(game);
			block.GetITileLight().SetLight(game, _range - (range - 1));
			return (!block.GetICanOpaque().CanOpaque(game));
		});
		game.GetGameBlocks(Game.TOAGame.BLOCK_LIGHT).FireGameBlockUpdateEvent();
	}
    public override void Disassemble(Game game){
		
	}
	public override IProcess GetIProcess(){
		return this;
	}
}
