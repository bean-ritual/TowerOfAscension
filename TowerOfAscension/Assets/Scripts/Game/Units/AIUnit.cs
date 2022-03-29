using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class AIUnit : 
	LevelUnit,
	WorldUnit.IWorldUnitUI,
	WorldUnit.IWorldUnitAnimations,
	Unit.IMoveable,
	Unit.IControllable,
	Unit.IAttackable,
	Unit.IAttacker,
	Unit.IKillable,
	Health.IHasHealth,
	Armour.IHasArmour
	{
	private static readonly Vector3 _UI_OFFSET = new Vector3(0, 0.9f);
	[field:NonSerialized]public	event EventHandler<EventArgs> OnWorldUnitUIUpdate;
	[field:NonSerialized]public event EventHandler<WorldUnit.UnitAnimateEventArgs> OnAttackAnimation;
	protected int uiSortingOrder = 100;
	protected AI _ai = AI.GetNullAI();
	protected Attribute _health = Attribute.GetNullAttribute();
	protected Attribute _armour = Attribute.GetNullAttribute();
	public AIUnit(){}
	public virtual Vector3 GetUIOffset(Game game){
		return _UI_OFFSET;
	}
	public virtual int GetUISortingOrder(Game game){
		return _controller.GetSortingOrder() + uiSortingOrder;
	}
	public virtual bool GetHealthBar(Game game){
		return true;
	}
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
	public virtual void Attacked(Game game, Unit unit, int attack){
		_health.Damage(game, this, attack);
	}
	public virtual void Attack(Game game, Direction direction){
		direction.GetTile(game.GetLevel(), _x, _y).GetAttackable().Attacked(game, this, 1);
	}
	public virtual void OnAttack(Game game, Tile tile){
		tile.GetXY(out int x, out int y);
		_ai.GetTurnControl().EndTurn(game, this);
		OnAttackAnimation?.Invoke(this, new WorldUnit.UnitAnimateEventArgs(this, (game.GetLevel().GetWorldPosition(x, y) + game.GetLevel().GetWorldPosition(_x, _y)) / 2));
	}
	public virtual void Kill(Game game){
		Default_Kill(this, game);
	}
	public virtual void OnKill(Game game){
		
	}
	public override bool Process(Game game){
		return _ai.Process(game, this);
	}
	public Attribute GetHealth(Game game){
		return _health;
	}
	public Attribute GetArmour(Game game){
		return _armour;
	}
	public override WorldUnit.IWorldUnitUI GetWorldUnitUI(){
		return this;
	}
	public override WorldUnit.IWorldUnitAnimations GetWorldUnitAnimations(){
		return this;
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
	public override Health.IHasHealth GetHasHealth(){
		return this;
	}
	public override Armour.IHasArmour GetHasArmour(){
		return this;
	}
}
