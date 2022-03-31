using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class UIButton : 
	MonoBehaviour,
	IPointerEnterHandler,
	IPointerExitHandler,
	IPointerClickHandler
	{
	[SerializeField]private Image _background;
	[SerializeField]private TextMeshProUGUI _text;
	public void OnPointerEnter(PointerEventData eventData){
		
	}
	public void OnPointerExit(PointerEventData eventData){
		//_background.color = Color.black;
		//_text.color = Color.white;
	}
	public void OnPointerClick(PointerEventData eventData){
		_background.color = Color.white;
		_text.color = Color.black;
	}
}
