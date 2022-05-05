using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Player :
	Unit,
	Unit.IPlayable
	{
	public Player(){}
	//
	//
	public void SetPlayer(Game game){
		game.GetPlayer().GetProxyable().SetProxyID(game, _id);
	}
	public void RemovePlayer(Game game){
		game.GetPlayer().GetProxyable().RemoveProxyID(game);
	}
	public override void AddTag(Game game, Tag tag){
		game.GetPlayer().AddTag(game, tag);
	}
	public override void RemoveTag(Game game, Tag.ID id){
		game.GetPlayer().RemoveTag(game, id);
	}
	public override Tag GetTag(Game game, Tag.ID id){
		return game.GetPlayer().GetTag(game, id);
	}
	public override void UpdateAllTags(Game game){
		game.GetPlayer().UpdateAllTags(game);
	}
	public override bool Process(Game game){
		return game.GetPlayer().Process(game);
	}
	//
	public override Unit.IPlayable GetPlayable(){
		return this;
	}
}
