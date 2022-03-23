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
	[field:NonSerialized]public event EventHandler<WorldUnit.UnitAnimateEventArgs> OnMoveAnimation;
	[field:NonSerialized]public event EventHandler<WorldUnit.UnitAnimateEventArgs> OnAttackAnimation;
	protected int uiSortingOrder = 100;
	protected AI _ai = AI.GetNullAI();
	protected Attribute _health = Attribute.GetNullAttribute();
	protected Attribute _armour = Attribute.GetNullAttribute();
	public AIUnit(){}
	public virtual Vector3 GetUIOffset(){
		return _UI_OFFSET;
	}
	public virtual int GetUISortingOrder(){
		return _sortingOrder + uiSortingOrder;
	}
	public virtual bool GetHealthBar(){
		return true;
	}
	public virtual void Move(Level level, Direction direction){
		Unit.Default_Move(this, level, direction);
	}
	public virtual void OnMove(Level level, Tile tile){
		tile.GetXY(out int x, out int y);
		Unit.Default_SetPosition(this, level, x, y, ref _x, ref _y);
		_ai.GetTurnControl().EndTurn(level, this);
		OnMoveAnimation?.Invoke(this, new WorldUnit.UnitAnimateEventArgs(this, level.GetWorldPosition(x, y)));
	}
	public virtual void SetAI(AI ai){
		_ai = ai;
	}
	public virtual AI GetAI(){
		return _ai;
	}
	public virtual void Attacked(Level level, Unit unit, int attack){
		_health.Damage(level, this, attack);
	}
	public virtual void Attack(Level level, Direction direction){
		direction.GetTile(level, _x, _y).GetAttackable().Attacked(level, this, 1);
	}
	public virtual void OnAttack(Level level, Tile tile){
		tile.GetXY(out int x, out int y);
		_ai.GetTurnControl().EndTurn(level, this);
		OnAttackAnimation?.Invoke(this, new WorldUnit.UnitAnimateEventArgs(this, (level.GetWorldPosition(x, y) + level.GetWorldPosition(_x, _y)) / 2));
	}
	public virtual void Kill(Level level){
		Default_Kill(this, level);
	}
	public virtual void OnKill(Level level){
		
	}
	public override bool Process(Level level){
		return _ai.Process(level, this);
	}
	public Attribute GetHealth(){
		return _health;
	}
	public Attribute GetArmour(){
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
