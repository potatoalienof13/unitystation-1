﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Rightclick : MonoBehaviour {
	public static Rightclick ins;
	public bool Initialise = false;
	public List<Menu> options = new List<Menu>();

	public int NumberofMenus = new int ();
	public List<Sprite> Spritenames = new List<Sprite>(){
		
	};

	public List<string> names = new List<string>(){
		"Menu1",
		"Menu2",
		"Menu3",
		"Menu4",
		"Menu5",
		"Menu6",
	};

	public List<Color> Colours = new List<Color>(){
		Color.gray,
		Color.red,
		Color.blue,
		Color.black,
		Color.cyan,
		Color.green,
	};

	public List<int> submenuAmounts = new List<int>(){
		1,
		5,
		3,
		2,
		4,
		6
	};



	public class Menu {
		public Color colour;
		public Sprite sprite;
		public string FunctionType;
		public string title;
		public GameObject Item;
		public List<Menu> SubMenus = new List<Menu>();
	}
		

	void Awake(){
		ins = this;
	}


	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			Vector3 position = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
			position.z = 0f;
			List<GameObject> objects = UITileList.GetItemsAtPosition(position);

			Generate (objects);
			//Logger.Log ("yo", Category.UI);
			if (options.Count > 0){
				RadialMenuSpawner.ins.SpawnRadialMenu(this);
			}

		}
		
	}
	private void Generate(List<GameObject> objects){
		options = new List<Menu> ();


		for (int i = 0; i < objects.Count; i++) {
			Menu newMenu = new Menu();
			newMenu.colour = Color.gray;
			ItemAttributes Nameues = objects[i].GetComponent<ItemAttributes>();
			if (Nameues == null) {
				newMenu.title = ins.names[i];

			} else {
				newMenu.title = Nameues.itemName;
			}

			var UseSprite = objects[i].GetComponentInChildren<SpriteRenderer>();
			if (UseSprite == null) {
				newMenu.sprite = ins.Spritenames [i];
			} else {
				newMenu.sprite = UseSprite.sprite;
			}
			var PickUpTriggerues = objects[i].GetComponent<PickUpTrigger>();
			if (!(PickUpTriggerues == null)) {
				Menu NewSubMenu = new Menu();
				NewSubMenu.colour = Color.gray;
				NewSubMenu.FunctionType = "PickUpTrigger";
				NewSubMenu.Item = objects [i];			
				//NewSubMenu.title = "sub " + ins.names[L];
				NewSubMenu.sprite = ins.Spritenames[1];
				newMenu.SubMenus.Add (NewSubMenu);
			}

			for (int L = 0; L < 3; L++) {
				Menu NewSubMenu = new Menu();
				NewSubMenu.colour = Color.gray;
				//NewSubMenu.title = "sub " + ins.names[L];
				NewSubMenu.sprite = ins.Spritenames[0];
				newMenu.SubMenus.Add (NewSubMenu);
			}
			ins.options.Add (newMenu);
		}

	}
}

