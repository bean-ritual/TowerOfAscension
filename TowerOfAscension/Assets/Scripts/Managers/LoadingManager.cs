using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LoadingManager : MonoBehaviour{
	private bool _update = false;
	private void Update(){
		if(!_update){
			_update = true;
			return;
		}
		LoadSystem.LoadCallback();
	}
}
