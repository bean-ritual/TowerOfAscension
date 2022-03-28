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
	public override bool Process(Game game, Unit self){
		OnPlayerControl?.Invoke(this, new OnPlayerControlEventArgs(self));
		switch(_state){
			default: return game.GetLevel().NextTurn();
			case ControlState.Null: return game.GetLevel().NextTurn();
			case ControlState.Locked:{
				StartTurn(game, self);
				return true;
			}
			case ControlState.Input: return false;
		}
	}
	public virtual void StartTurn(Game game, Unit self){
		if(_state != ControlState.Input){
			_state = ControlState.Input;
		}
	}
	public virtual void EndTurn(Game game, Unit self){
		if(_state == ControlState.Input){
			_state = ControlState.Locked;
			game.GetLevel().LightUpdate(game, self);
			game.GetLevel().NextTurn();
		}
	}
	public override ITurnControl GetTurnControl(){
		return this;
	}
}
