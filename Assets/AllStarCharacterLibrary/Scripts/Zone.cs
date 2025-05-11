using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Zone : MonoBehaviour 
{
	public Game 				game;
	public bool 				world;
	public int 					index;
	public List<Zone> 			zones;
	public List<envVars>		envs;
	public List<Collider>		jumpTargets;
	public List<Collider>		platforms;
	public List<Collider>		floors;
	public	Transform			player;
	public ASCLBasicController 	abc;
	public Collider 			zonecollider;

	void Start()
	{

		zonecollider = gameObject.GetComponent<Collider>();
		zonecollider.enabled=false;

	}

	void Update()
	{

	}
}
