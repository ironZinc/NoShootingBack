using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteFire : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Invoke("delete", 0.06f);
	}

	void delete()
	{
		Destroy(this.gameObject);
	}

}
