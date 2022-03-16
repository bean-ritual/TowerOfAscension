using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PlayerControl : 
	AI,
	AI.ITurnControl
	{
	[field:NonSerialized]public static event EventHandler<OnPlayerControlEventArgs> OnPlayerControl;
	public class OnPlayerControlEventArgs : EventArgs{
		public Unit player;
		public OnPlayerControlEventArgs(Unit player){
			this.player = player;
		}
	}
	public enum ControlState{
		Null,
		Locked,
		Input,
	};
	private ControlState _state;
	public PlayerControl(){
		_state = ControlState.Locked;
	}
	public override bool Process(Level level, Unit self){
		OnPlayerControl?.Invoke(this, new OnPlayerControlEventArgs(self));
		switch(_state){
			default: return level.NextTurn();
			case ControlState.Null: return level.NextTurn();
			case ControlState.Locked:{
				StartTurn(level, self);
				return true;
			}
			case ControlState.Input: return false;
		}
	}
	public virtual void StartTurn(Level level, Unit self){
		if(_state != ControlState.Input){
			_state = ControlState.Input;
		}
	}
	public virtual void EndTurn(Level level, Unit self){
		if(_state == ControlState.Input){
			_state = ControlState.Locked;
			level.NextTurn();
		}
	}
	public override ITurnControl GetTurnControl(){
		return this;
	}
}
