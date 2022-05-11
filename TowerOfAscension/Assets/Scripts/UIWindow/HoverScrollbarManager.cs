using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class HoverScrollbarManager : 
	MonoBehaviour,
	IPointerEnterHandler,
	IPointerExitHandler
	{
	[SerializeField]private GameObject _scrollbar;
	private void Awake(){
		HideScrollbar();
	}
	public void OnPointerEnter(PointerEventData eventData){
		ShowScrollbar();
	}
	public void OnPointerExit(PointerEventData eventData){
		HideScrollbar();
	}
	public void ShowScrollbar(){
		const bool ENTER = true;
		_scrollbar.SetActive(ENTER);
	}
	public void HideScrollbar(){
		const bool EXIT = false;
		_scrollbar.SetActive(EXIT);
	}
}
