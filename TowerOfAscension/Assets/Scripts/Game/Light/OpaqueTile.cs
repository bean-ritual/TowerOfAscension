using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class OpaqueTile : TileLight{
	public OpaqueTile() : base(){}
	public override bool CanOpaque(Game game){
		return true;
	}
}
