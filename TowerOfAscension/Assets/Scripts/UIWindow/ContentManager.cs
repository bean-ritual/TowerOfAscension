using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ContentManager : MonoBehaviour{
	[SerializeField]private RectTransform _content;
	public RectTransform GetContent(){
		return _content;
	}
}
