using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Discoverer : 
	Tag,
	Tag.IInput<Tile>
	{
	private static Tag.ID _TAG_ID = Tag.ID.Discoverer;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void Input(Game game, Unit self, Tile tile){
		tile.GetDiscoverable().Discover(game, self);
	}
	public override Tag.IInput<Tile> GetIInputTile(){
		return this;
	} 
	public static Tag Create(){
		return new Discoverer();
	}
}
