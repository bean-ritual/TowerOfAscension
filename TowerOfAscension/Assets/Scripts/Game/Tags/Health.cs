using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Health : 
	Tag,
	Tag.IFortifyValue1<int>,
	Tag.IFortifyValue2<int>,
	Tag.IDamageValue1<int>,
	Tag.IDamageValue2<int>,
	Tag.IGetIntValue1,
	Tag.IGetIntValue2,
	Tag.IRefresh
	{
	private static Queue<Health> _POOL = new Queue<Health>();
	private const Tag.ID _TAG_ID = Tag.ID.Health;
	private const int _MAX_MAX_HEALTH = 999;
	private const int _MIN_MAX_HEALTH = 10;
	private const float _MAX_HEALTH = 1f;
	private const int _MIN_HEALTH = 0;
	private float _health;
	private int _baseMax;
	private int _modifyMax;
	private int _scaling;
	public void Setup(int health, int scaling){
		_health = _MAX_HEALTH;
		_baseMax = health;
		_scaling = scaling;
		_modifyMax = 0;
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		_POOL.Enqueue(this);
	}
	public void FortifyValue1(Game game, Unit self, int value){
		const string FORTIFY_MESSAGE = "You gain {0} Hitpoints";
		self.GetTag(game, Tag.ID.WorldUnit).GetIGetWorldUnitController().GetWorldUnitController(game, self).InvokeTextPopupEvent(("+" + value), TextPopup.TextColour.Green);
		self.GetTag(game, Tag.ID.PlayerLog).GetIInputString().Input(game, self, String.Format(FORTIFY_MESSAGE, value));
		int max = GetIntValue2(game, self);
		_health = Mathf.Clamp(((max * _health) + value) / max, _MIN_HEALTH, _MAX_HEALTH);
		TagUpdateEvent();
	}
	public void FortifyValue2(Game game, Unit self, int value){
		_modifyMax = (_modifyMax + value);
		TagUpdateEvent();
	}
	public void DamageValue1(Game game, Unit self, int value){
		const string DAMAGE_MESSAGE = "You take {0} Damage";
		self.GetTag(game, Tag.ID.WorldUnit).GetIGetWorldUnitController().GetWorldUnitController(game, self).InvokeTextPopupEvent(("-" + value), TextPopup.TextColour.Red);
		self.GetTag(game, Tag.ID.PlayerLog).GetIInputString().Input(game, self, String.Format(DAMAGE_MESSAGE, value));
		int max = GetIntValue2(game, self);
		_health = Mathf.Clamp(((max * _health) - value) / max, _MIN_HEALTH, _MAX_HEALTH);
		if(_health <= 0){
			self.GetTag(game, Tag.ID.Alive).GetIDamageValue1().DamageValue1(game, self);
		}
		TagUpdateEvent();
	}
	public void DamageValue2(Game game, Unit self, int value){
		_modifyMax = (_modifyMax - value);
		TagUpdateEvent();
	}
	public int GetIntValue1(Game game, Unit self){
		return (int)Mathf.Ceil(GetIntValue2(game, self) * _health);
	}
	public int GetIntValue2(Game game, Unit self){
		return Mathf.Clamp((_baseMax + _modifyMax + (self.GetTag(game, Tag.ID.Level).GetIGetIntValue1().GetIntValue1(game, self) * _scaling)), _MIN_MAX_HEALTH, _MAX_MAX_HEALTH);
	}
	public void Refresh(Game gain, Unit self){
		
	}
	public override Tag.IFortifyValue1<int> GetIFortifyValue1Int(){
		return this;
	}
	public override Tag.IFortifyValue2<int> GetIFortifyValue2Int(){
		return this;
	}
	public override Tag.IDamageValue1<int> GetIDamageValue1Int(){
		return this;
	}
	public override Tag.IDamageValue2<int> GetIDamageValue2Int(){
		return this;
	}
	public override Tag.IGetIntValue1 GetIGetIntValue1(){
		return this;
	}
	public override Tag.IGetIntValue2 GetIGetIntValue2(){
		return this;
	}
	public override Tag.IRefresh GetIRefresh(){
		return this;
	}
	public static Tag Create(int health, int scaling){
		Health tag;
		if(_POOL.Count > 0){
			tag = _POOL.Dequeue();
		}else{
			tag = new Health();
		}
		tag.Setup(health, scaling);
		return tag;
	}
}
