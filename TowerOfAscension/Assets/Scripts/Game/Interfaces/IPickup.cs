using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IPickup{
	void Pickup(Game game, Data holder);
	void Drop(Game game);
}
