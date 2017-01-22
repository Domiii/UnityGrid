using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DraggableSprite : MonoBehaviour {
	public SpriteRenderer sprite;

	private bool mouseDown = false;
	private Vector3 dragOffset;
	private bool restrictX;
	private bool restrictY;
	private float fakeX;
	private float fakeY;
	private float myWidth;
	private float myHeight;

	void Start()
	{
		if (sprite == null) {
			sprite = GetComponent<SpriteRenderer> ();
		}
		if (sprite == null) {
			Debug.LogError ("sprite not assigned in DraggableSprite", this);
			return;
		}
		SnapToGrid ();
	}


	void OnMouseDown() 
	{
		mouseDown = true;
		var mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		print (mousePos - transform.position);
		dragOffset = (transform.position - sprite.bounds.min) - BuildingGrid.Instance.SnapToGridFloor (mousePos - sprite.bounds.min + Vector3.one * 0.01f);
	}

	void OnMouseUp() 
	{
		mouseDown = false;
	}

	void SnapToGrid() {
		BuildingGrid.Instance.SnapToGrid (sprite);
	}


	void Update () 
	{
		if (mouseDown) {
			var mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position = mousePos + dragOffset;
			SnapToGrid ();
		}
	}
}