using System;
using UnityEngine;

// Token: 0x0200014E RID: 334
[ExecuteInEditMode]
public class PutMeshVertsScript : MonoBehaviour
{
	// Token: 0x060009A1 RID: 2465 RVA: 0x0004B5B0 File Offset: 0x000497B0
	private void Start()
	{
		Mesh sharedMesh = base.gameObject.GetComponent<MeshFilter>().sharedMesh;
		sharedMesh.colors = new Color[0];
		Debug.Log("Vertices: " + sharedMesh.vertexCount);
		Debug.Log("Colors: " + sharedMesh.colors.Length);
		Debug.Log("Normals: " + sharedMesh.normals.Length);
		Debug.Log("Tangents: " + sharedMesh.tangents.Length);
		Debug.Log("Uvs: " + sharedMesh.uv.Length);
		Debug.Log("Uv1s: " + sharedMesh.uv1.Length);
		Debug.Log("Uv2s: " + sharedMesh.uv2.Length);
	}
}
