using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class AIUnit : 
	LevelUnit,
	Unit.IMoveable,
	Unit.IControllable
	{
	[field:NonSerialized]public event EventHandler<EventArgs> OnMoveEvent;
	protected AI _ai = AI.GetNullAI();
	public AIUnit(){}
	public void Move(Level level, Direction direction){
		Unit.Default_Move(this, level, direction);
	}
	public void OnMove(Level level, Tile tile){
		tile.GetXY(out int x, out int y);
		Unit.Default_SetPosition(this, level, x, y, ref _x, ref _y);
		_ai.GetTurnControl().EndTurn(level, this);
		OnMoveEvent?.Invoke(this, EventArgs.Empty);
	}
	public void SetAI(AI ai){
		_ai = ai;
	}
	public AI GetAI(){
		return _ai;
	}
	public override bool Process(Level level){
		return _ai.Process(level, this);
	}
	public override Unit.IMoveable GetMoveable(){
		return this;
	}
	public override Unit.IControllable GetControllable(){
		return this;
	}
}
