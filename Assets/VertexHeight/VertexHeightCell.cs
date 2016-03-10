using UnityEngine;
using System.Collections;

public class VertexHeightCell : LifeCell {
	public Mesh mesh;

	override public void UpdateState(float value)
	{
		int index = GoLController.instance.GetIndexFromPosition(x, y);
		this.mesh.vertices[index].y = value * 0.5f;

		Vector3[] verticles = mesh.vertices;
		Vector3 v = this.mesh.vertices[index];
		v.y =  value * 1f;
		verticles[index] = v;
		this.mesh.vertices = verticles;
	}
}
