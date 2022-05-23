using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IWorldPosition{
	void Spawn(Game game, int x, int y);
	void Despawn(Game game);
	void SetPosition(Game game, int x, int y);
	void ClearPosition(Game game);
	Map.Tile GetTile(Game game);
	Vector3 GetPosition(Game game);
}
