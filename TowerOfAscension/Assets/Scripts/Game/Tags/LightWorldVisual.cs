using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LightWorldVisual : WorldVisual{
	public override bool Check(Game game, Unit self){
		return self.GetTileable().GetTile(game).GetLightable().GetLight() > 0;
	}
	public static Tag Create(SpriteSheet.SpriteID spriteID, int spriteIndex, int sortingOrder){
		LightWorldVisual tag = new LightWorldVisual();
		tag.Setup(spriteID, spriteIndex, sortingOrder);
		return tag;
	}
}
