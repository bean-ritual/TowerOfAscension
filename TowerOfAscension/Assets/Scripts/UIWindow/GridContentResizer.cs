using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridContentResizer : MonoBehaviour{
	[SerializeField]private GridLayoutGroup _grid;
	[SerializeField]private RectTransform _content;
	private void Update(){
		Resize();
	}
	private void Resize(){
		float width = CalculateCellWidth() / CalculateCellHeight();
		Vector2 delta = _content.sizeDelta;
		delta.x = width;
		_content.sizeDelta = delta;
	}
	private float CalculateCellWidth(){
		float width = _grid.cellSize.x + _grid.spacing.x + _grid.padding.horizontal;
		int children = _content.childCount;
		return children * width;
	}
	private int CalculateCellHeight(){
		float height = _grid.cellSize.y + _grid.spacing.y + _grid.padding.vertical;
		return (int)Mathf.Ceil(_content.rect.height / height);
	}
}
