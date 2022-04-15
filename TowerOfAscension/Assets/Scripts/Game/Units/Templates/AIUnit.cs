using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class AIUnit : 
	LevelUnit,
	Unit.IMoveable,
	Unit.IControllable,
	Unit.IAttacker,
	Unit.IAttackable,
	Unit.IKillable
	{
	protected AI _ai = AI.GetNullAI();
	public AIUnit(){}
	public virtual void Move(Game game, Direction direction){
		Unit.Default_Move(this, game, direction);
	}
	public virtual void OnMove(Game game, Tile tile){
		tile.GetXY(out int x, out int y);
		Unit.Default_SetPosition(this, game, x, y, ref _x, ref _y, 1);
		_ai.GetTurnControl().EndTurn(game, this);
	}
	public virtual void SetAI(Game game, AI ai){
		_ai = ai;
	}
	public virtual AI GetAI(Game game){
		return _ai;
	}
	public virtual void TryAttack(Game game, Direction direction){
		Unit attack = GetHasInventory().GetInventory(game).GetWeaponEquippable().GetWeapon(game, this);
		if(attack.IsNull()){
			attack = this;
		}
		direction.GetTileFromUnit(game, this).GetAttackable().Attack(game, this, attack);
		_ai.GetTurnControl().EndTurn(game, this);
	}
	public virtual void DoAttack(Game game, Unit skills, Unit target){
		//target.GetHasHealth().GetHealth(game).Damage(game, target, 1);
		//_controller.InvokeAttackAnimation((GetPositionable().GetPosition(game) + target.GetPositionable().GetPosition(game)) / 2);
	}
	public virtual void CheckAttack(Game game, Unit skills, Unit attack){
		attack.GetAttacker().DoAttack(game, skills, this);
	}
	public virtual void Kill(Game game){
		Default_Kill(this, game);
	}
	public virtual void OnKill(Game game){
		
	}
	public override bool Process(Game game){
		return _ai.Process(game, this);
	}
	public override Unit.IMoveable GetMoveable(){
		return this;
	}
	public override Unit.IControllable GetControllable(){
		return this;
	}
	public override Unit.IAttacker GetAttacker(){
		return this;
	}
	public override Unit.IAttackable GetAttackable(){
		return this;
	}
	public override IKillable GetKillable(){
		return this;
	}
}
