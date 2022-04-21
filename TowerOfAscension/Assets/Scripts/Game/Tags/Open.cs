using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Open : 
	Tag,
	Tag.IInput<Unit>
	{
	private static Tag.ID _TAG_ID = Tag.ID.Interactable;
	private bool _locked = true;
	public void Setup(){
		_locked = true;
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void Input(Game game, Unit self, Unit actor){
		_locked ^= true;
		if(!_locked){
			self.GetTaggable().GetTag(game, Tag.ID.WorldUnit).GetISetValue1Int().SetValue1(game, self, 1);
			self.GetTaggable().GetTag(game, Tag.ID.Collision).GetISetValue1Collider().SetValue1(game, self, Tag.Collider.Null);
		}else{
			self.GetTaggable().GetTag(game, Tag.ID.WorldUnit).GetISetValue1Int().SetValue1(game, self, 0);
			self.GetTaggable().GetTag(game, Tag.ID.Collision).GetISetValue1Collider().SetValue1(game, self, Tag.Collider.Basic);
		}
		self.GetTaggable().GetTag(game, Tag.ID.Opacity).GetISetValue1Bool().SetValue1(game, self, _locked);
	}
	public override Tag.IInput<Unit> GetIInputUnit(){
		return this;
	} 
	public static Tag Create(){
		return new Open();
	}
}
