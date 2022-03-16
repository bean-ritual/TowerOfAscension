using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PlayerControl : AI{
	[field:NonSerialized]public static event EventHandler<OnPlayerControlEventArgs> OnPlayerControl;
	public class OnPlayerControlEventArgs : EventArgs{
		public Unit player;
		public OnPlayerControlEventArgs(Unit player){
			this.player = player;
		}
	}
	public override bool Process(Level level, Unit self){
		return level.NextTurn();
	}
}
