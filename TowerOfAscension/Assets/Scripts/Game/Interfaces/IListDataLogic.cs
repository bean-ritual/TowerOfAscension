using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IListDataLogic{
	bool DoData(Game game, Func<Data, bool> DoLogic);
	bool DoData(Game game, Func<Data, bool> DoLogic, out Data hit);
}
