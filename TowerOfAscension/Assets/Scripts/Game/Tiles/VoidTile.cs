using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class VoidTile : Tile.NullTile{
	public VoidTile(int x, int y){
		_x = x;
		_y = y;
	}
	public override void GetXY(out int x, out int y){
		x = _x;
		y = _y;
	}
	public override void Print(Level level, ClassicGen master, BluePrint.Print print, Tile tile){
		level.Set(_x, _y, tile);
		print.OnSpawn(level, master, tile);
	}
	public override bool Check(Level level, ClassicGen master){
		return true;
	}
	public override Tile.IPrintable GetPrintable(){
		return this;
	}
}
