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
	Unit.ITaggable,
	Unit.ITileable
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
	public void Add(Register<Unit> register){
		register.Add(this, ref _id);
	}
	public void Remove(Register<Unit> register){
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
	public VisualController GetVisualController(Game game){
		return game.GetPlayer().GetVisualController().GetVisualController(game);
	}
	public bool GetWorldVisibility(Game game){
		return game.GetPlayer().GetVisualController().GetWorldVisibility(game);
	}
	public void SetPosition(Game game, int x, int y, int moveSpeed = 0){
		game.GetPlayer().GetPositionable().SetPosition(game, x, y, moveSpeed);
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
		return game.GetPlayer().GetTileable().GetTile(game);
	}
	public Tile GetTileFrom(Game game, int x, int y){
		return game.GetPlayer().GetTileable().GetTileFrom(game, x, y);
	}
	public Register<Unit>.ID GetID(){
		return _id;
	}
	public override bool Process(Game game){
		return game.GetPlayer().Process(game);
	}
	//
	public override Unit.IPlayable GetPlayable(){
		return this;
	}
	public override Unit.ITaggable GetTaggable(){
		return this;
	}
	public override Unit.ITileable GetTileable(){
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
}
