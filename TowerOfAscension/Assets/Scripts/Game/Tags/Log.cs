using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Log : 
	Tag,
	Tag.IInput<string>
	{
	private const Tag.ID _TAG_ID = Tag.ID.PlayerLog;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void Input(Game game, Unit self, string value){
		MessageBoxManager.GetInstance().ShowMessage(value);
	}
	public override Tag.IInput<string> GetIInputString(){
		return this;
	}
	public static Tag Create(){
		return new Log();
	}
}
