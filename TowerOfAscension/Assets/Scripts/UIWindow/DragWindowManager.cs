using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragWindowManager : 
	MonoBehaviour, 
	IDragHandler, 
	IPointerDownHandler
	{
	[SerializeField]private UIWindowManager _window;
	public void OnDrag(PointerEventData eventData){
		_window.SetDeltaPosition(eventData.delta);
	}
	public void OnPointerDown(PointerEventData eventData){
		_window.SendWindowToFront();
	}
}
