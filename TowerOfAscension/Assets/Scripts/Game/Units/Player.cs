using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player :
	Unit,
	VisualController.IVisualController,
	Unit.ISpawnable,
	Unit.IPlayable,
	Unit.IProcessable,
	Unit.ICollideable,
	Unit.IMoveable,
	Unit.IControllable,
	Unit.IAttackable,
	Unit.IAttacker,
	Unit.IKillable,
	Unit.IDiscoverer,
	Unit.IInteractor,
	Unit.IHostileTarget,
	Unit.IExitable,
	Unit.IHasInventory,
	Health.IHasHealth,
	Armour.IHasArmour,
	Level.ILightSource
	{
	private Register<Unit>.ID _id = Register<Unit>.ID.GetNullID();
	public Player(){}
	//
	public void Spawn(Game game, int x, int y){
		Unit.Default_Spawn(this, game, x, y);
	}
	public void Despawn(Game game){
		Unit.Default_Despawn(this, game);
	}
	public void SetPlayer(Game game){
		Unit.Default_SetPlayer(game, ref _id);
	}
	public void RemovePlayer(Game game){
		Unit.Default_RemovePlayer(game);
	}
	public void AddToRegister(Register<Unit> register){
		register.Add(this, ref _id);
	}
	public void RemoveFromRegister(Register<Unit> register){
		register.Remove(_id);
	}
	//
	public void Discover(Game game, Tile tile){
		game.GetPlayer().GetDiscoverer().Discover(game, tile);
	}
	public void Interact(Game game, Direction direction){
		game.GetPlayer().GetInteractor().Interact(game, direction);
	}
	public bool CheckHostility(Game game, Unit unit){
		return game.GetPlayer().GetHostileTarget().CheckHostility(game, unit);
	}
	public void Exit(Game game){
		game.GetPlayer().GetExitable().Exit(game);
	}
	public Inventory GetInventory(Game game){
		return game.GetPlayer().GetHasInventory().GetInventory(game);
	}
	public int GetLightRange(Game game){
		return game.GetPlayer().GetLightSource().GetLightRange(game);
	}
	public void Move(Game game, Direction direction){
		game.GetPlayer().GetMoveable().Move(game, direction);
	}
	public void OnMove(Game game, Tile tile){
		game.GetPlayer().GetMoveable().OnMove(game, tile);
	}
	public void SetAI(Game game, AI ai){
		game.GetPlayer().GetControllable().SetAI(game, ai);
	}
	public AI GetAI(Game game){
		return game.GetPlayer().GetControllable().GetAI(game);
	}
	public void Attacked(Game game, Unit unit, int attack){
		game.GetPlayer().GetAttackable().Attacked(game, unit, attack);
	}
	public void Attack(Game game, Direction direction){
		game.GetPlayer().GetAttacker().Attack(game, direction);
	}
	public void OnAttack(Game game, Tile tile){
		game.GetPlayer().GetAttacker().OnAttack(game, tile);
	}
	public void Kill(Game game){
		game.GetPlayer().GetKillable().Kill(game);
	}
	public void OnKill(Game game){
		game.GetPlayer().GetKillable().OnKill(game);
	}
	public Attribute GetHealth(Game game){
		return game.GetPlayer().GetHasHealth().GetHealth(game);
	}
	public Attribute GetArmour(Game game){
		return game.GetPlayer().GetHasArmour().GetArmour(game);
	}
	public VisualController GetVisualController(Game game){
		return game.GetPlayer().GetVisualController().GetVisualController(game);
	}
	public bool GetWorldVisibility(Game game){
		return game.GetPlayer().GetVisualController().GetWorldVisibility(game);
	}
	public void SetPosition(Game game, int x, int y){
		game.GetPlayer().GetPositionable().SetPosition(game, x, y);
	}
	public void GetPosition(Game game, out int x, out int y){
		game.GetPlayer().GetPositionable().GetPosition(game, out x, out y);
	}
	public void RemovePosition(Game game){
		game.GetPlayer().GetPositionable().RemovePosition(game);
	}
	public Vector3 GetPosition(Game game){
		return game.GetPlayer().GetPositionable().GetPosition(game);
	}
	public Tile GetTile(Game game){
		return game.GetPlayer().GetPositionable().GetTile(game);
	}
	public Register<Unit>.ID GetID(){
		return _id;
	}
	public bool Process(Game game){
		return game.GetPlayer().GetProcessable().Process(game);
	}
	public bool CheckCollision(Game game, Unit check){
		return game.GetPlayer().GetCollideable().CheckCollision(game, check);
	}
	//
	public override Unit.IPlayable GetPlayable(){
		return this;
	}
	public override Unit.IDiscoverer GetDiscoverer(){
		return this;
	}
	public override Unit.IInteractor GetInteractor(){
		return this;
	}
	public override Unit.IHostileTarget GetHostileTarget(){
		return this;
	}
	public override Unit.IExitable GetExitable(){
		return this;
	}
	public override Unit.IHasInventory GetHasInventory(){
		return this;
	}
	public override Level.ILightSource GetLightSource(){
		return this;
	}
	public override VisualController.IVisualController GetVisualController(){
		return this;
	}
	public override Unit.ISpawnable GetSpawnable(){
		return this;
	}
	public override Unit.IPositionable GetPositionable(){
		return this;
	}
	public override Register<Unit>.IRegisterable GetRegisterable(){
		return this;
	}
	public override IProcessable GetProcessable(){
		return this;
	}
	public override Unit.ICollideable GetCollideable(){
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
