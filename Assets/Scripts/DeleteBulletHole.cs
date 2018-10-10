using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBulletHole : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("delete", 10f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void delete()
	{
		Destroy(this.gameObject);
	}
}
