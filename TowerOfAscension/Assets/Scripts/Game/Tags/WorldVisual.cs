using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class WorldVisual : 
	Tag,
	Tag.ISetValue1<SpriteSheet.SpriteID>,
	Tag.ISetValue1<int>,
	Tag.ISetValue2<int>,
	Tag.IGetSprite,
	Tag.IGetIntValue2,
	Tag.ICondition,
	Tag.IGetWorldUnitController,
	WorldUnit.IWorldUnitController
	{
	[field:NonSerialized]public event EventHandler<WorldUnit.MeleeAttackEventArgs> OnMeleeAttackAnimation;
	[field:NonSerialized]public event EventHandler<WorldUnit.TextPopupEventArgs> OnTextPopupEvent;
	private const Tag.ID _TAG_ID = Tag.ID.WorldUnit;
	private SpriteSheet.SpriteID _spriteID = SpriteSheet.SpriteID.Null;
	private int _spriteIndex;
	private int _sortingOrder;
	public void Setup(SpriteSheet.SpriteID spriteID, int spriteIndex, int sortingOrder){
		_spriteID = spriteID;
		_spriteIndex = spriteIndex;
		_sortingOrder = sortingOrder;
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void SetValue1(Game game, Unit self, SpriteSheet.SpriteID value){
		_spriteID = value;
		TagUpdateEvent();
	}
	public void SetValue1(Game game, Unit self, int value){
		_spriteIndex = value;
		TagUpdateEvent();
	}
	public void SetValue2(Game game, Unit self, int value){
		_sortingOrder = value;
		TagUpdateEvent();
	}
	public Sprite GetSprite(Game game, Unit self){
		return SpriteSheet.SPRITESHEET_DATA.GetSpriteSheet(_spriteID).GetSprite(_spriteIndex);
	}
	public int GetIntValue2(Game game, Unit self){
		return _sortingOrder;
	}
	public virtual bool Check(Game game, Unit self){
		return true;
	}
	public void InvokeMeleeAttackAnimation(Direction direction){
		OnMeleeAttackAnimation?.Invoke(this, new WorldUnit.MeleeAttackEventArgs(direction));
	}
	public void InvokeTextPopupEvent(string text){
		OnTextPopupEvent?.Invoke(this, new WorldUnit.TextPopupEventArgs(text));
	}
	public WorldUnit.IWorldUnitController GetWorldUnitController(Game game, Unit self){
		return this;
	}
	public override Tag.ISetValue1<SpriteSheet.SpriteID> GetISetValue1SpriteID(){
		return this;
	}
	public override Tag.ISetValue1<int> GetISetValue1Int(){
		return this;
	}
	public override Tag.ISetValue2<int> GetISetValue2Int(){
		return this;
	}
	public override Tag.IGetSprite GetIGetSprite(){
		return this;
	}
	public override Tag.IGetIntValue2 GetIGetIntValue2(){
		return this;
	}
	public override Tag.ICondition GetICondition(){
		return this;
	}
	public override Tag.IGetWorldUnitController GetIGetWorldUnitController(){
		return this;
	}
	public static Tag Create(SpriteSheet.SpriteID spriteID, int spriteIndex, int sortingOrder){
		WorldVisual tag = new WorldVisual();
		tag.Setup(spriteID, spriteIndex, sortingOrder);
		return tag;
	}
}
