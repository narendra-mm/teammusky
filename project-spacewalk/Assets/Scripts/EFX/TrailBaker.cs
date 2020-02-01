using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBaker : MonoBehaviour
{
#pragma warning disable 649
	[SerializeField] private TrailRenderer _trail;
	[SerializeField] private MeshFilter _meshFilter;
	[SerializeField] private Camera _camera;
	[SerializeField] private bool _keepTransforms;
#pragma warning restore 649


	[ContextMenu("BakeMesh")]
	public void BakeTrailMesh()
	{
		_trail.BakeMesh(_meshFilter.mesh, _camera, _keepTransforms);
		_trail.Clear();
	}

	public void ClearBakedMesh()
	{
		_meshFilter.mesh = null;
	}
}
