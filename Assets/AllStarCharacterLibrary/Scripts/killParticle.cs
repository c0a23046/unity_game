using UnityEngine;
using System.Collections;
#pragma warning disable

public class killParticle : MonoBehaviour 
{
	ParticleSystem ps;
	public float lifespan;
	float startTime;

	// Use this for initialization
	void Start () 
	{
		ps = GetComponent<ParticleSystem>();
		startTime = Time.fixedTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if((Time.fixedTime-startTime)> lifespan)
		{
			Destroy(gameObject);
		}
	}
}
