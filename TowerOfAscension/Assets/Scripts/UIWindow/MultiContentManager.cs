using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MultiContentManager : MonoBehaviour{
	[SerializeField]private RectTransform _leftContent;
	[SerializeField]private RectTransform _rightContent;
	public RectTransform GetLeftContent(){
		return _leftContent;
	}
	public RectTransform GetRightContent(){
		return _rightContent;
	}
}
