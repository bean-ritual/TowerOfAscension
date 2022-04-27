using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LightWorldVisual : WorldVisualAnimations{
	public override bool Check(Game game, Unit self){
		return self.GetTag(game, Tag.ID.Position).GetIGetTile().GetTile(game, self).GetLightable().GetLight() > 0;
	}
	public static new Tag Create(SpriteSheet.SpriteID spriteID, int spriteIndex, int sortingOrder){
		LightWorldVisual tag = new LightWorldVisual();
		tag.Setup(spriteID, spriteIndex, sortingOrder);
		return tag;
	}
}
