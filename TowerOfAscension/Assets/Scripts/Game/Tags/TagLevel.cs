using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TagLevel : 
	Tag,
	Tag.ISetValue1<int>,
	Tag.IFortifyValue1,
	Tag.IGetIntValue1
	{
	private const int _MIN_LEVEL = 0;
	private const int _MAX_LEVEL = 100;
	private static Tag.ID _TAG_ID = Tag.ID.Level;
	private int _level;
	public void Setup(int level){
		_level = level;
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void SetValue1(Game game, Unit self, int value){
		_level = value;
		self.UpdateAllTags(game);
	}
	public void FortifyValue1(Game game, Unit self){
		const string LEVEL_MESSAGE = "You gain a Level!";
		const int LEVEL_UP = 1;
		_level = (_level + LEVEL_UP);
		self.GetTag(game, Tag.ID.PlayerLog).GetIInputString().Input(game, self, LEVEL_MESSAGE);
		self.UpdateAllTags(game);
	}
	public int GetIntValue1(Game game, Unit self){
		return _level;
	}
	public override Tag.ISetValue1<int> GetISetValue1Int(){
		return this;
	}
	public override Tag.IFortifyValue1 GetIFortifyValue1(){
		return this;
	}
	public override Tag.IGetIntValue1 GetIGetIntValue1(){
		return this;
	}
	public static Tag Create(int level){
		TagLevel tag = new TagLevel();
		tag.Setup(level);
		return tag;
	}
}
