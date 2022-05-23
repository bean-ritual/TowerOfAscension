using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IDataTile{
	void SetData(Game game, Data data);
	Data GetData(Game game);
}
