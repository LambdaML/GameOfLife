using UnityEngine;
using System.Collections;

public class PixelController : GoLController {
	public SpriteRenderer spriteRenderer; 
	private Texture2D texture;
	public float resolution = 1f;
	public int pixelPerUnit = 100;

	public int size = 1;

	override internal void Prepare()
	{
		base.Prepare();
		texture = new Texture2D(col * size, row * size, TextureFormat.RGBA32, false, false);
		Sprite sprite = Sprite.Create(texture, new Rect(0, 0, col * size, row * size), new Vector2(0.5f, 0.5f), pixelPerUnit);
		spriteRenderer.sprite = sprite;

		Camera.main.orthographicSize = size;
	}

	override internal void CreateCells()
	{
		base.CreateCells();
		texture.Apply();
	}
		
	override internal void ChangeState()
	{
		base.ChangeState();
		texture.Apply();
	}

	override internal void SetUpCell(LifeCell cell)
	{
		base.SetUpCell(cell);
		((PixelCell)cell).texture = texture;
		((PixelCell)cell).size = size;
	}
}
