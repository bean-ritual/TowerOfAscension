using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ITileLight{
	void SetLight(Game game, int lightLevel);
	int GetLight(Game game);
	void Discover(Game game);
	bool GetDiscovered(Game game);
}
