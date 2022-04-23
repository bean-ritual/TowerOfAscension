using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class WorldVisualUI : 
	Tag,
	Tag.IGetVector
	{
    private const Tag.ID _TAG_ID = Tag.ID.WorldUnitUI;
	private Vector3 _uiOffset;
	public void Setup(Vector3 uiOffset){
		_uiOffset = uiOffset;
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public Vector3 GetVector(Game game, Unit self){
		return _uiOffset;
	}
	public override Tag.IGetVector GetIGetVector(){
		return this;
	}
	public static Tag Create(Vector3 uiOffset){
		WorldVisualUI tag = new WorldVisualUI();
		tag.Setup(uiOffset);
		return tag;
	}
}
