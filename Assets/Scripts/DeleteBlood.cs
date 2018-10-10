using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBlood : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("delete", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void delete()
	{
		Destroy(this.gameObject);
	}
}
