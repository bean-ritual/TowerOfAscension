using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ResizeWindowManager : 
	MonoBehaviour, 
	IDragHandler, 
	IPointerDownHandler
	{
	public enum ResizeDirection{
		Left,
		Right,
		Top,
		Bottom,
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight,
	};
	private static readonly Vector2[] _dragConstaints = {
		new Vector2(1, 0),
		new Vector2(0, 1),
		new Vector2(1, 1),
		new Vector2(-1, 0),
		new Vector2(0, -1),
		new Vector2(-1, -1),
		new Vector2(-1, 1),
		new Vector2(1, -1),
	};
	[SerializeField]private ResizeDirection _direction;
	[SerializeField]private UIWindowManager _window;
	public void OnDrag(PointerEventData eventData){
		_window.ResizeDeltaWindow((eventData.delta * GetDragConstaints(_direction)), GetAnchorForResize(_direction));
	}
	public void OnPointerDown(PointerEventData eventData){
		_window.SendWindowToFront();
	}
	public static UIWindowManager.UIAnchor GetAnchorForResize(ResizeDirection resize){
		switch(resize){
			default: return UIWindowManager.UIAnchor.BottomLeft;
			case ResizeDirection.Left: return UIWindowManager.UIAnchor.MiddleRight;
			case ResizeDirection.Right: return UIWindowManager.UIAnchor.MiddleLeft;
			case ResizeDirection.Top: return UIWindowManager.UIAnchor.BottomMiddle;
			case ResizeDirection.Bottom: return UIWindowManager.UIAnchor.TopMiddle;
			case ResizeDirection.TopLeft: return UIWindowManager.UIAnchor.BottomRight;
			case ResizeDirection.TopRight: return UIWindowManager.UIAnchor.BottomLeft;
			case ResizeDirection.BottomLeft: return UIWindowManager.UIAnchor.TopRight;
			case ResizeDirection.BottomRight: return UIWindowManager.UIAnchor.TopLeft;
		}
	}
	private Vector2 GetDragConstaints(ResizeDirection resize){
		switch(resize){
			default: return _dragConstaints[2];
			case ResizeDirection.Left: return _dragConstaints[3];
			case ResizeDirection.Right: return _dragConstaints[0];
			case ResizeDirection.Top: return _dragConstaints[1];
			case ResizeDirection.Bottom: return _dragConstaints[4];
			case ResizeDirection.TopLeft: return _dragConstaints[6];
			case ResizeDirection.TopRight: return _dragConstaints[2];
			case ResizeDirection.BottomLeft: return _dragConstaints[5];
			case ResizeDirection.BottomRight: return _dragConstaints[7];
		}
	}
}
