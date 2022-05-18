using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TagControl : 
	Tag,
	Tag.IProcess,
	Tag.IClear
	{
	[field:NonSerialized]public static event EventHandler<OnPlayerControlEventArgs> OnPlayerControl;
	public class OnPlayerControlEventArgs : EventArgs{
		public Unit player;
		public OnPlayerControlEventArgs(Unit player){
			this.player = player;
		}
	}
	private static Tag.ID _TAG_ID = Tag.ID.AI;
	public enum ControlState{
		Null,
		Locked,
		Input,
	};
	private ControlState _state = ControlState.Null;
	public void Setup(){
		_state = ControlState.Locked;
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public bool Process(Game game, Unit self){
		OnPlayerControl?.Invoke(this, new OnPlayerControlEventArgs(self));
		switch(_state){
			default: return game.GetLevel().NextTurn(game);
			case ControlState.Null: return game.GetLevel().NextTurn(game);
			case ControlState.Locked:{
				if(_state != ControlState.Input){
					_state = ControlState.Input;
				}
				return true;
			}
			case ControlState.Input: return false;
		}
	}
	public void Clear(Game game, Unit self){
		if(_state == ControlState.Input){
			_state = ControlState.Locked;
			game.GetLevel().NextTurn(game);
		}
	}
	public override Tag.IProcess GetIProcess(){
		return this;
	}
	public override Tag.IClear GetIClear(){
		return this;
	}
	public static void InvokePlayerControl(object sender, Unit unit){
		OnPlayerControl?.Invoke(sender, new OnPlayerControlEventArgs(unit));
	}
	public static Tag Create(){
		TagControl tag = new TagControl();
		tag.Setup();
		return tag;
	}
}
