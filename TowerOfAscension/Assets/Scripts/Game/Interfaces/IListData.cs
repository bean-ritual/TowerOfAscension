using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IListData{
	void AddData(Game game, Data data);
	void RemoveData(Game game, Data data);
	Data GetData(Game game, int index);
	int GetDataCount();
}