using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IKillable{
	void Kill(Game game);
	void SetKiller(Game game, Data killer);
}
