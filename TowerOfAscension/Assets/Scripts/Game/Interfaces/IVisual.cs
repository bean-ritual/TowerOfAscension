using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IVisual{
	void PlayAnimation(Game game, WorldAnimation animation);
	int GetSprite(Game game);
	int GetSortingOrder(Game game);
	bool GetRender(Game game);
}
