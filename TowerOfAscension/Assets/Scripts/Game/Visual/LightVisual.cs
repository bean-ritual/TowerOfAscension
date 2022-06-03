using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LightVisual : WorldVisual{
	public LightVisual(int sprite, int sortingOrder) : base(sprite, sortingOrder){}
	public override bool GetRender(Game game){
		return GetSelf(game).GetBlock(game, Game.TOAGame.BLOCK_WORLD).GetIWorldPosition().GetTile(game).GetIDataTile().GetBlock(game, Game.TOAGame.BLOCK_LIGHT).GetITileLight().GetLight(game) > 0;
	}
}
