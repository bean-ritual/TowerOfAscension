using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TagOverride : Tag{
	private Tag.ID _tagID;
	private Tag.ID _tagOver;
	public void Setup(Tag.ID tagID, Tag.ID tagOver){
		_tagID = tagID;
		_tagOver = tagOver;
	}
	public override Tag.ID GetTagID(){
		return _tagID;
	}
	public override void Disassemble(){
		//
	}
	public override Tag GetSelf(Game game, Unit self){
		return self.GetRealTag(game, _tagOver).GetIGetUnit().GetUnit(game, self).GetTag(game, _tagID);
	}
}
