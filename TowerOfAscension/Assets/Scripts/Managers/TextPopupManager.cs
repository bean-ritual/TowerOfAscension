using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextPopupManager : 
	MonoBehaviour,
	TextPopupManager.ITextPopupManager
	{
	public interface ITextPopupManager{
		void PopText(string text, Vector3 position);
		void PopText(string text, Vector3 position, int colour);
	}
	[Serializable]
	public class NullTextPopupManager : ITextPopupManager{
		public void PopText(string text, Vector3 position){}
		public void PopText(string text, Vector3 position, int colour){}
	}
	private static TextPopupManager _INSTANCE;
	[SerializeField]private GameObject _prefabTextPopup;
	private void Awake(){
		if(_INSTANCE == null){
			_INSTANCE = this;
		}else{
			Destroy(gameObject);
		}
	}
	public void PopText(string text, Vector3 position){
		Instantiate(
			_prefabTextPopup,
			position,
			Quaternion.identity,
			this.transform
		).GetComponent<TextPopup>().Setup(
			text
		);
	}
	public void PopText(string text, Vector3 position, int colour){
		Instantiate(
			_prefabTextPopup,
			position,
			Quaternion.identity,
			this.transform
		).GetComponent<TextPopup>().Setup(
			text,
			colour
		);
	}
	private static NullTextPopupManager _NULL_TEXT_POPUP_MANAGER = new NullTextPopupManager();
	public static ITextPopupManager GetInstance(){
		if(_INSTANCE == null){
			return _NULL_TEXT_POPUP_MANAGER;
		}else{
			return _INSTANCE;
		}
	}
}
