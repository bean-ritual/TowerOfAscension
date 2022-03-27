using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridContentResizer : MonoBehaviour{
	private float _width;
	private float _height;
	[SerializeField]private GridLayoutGroup _grid;
	[SerializeField]private RectTransform _content;
	[SerializeField]private RectTransform _viewport;
	private void OnGUI(){
		Resize();
	}
	private void Resize(){
		_height = _viewport.rect.height;
		_width = CalculateCellsX() / CalculateCellsY();
		_content.sizeDelta = new Vector2(_width, _height);
	}
	private float CalculateCellsX(){
		float cellSize = _grid.cellSize.x + _grid.spacing.x;
		int children = _content.childCount;
		return children * cellSize + (_grid.padding.horizontal * 2);
	}
	private float CalculateCellsY(){
		float cellSize = _grid.cellSize.y + (_grid.spacing.y / 2f)+ (_grid.padding.vertical * 2);
		return (int)Mathf.Ceil(_height / cellSize);
	}
}
