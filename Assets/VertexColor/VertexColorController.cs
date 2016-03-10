using UnityEngine;
using System.Collections;

public class VertexColorController : GoLController {
	public MeshFilter meshFilter; 
	public float resolution = 1f;

	override internal void Prepare()
	{
		base.Prepare();

		meshFilter.mesh.Clear();

		float maxX = (col - 1)/(resolution*2);
		float maxZ = (row - 1)/(resolution*2);
		int width = (int)(maxX*2 * resolution);
		int height = (int)(maxZ*2 * resolution);
		Vector3[] vertices = new Vector3[(width + 1) * (height + 1)];
		int[] triangles = new int[width * height * 6];
		int c = 0,t = 0;
		float offset = 1f/resolution;
		for(float z = -maxZ; z <= maxZ; z+= offset) {
			for(float x = -maxX; x <= maxX; x+= offset) {
				vertices[ c ] = new Vector3(x, 0f, z);
				if (-maxZ < z) {
					if (x < maxX) {
						triangles[t++] = c;
						triangles[t++] = c + 1;
						triangles[t++] = c - (width + 1);
					}
					if (-maxX < x) {
						triangles[t++] = c;
						triangles[t++] = c - (width + 1);
						triangles[t++] = c - (width + 1) - 1;
					}
				}
				c++;
			}
		}
		meshFilter.mesh.vertices = vertices;
		meshFilter.mesh.triangles = triangles;

		Vector3[] normales = new Vector3[ vertices.Length ];
		Vector2[] uvs = new Vector2[ vertices.Length ];
		for( int n = 0; n < normales.Length; n++) {
			normales[n] = Vector3.up;
			uvs[n] = Vector2.zero;
		}
		meshFilter.mesh.normals = normales;

		Color[] colors = new Color[vertices.Length];
		meshFilter.mesh.colors = colors;
	}

	override internal void SetUpCell(LifeCell cell)
	{
		base.SetUpCell(cell);
		((VertexColorCell)cell).mesh = meshFilter.mesh;
	}
}
