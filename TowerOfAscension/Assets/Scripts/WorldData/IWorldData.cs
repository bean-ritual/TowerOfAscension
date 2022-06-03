using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IWorldData{
	void Setup(int id);
	void SetData(Data data);
	void PlayAnimation(WorldAnimation animation);
	void SetPosition(Vector3 position);
	Vector3 GetPosition();
	void SetSprite(int sprite);
	void SetSortingOrder(int sortingOrder);
	void SetVisible(bool visible);
	void PlayCoroutine(IEnumerator coroutine);
	void Disassemble();
	int GetID();
}
