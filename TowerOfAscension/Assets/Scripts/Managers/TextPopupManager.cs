using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextPopupManager : MonoBehaviour{
	private static TextPopupManager _INSTANCE;
	[SerializeField]private int _sortingOrder;
	[SerializeField]private GameObject _prefabTextPopup;
	private void Awake(){
		if(_INSTANCE != null){
			Destroy(gameObject);
			return;
		}
		_INSTANCE = this;
	}
	public void PopText(string text, Vector3 position){
		Instantiate(
			_prefabTextPopup,
			position,
			Quaternion.identity,
			this.transform
		).GetComponent<TextPopup>().Setup(
			text,
			_sortingOrder
		);
	}
	public static TextPopupManager GetInstance(){
		return _INSTANCE;
	}
}
