using UnityEngine;
using System.Collections.Generic;

public class ViewMesh : MonoBehaviour {

	public int resolution = 16;
	public LayerMask mask;
	public float edgeDistanceThreshold = .5f;
	public int edgeSolverIterations = 4;

	MeshFilter filter;
	new Cam camera;
	Mesh mesh;

	void Awake() {
		filter = GetComponent<MeshFilter>();
		camera = GetComponentInParent<Cam>();
		mesh = new Mesh();
		mesh.name = "view mesh";
		filter.mesh = mesh;
	}

	void Update() {
		UpdateMesh();
	}

	void UpdateMesh() {
		//find thaose damn verts
		float inc = (camera.viewAngle * 2) / (resolution + 1);
		List<Vector3> vertList = new List<Vector3>();
		float maxDist = 5000;
		for (float angle = -camera.viewAngle; angle < camera.viewAngle; angle += inc) {
			Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector3.right;
			RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.rotation * dir, maxDist, mask);
			if (hit.collider == null) {
				hit.point = transform.position + (transform.rotation * dir) * maxDist;
				hit.distance = maxDist;
			}
			Debug.DrawLine(transform.position, hit.point, Color.white);
			vertList.Add(transform.InverseTransformPoint(hit.point));
		}
		//mesh generation: simple
		Vector3[] verts = new Vector3[vertList.Count + 1];
		int[] tris = new int[(verts.Length - 2) * 3];
		verts[0] = Vector3.zero;
		int i = 1;
		int t = 0;
		foreach (Vector3 vert in vertList){
			if (i >= 2) {
				tris[t ++] = i;
				tris[t ++] = i - 1;
				tris[t ++] = 0;
			}
			verts[i ++] = vert;
		}
		mesh.vertices = verts;
		mesh.triangles = tris;
	}

}