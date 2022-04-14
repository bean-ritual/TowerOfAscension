using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Player :
	Unit,
	VisualController.IVisualController,
	Unit.ISpawnable,
	Unit.IPlayable,
	Unit.IProcessable,
	Unit.ITaggable,
	Unit.ICollideable,
	Unit.IMoveable,
	Unit.IControllable,
	Unit.IAttacker,
	Unit.IAttackable,
	Unit.IKillable,
	Unit.IDiscoverer,
	Unit.IInteractor,
	Unit.IHostileTarget,
	Unit.IExitable,
	Unit.IHasInventory,
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
	public void AddTag(Game game, Tag tag){
		game.GetPlayer().GetTaggable().AddTag(game, tag);
	}
	public void RemoveTag(Game game, Tag.ID id){
		game.GetPlayer().GetTaggable().RemoveTag(game, id);
	}
	public Tag GetTag(Game game, Tag.ID id){
		return game.GetPlayer().GetTaggable().GetTag(game, id);
	}
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
	public void TryAttack(Game game, Direction direction){
		game.GetPlayer().GetAttacker().TryAttack(game, direction);
	}
	public void DoAttack(Game game, Unit skills, Unit target){
		game.GetPlayer().GetAttacker().DoAttack(game, skills, target);
	}
	public void CheckAttack(Game game, Unit skills, Unit attack){
		game.GetPlayer().GetAttackable().CheckAttack(game, skills, attack);
	}
	public void Kill(Game game){
		game.GetPlayer().GetKillable().Kill(game);
	}
	public void OnKill(Game game){
		game.GetPlayer().GetKillable().OnKill(game);
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
	public override Unit.ITaggable GetTaggable(){
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
