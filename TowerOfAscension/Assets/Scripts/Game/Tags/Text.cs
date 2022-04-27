using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Text : 
	Tag,
	Tag.IGetStringValue1
	{
	private static Tag.ID _tagID = Tag.ID.Null;
	private string _text;
	public void Setup(Tag.ID tagID, string text){
		_tagID = tagID;
		_text = text;
	}
	public override Tag.ID GetTagID(){
		return _tagID;
	}
	public override void Disassemble(){
		//
	}
	public override void BuildString(StringBuilder builder){
		builder.Append(_text).Append(System.Environment.NewLine);
	}
	public string GetStringValue1(Game game, Unit self){
		return _text;
	}
	public override Tag.IGetStringValue1 GetIGetStringValue1(){
		return this;
	}
	public static Tag Create(Tag.ID tagID, string text){
		Text tag = new Text();
		tag.Setup(tagID, text);
		return tag;
	}
}
