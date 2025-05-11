using UnityEngine;
using System.Collections;

public class Billboard2 : MonoBehaviour 
{
	public Transform	cameraTransform;
	public bool		flipFacing;

	// Use this for initialization
	void Start () 
	{
		cameraTransform = Camera.main.transform;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		transform.rotation = cameraTransform.rotation;
	}
}
