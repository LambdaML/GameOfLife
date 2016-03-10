using UnityEngine;
using System.Collections;

public class PixelCell : LifeCell {
	public Texture2D texture;
	public int size;
	override public void UpdateState(float value)
	{
		if (value == 1) {
			for (int tx = x * size; tx < x * size + size; tx++) {
				for (int ty = y * size; ty < y * size + size; ty++) {
					texture.SetPixel(tx, ty, Color.white);
				}
			}
		} else {
			for (int tx = x * size; tx < x * size + size; tx++) {
				for (int ty = y * size; ty < y * size + size; ty++) {
					texture.SetPixel(tx, ty, Color.black);
				}
			}
		}
	}
}