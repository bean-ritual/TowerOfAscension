using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class HeroWorldVisual :
	WorldVisualAnimations{
	public override bool Check(Game game, Unit self){
		return true;
	}
}
