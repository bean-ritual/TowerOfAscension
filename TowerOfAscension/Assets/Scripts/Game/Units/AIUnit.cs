using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class AIUnit : 
	LevelUnit,
	Unit.IMoveable,
	Unit.IControllable,
	Unit.IAttackable,
	Unit.IAttacker,
	Unit.IKillable
	{
	[field:NonSerialized]public event EventHandler<EventArgs> OnMoveEvent;
	protected AI _ai = AI.GetNullAI();
	public AIUnit(){}
	public virtual void Move(Level level, Direction direction){
		Unit.Default_Move(this, level, direction);
	}
	public virtual void OnMove(Level level, Tile tile){
		tile.GetXY(out int x, out int y);
		Unit.Default_SetPosition(this, level, x, y, ref _x, ref _y);
		_ai.GetTurnControl().EndTurn(level, this);
		OnMoveEvent?.Invoke(this, EventArgs.Empty);
	}
	public virtual void SetAI(AI ai){
		_ai = ai;
	}
	public virtual AI GetAI(){
		return _ai;
	}
	public virtual void Attacked(Level level, Unit unit, int attack){
		GetKillable().Kill(level);
	}
	public virtual void OnAttacked(){
		//
	}
	public virtual void Attack(Level level, Direction direction){
		direction.GetTile(level, _x, _y).GetAttackable().Attacked(level, this, 10);
	}
	public virtual void OnAttack(){
		
	}
	public virtual void Kill(Level level){
		Default_Kill(this, level);
	}
	public virtual void OnKill(Level level){
		
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
	public override Unit.IAttackable GetAttackable(){
		return this;
	}
	public override Unit.IAttacker GetAttacker(){
		return this;
	}
	public override IKillable GetKillable(){
		return this;
	}
}
