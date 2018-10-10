using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerControl : MonoBehaviour {

	public int health = 3;
	CharacterController cont;
	FirstPersonController first;
	Rigidbody rig;

	public RawImage blood;
	public RawImage blood2;
	public Image death;

	public GameObject thing;
	private GameObject gunman;
	public List<GameObject> exits;
     
    private Transform tr;
    private float dist;

	void Start () 
	{
        tr = transform;
        CharacterController ch = GetComponent<CharacterController>();
        dist = ch.height/2;
		cont = gameObject.GetComponent<CharacterController>();
		first = gameObject.GetComponent<FirstPersonController>();
		rig = gameObject.GetComponent<Rigidbody>();
		gunman = GameObject.FindGameObjectWithTag("Gunman");
		Invoke("findExits", 1f);
	}
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.H))
		{
			thing.SetActive(false);
			gunman.transform.position = new Vector3(0f, -0.19f, 0f);
			for(int i = 0; i < exits.Count; i++)
			{
				exits[i].SetActive(false);
			}
			int temp = Random.Range(0, exits.Count);
			exits[temp].SetActive(true);
			BoxCollider exitCollider = exits[temp].GetComponent<BoxCollider>();
			exitCollider.enabled = true;
		}
		
		if(health == 1)
		{
			first.m_WalkSpeed = 2f;
			first.m_RunSpeed = 2.2f;
			blood2.enabled = true;
		}else if(health == 2){
			first.m_WalkSpeed = 2.5f;
			first.m_RunSpeed = 3f;
			blood.enabled = true;
		}else if(health == 3){
			first.m_WalkSpeed = 5f;
			first.m_RunSpeed = 10f;
		}

		if(health <= 0)
		{
			SceneManager.LoadScene("GameOver");
			Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
		}
	}

	void findExits()
	{
		exits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Exit"));
	}

	void FixedUpdate ()
     {
         float vScale = 1.0f;

         if (Input.GetKey(KeyCode.LeftControl))
         {
             vScale = 0.5f;
			first.m_WalkSpeed = 1.8f;
			first.m_RunSpeed = 1.8f;
         }else{
			if(health == 1)
			{
				first.m_WalkSpeed = 2f;
				first.m_RunSpeed = 2.2f;
			}else if(health == 2){
				first.m_WalkSpeed = 2.5f;
				first.m_RunSpeed = 3f;
			}else if(health == 3){
				first.m_WalkSpeed = 5f;
				first.m_RunSpeed = 10f;
			}
		 }
         
         float ultScale = tr.localScale.y;
         
         Vector3 tmpScale = tr.localScale;
         Vector3 tmpPosition = tr.position;
         
         tmpScale.y = Mathf.Lerp(tr.localScale.y, vScale, 5 * Time.deltaTime);
         tr.localScale = tmpScale;
         
         tmpPosition.y += dist * (tr.localScale.y - ultScale);     
         tr.position = tmpPosition;
     }

	public void die()
	{
		cont.enabled = false;
		first.enabled = false;
		death.enabled = true;
	}

	public void subtractHealth()
	{
		health -= 1;
	}

}
