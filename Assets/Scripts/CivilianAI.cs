using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CivilianAI : MonoBehaviour {

	public GameObject player;

	private GameObject gunman;
	public int health = 3;

	public GameObject[] safePlaceArray;
	bool firstTimeScared = true;

	private Animator anim;
	private int state;
	private bool scared = false;
	private Component[] bones;
	private BoxCollider col;

	private float wanderRadius = 25f;
    private Transform target;
    private NavMeshAgent agent;

	public Material black;
	public Material blue;
	public Material jeans;
	public Material pink;
	public Material yellow;
	public Material darkYellow;
	public Material brown;
	public Material gray;
	public GameObject torso;
	public GameObject arm1_L;
	public GameObject arm2_L;
	public GameObject arm1_R;
	public GameObject arm2_R;
	public GameObject leg1_L;
	public GameObject leg2_L;
	public GameObject leg1_R;
	public GameObject leg2_R;
	private bool upperShort;
	private int upperColor;
	private int lowerColor;

	void Start () 
	{
		anim = GetComponent<Animator>();
		bones = gameObject.GetComponentsInChildren<Rigidbody> (); 
		col = gameObject.GetComponent<BoxCollider>();
		InvokeRepeating("myResetAnimationState", 0f, Random.Range(0f, 6f));
		agent = GetComponent<NavMeshAgent> ();
		upperShort = (Random.Range(0, 2) == 1);
		upperColor = Random.Range(0, 8);
		lowerColor = Random.Range(0, 8);
		gunman = GameObject.FindGameObjectWithTag("Gunman");
		Invoke("findSafePlaces", 1f);
		dress();

	}
	
	void Update ()
	{
		
		if(Mathf.Abs(gunman.transform.position.y - transform.position.y) < 0.5f || Vector3.Distance(gunman.transform.position, transform.position) < 32f)
		{
			if(firstTimeScared)
			{
				CancelInvoke("myResetAnimationState");
				scared = true;
				state = 0;
			}
		}
		
		anim.SetInteger("animState", state);
		anim.SetBool("animScared", scared);

		if(health <= 0)
		{
			goRagdoll();
		}

		if(!scared)
		{
			if(state == 0)
			{
				agent.SetDestination(agent.transform.position);
			}else if(state == 1){
				wander();
			}
		}else{
				if(firstTimeScared)
				{
					int temp = Random.Range(0, safePlaceArray.Length);
					agent.SetDestination(safePlaceArray[temp].transform.position);
					state = 1;
					agent.stoppingDistance = 4f;
					agent.speed = 10f;
					firstTimeScared = false;
				}

				escapeToSafePlace();
		}
	}

	void findSafePlaces()
	{
		 safePlaceArray = GameObject.FindGameObjectsWithTag("SafePlace");
	}

	void escapeToSafePlace()
	{
		RaycastHit hit;
		if(agent.remainingDistance <= 2f && Physics.Raycast(transform.position, gunman.transform.position - transform.position, out hit, 25f) && hit.transform.CompareTag("Gunman"))
		{
			int temp = Random.Range(0, safePlaceArray.Length);
			agent.SetDestination(safePlaceArray[temp].transform.position);
			state = 1;
		}else if(agent.remainingDistance <= 2f){
			state = 0;
			agent.SetDestination(transform.position);
		}
	}

	public void subtractHealth()
	{
		health -= 1;
	}

	void myResetAnimationState()
	{
		state = Random.Range(0, 2);
	}

	void goRagdoll () 
	{
		foreach (Rigidbody ragdoll in bones)
		{
			ragdoll.isKinematic = false;
		}
			
		anim.enabled = false;
		col.enabled = false;
		agent.enabled = false;
	}

	public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) 
	{
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }

	void wander()
	{
		if(agent.remainingDistance <= 1f)
		{
			Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
			agent.SetDestination(newPos);
		}
	}

		void dress()
	{
		if(upperShort)
		{
			if(upperColor == 0)
			{
				torso.GetComponent<Renderer>().material = black;
				arm1_L.GetComponent<Renderer>().material = black;
				arm1_R.GetComponent<Renderer>().material = black;
			}else if(upperColor == 1){
				torso.GetComponent<Renderer>().material = blue;
				arm1_L.GetComponent<Renderer>().material = blue;
				arm1_R.GetComponent<Renderer>().material = blue;
			}else if(upperColor == 2){
				torso.GetComponent<Renderer>().material = jeans;
				arm1_L.GetComponent<Renderer>().material = jeans;
				arm1_R.GetComponent<Renderer>().material = jeans;
			}else if(upperColor == 3){
				torso.GetComponent<Renderer>().material = pink;
				arm1_L.GetComponent<Renderer>().material = pink;
				arm1_R.GetComponent<Renderer>().material = pink;
			}else if(upperColor == 4){
				torso.GetComponent<Renderer>().material = yellow;
				arm1_L.GetComponent<Renderer>().material = yellow;
				arm1_R.GetComponent<Renderer>().material = yellow;
			}
			else if(upperColor == 5){
				torso.GetComponent<Renderer>().material = darkYellow;
				arm1_L.GetComponent<Renderer>().material = darkYellow;
				arm1_R.GetComponent<Renderer>().material = darkYellow;
			}else if(upperColor == 6){
				torso.GetComponent<Renderer>().material = brown;
				arm1_L.GetComponent<Renderer>().material = brown;
				arm1_R.GetComponent<Renderer>().material = brown;
			}else if(upperColor == 7){
				torso.GetComponent<Renderer>().material = gray;
				arm1_L.GetComponent<Renderer>().material = gray;
				arm1_R.GetComponent<Renderer>().material = gray;
			}
		}else{
			if(upperColor == 0)
			{
				torso.GetComponent<Renderer>().material = black;
				arm1_L.GetComponent<Renderer>().material = black;
				arm1_R.GetComponent<Renderer>().material = black;
				arm2_L.GetComponent<Renderer>().material = black;
				arm2_R.GetComponent<Renderer>().material = black;
			}else if(upperColor == 1){
				torso.GetComponent<Renderer>().material = blue;
				arm1_L.GetComponent<Renderer>().material = blue;
				arm1_R.GetComponent<Renderer>().material = blue;
				arm2_L.GetComponent<Renderer>().material = blue;
				arm2_R.GetComponent<Renderer>().material = blue;
			}else if(upperColor == 2){
				torso.GetComponent<Renderer>().material = jeans;
				arm1_L.GetComponent<Renderer>().material = jeans;
				arm1_R.GetComponent<Renderer>().material = jeans;
				arm2_L.GetComponent<Renderer>().material = jeans;
				arm2_R.GetComponent<Renderer>().material = jeans;
			}else if(upperColor == 3){
				torso.GetComponent<Renderer>().material = pink;
				arm1_L.GetComponent<Renderer>().material = pink;
				arm1_R.GetComponent<Renderer>().material = pink;
				arm2_L.GetComponent<Renderer>().material = pink;
				arm2_R.GetComponent<Renderer>().material = pink;
			}else if(upperColor == 4){
				torso.GetComponent<Renderer>().material = yellow;
				arm1_L.GetComponent<Renderer>().material = yellow;
				arm1_R.GetComponent<Renderer>().material = yellow;
				arm2_L.GetComponent<Renderer>().material = yellow;
				arm2_R.GetComponent<Renderer>().material = yellow;
			}else if(upperColor == 5){
				torso.GetComponent<Renderer>().material = darkYellow;
				arm1_L.GetComponent<Renderer>().material = darkYellow;
				arm1_R.GetComponent<Renderer>().material = darkYellow;
				arm2_L.GetComponent<Renderer>().material = darkYellow;
				arm2_R.GetComponent<Renderer>().material = darkYellow;
			}else if(upperColor == 6){
				torso.GetComponent<Renderer>().material = brown;
				arm1_L.GetComponent<Renderer>().material = brown;
				arm1_R.GetComponent<Renderer>().material = brown;
				arm2_L.GetComponent<Renderer>().material = brown;
				arm2_R.GetComponent<Renderer>().material = brown;
			}else if(upperColor == 7){
				torso.GetComponent<Renderer>().material = gray;
				arm1_L.GetComponent<Renderer>().material = gray;
				arm1_R.GetComponent<Renderer>().material = gray;
				arm2_L.GetComponent<Renderer>().material = gray;
				arm2_R.GetComponent<Renderer>().material = gray;
			}
		}

			if(lowerColor == 0)
			{
				leg1_L.GetComponent<Renderer>().material = black;
				leg1_R.GetComponent<Renderer>().material = black;
				leg2_L.GetComponent<Renderer>().material = black;
				leg2_R.GetComponent<Renderer>().material = black;
			}else if(lowerColor == 1){
				leg1_L.GetComponent<Renderer>().material = blue;
				leg1_R.GetComponent<Renderer>().material = blue;
				leg2_L.GetComponent<Renderer>().material = blue;
				leg2_R.GetComponent<Renderer>().material = blue;
			}else if(lowerColor == 2){
				leg1_L.GetComponent<Renderer>().material = jeans;
				leg1_R.GetComponent<Renderer>().material = jeans;
				leg2_L.GetComponent<Renderer>().material = jeans;
				leg2_R.GetComponent<Renderer>().material = jeans;
			}else if(lowerColor == 3){
				leg1_L.GetComponent<Renderer>().material = pink;
				leg1_R.GetComponent<Renderer>().material = pink;
				leg2_L.GetComponent<Renderer>().material = pink;
				leg2_R.GetComponent<Renderer>().material = pink;
			}else if(lowerColor == 4){
				leg1_L.GetComponent<Renderer>().material = yellow;
				leg1_R.GetComponent<Renderer>().material = yellow;
				leg2_L.GetComponent<Renderer>().material = yellow;
				leg2_R.GetComponent<Renderer>().material = yellow;
			}else if(lowerColor == 5){
				leg1_L.GetComponent<Renderer>().material = darkYellow;
				leg1_R.GetComponent<Renderer>().material = darkYellow;
				leg2_L.GetComponent<Renderer>().material = darkYellow;
				leg2_R.GetComponent<Renderer>().material = darkYellow;
			}else if(lowerColor == 6){
				leg1_L.GetComponent<Renderer>().material = brown;
				leg1_R.GetComponent<Renderer>().material = brown;
				leg2_L.GetComponent<Renderer>().material = brown;
				leg2_R.GetComponent<Renderer>().material = brown;
			}else if(lowerColor == 7){
				leg1_L.GetComponent<Renderer>().material = gray;
				leg1_R.GetComponent<Renderer>().material = gray;
				leg2_L.GetComponent<Renderer>().material = gray;
				leg2_R.GetComponent<Renderer>().material = gray;
			}
	}
}
