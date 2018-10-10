using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisions : MonoBehaviour {

	public GameObject bulletHolePrefab;
	public GameObject bloodPrefab;

	private GameObject blood1;
	private GameObject blood2;
	private GameObject blood3;
	private GameObject blood4;
	private GameObject blood5;
	private GameObject blood6;
	private GameObject blood7;
	private GameObject blood8;

	private Vector3 hitPoint;

	void Start () 
	{
		
	}

	void Update () 
	{
		
	}

	void OnCollisionEnter(Collision col)
	{
		Debug.Log("hit something");
		foreach (ContactPoint contact in col.contacts)
        {
            hitPoint = contact.point;
        }

		if(col.transform.gameObject.CompareTag("Civilian"))
		{
			//Debug.Log("hit civilian");
			col.gameObject.GetComponent<CivilianAI>().subtractHealth();
			blood1 = Instantiate(bloodPrefab, hitPoint, transform.rotation);
			blood2 = Instantiate(bloodPrefab, hitPoint, transform.rotation);
			blood3 = Instantiate(bloodPrefab, hitPoint, transform.rotation);
			blood4 = Instantiate(bloodPrefab, hitPoint, transform.rotation);
			blood5 = Instantiate(bloodPrefab, hitPoint, transform.rotation);
			blood6 = Instantiate(bloodPrefab, hitPoint, transform.rotation);
			blood7 = Instantiate(bloodPrefab, hitPoint, transform.rotation);
			blood8 = Instantiate(bloodPrefab, hitPoint, transform.rotation);
			if(col.gameObject.GetComponent<CivilianAI>().health <= 0)
			{
				col.rigidbody.AddForce((col.transform.position - transform.position) * 2f);
			}
			Destroy(this.gameObject);
		}else if(col.transform.gameObject.CompareTag("Player")){
			col.gameObject.GetComponent<PlayerControl>().subtractHealth();
			blood1 = Instantiate(bloodPrefab, hitPoint, transform.rotation);
			blood2 = Instantiate(bloodPrefab, hitPoint, transform.rotation);
			blood3 = Instantiate(bloodPrefab, hitPoint, transform.rotation);
			blood4 = Instantiate(bloodPrefab, hitPoint, transform.rotation);
			blood5 = Instantiate(bloodPrefab, hitPoint, transform.rotation);
			blood6 = Instantiate(bloodPrefab, hitPoint, transform.rotation);
			blood7 = Instantiate(bloodPrefab, hitPoint, transform.rotation);
			blood8 = Instantiate(bloodPrefab, hitPoint, transform.rotation);
			if(col.gameObject.GetComponent<PlayerControl>().health <= 0)
			{
				col.gameObject.GetComponent<PlayerControl>().die();
				col.rigidbody.AddForce((col.transform.position - transform.position) * 2f);
			}
			Destroy(this.gameObject);
		}else if(col.transform.gameObject.CompareTag("Level")){
			Instantiate(bulletHolePrefab, hitPoint, transform.rotation);
			Destroy(this.gameObject);
		}
	}

}
