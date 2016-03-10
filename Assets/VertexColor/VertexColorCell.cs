using UnityEngine;
using System.Collections;

public class VertexColorCell : LifeCell {
	public Mesh mesh;

	void Start () {
	}

	void Update () {
		/*
		Mesh mesh = meshFilter.mesh;

		Vector3[] vs = mesh.vertices;
		vs[1].y = (Time.time % 1);
		mesh.vertices = vs;

		Color[] colors = new Color[mesh.vertices.Length];
		colors[1] = new Color(Time.time % 1.0f, 0f, 0f, 1f);
		mesh.colors = colors;
		*/
	}

	override public void UpdateState(float value)
	{
		int index = GoLController.instance.GetIndexFromPosition(x, y);

		Vector3[] verticles = mesh.vertices;
		Vector3 v = this.mesh.vertices[index];
		v.y =  value * .5f;
		verticles[index] = v;
		this.mesh.vertices = verticles;

		Color[] colors = mesh.colors;
		colors[index] = new Color(value * 1f, 0f, 0f, 0.7f);
		this.mesh.colors = colors;
	}
}