using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.transform.CompareTag("Player"))
		{
			SceneManager.LoadScene("Win");
			Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
		}
	}

}
