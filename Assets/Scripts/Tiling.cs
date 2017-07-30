using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tiling : MonoBehaviour {

	public Sprite[] sprites;
	SpriteRenderer rend;

	public void SetBitMask(int mask){
		rend = GetComponent<SpriteRenderer> ();
		rend.sprite = sprites[mask];
	}
}
