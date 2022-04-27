using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class WorldVisualAnimations : 
	WorldVisual,
	Tag.IGetWorldUnitController,
	WorldUnit.IWorldUnitController
	{
	[field:NonSerialized]public event EventHandler<WorldUnit.MeleeAttackEventArgs> OnMeleeAttackAnimation;
	[field:NonSerialized]public event EventHandler<WorldUnit.TextPopupEventArgs> OnTextPopupEvent;
	public void InvokeMeleeAttackAnimation(Direction direction){
		OnMeleeAttackAnimation?.Invoke(this, new WorldUnit.MeleeAttackEventArgs(direction));
	}
	public void InvokeTextPopupEvent(string text){
		OnTextPopupEvent?.Invoke(this, new WorldUnit.TextPopupEventArgs(text));
	}
	public void InvokeTextPopupEvent(string text, TextPopup.TextColour colour){
		OnTextPopupEvent?.Invoke(this, new WorldUnit.TextPopupEventArgs(text, colour));
	}
	public WorldUnit.IWorldUnitController GetWorldUnitController(Game game, Unit self){
		return this;
	}
	public override Tag.IGetWorldUnitController GetIGetWorldUnitController(){
		return this;
	}
	public static new Tag Create(SpriteSheet.SpriteID spriteID, int spriteIndex, int sortingOrder){
		WorldVisualAnimations tag = new WorldVisualAnimations();
		tag.Setup(spriteID, spriteIndex, sortingOrder);
		return tag;
	}
}
