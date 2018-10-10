using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GunmanAI : MonoBehaviour {

	private Animator anim;
	private int state = 0;

	public List<GameObject> targets = new List<GameObject>();
	private GameObject[] civilians;
	public List<GameObject> aliveCivilians = new List<GameObject>();
	private NavMeshAgent agent;
	private GameObject player;
	
	public bool shouldShoot;
	public bool ready = true;
	public GameObject bulletEmitter;
	public GameObject bullet;
	public GameObject fire;
	private Vector3 dir;
	private GameObject target;
	private bool lookAtTarget;
	
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
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player");
		upperShort = (Random.Range(0, 2) == 1);
		upperColor = Random.Range(0, 8);
		lowerColor = Random.Range(0, 8);
		ready = true;
		dress();
		Invoke("findCivilians", 1f);
		InvokeRepeating("findTargets", 0f, 2f);
	}
	
	void Update () 
	{
		// Debug.DrawRay(transform.position, (GetClosestCivilian(aliveCivilians).transform.position + new Vector3(0f, 1f, 0f)) - transform.position, Color.red);
		// foreach(GameObject dude in targets)
		// {
		// 	Debug.DrawRay(transform.position, dude.transform.position - transform.position, Color.green);
		// }


		if(lookAtTarget)
		{
			agent.transform.LookAt(target.transform.position);
		}
		anim.SetInteger("gunmanState", state);

		StartCoroutine(shoot());
	}

	void findCivilians()
	{
		civilians = GameObject.FindGameObjectsWithTag("Civilian");
		aliveCivilians.AddRange(civilians);
		aliveCivilians.Add(player);
	}

	IEnumerator shoot()
	{
		if(shouldShoot && ready)
		{
			GameObject fireObject = Instantiate(fire, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;
			GameObject bulletObject = Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;
			bulletObject.transform.Rotate(Vector3.left * 90);
			Rigidbody bulletObjectRigidBody = bulletObject.GetComponent<Rigidbody>();
			bulletObjectRigidBody.velocity = transform.forward * 90;
			Destroy(bulletObject, 3f);
			ready = false;
			yield return new WaitForSeconds(0.74f);
			ready = true;
		}
	}

	void findTargets()
	{
		for(int i = 0; i < aliveCivilians.Count; i++)
		{
			if(aliveCivilians[i].CompareTag("Civilian"))
			{
				if(aliveCivilians[i].GetComponent<CivilianAI>().health <= 0)
				{
					aliveCivilians.Remove(aliveCivilians[i]);
				}
			}else if(aliveCivilians[i].CompareTag("Player")){
				if(aliveCivilians[i].GetComponent<PlayerControl>().health <= 0)
				{
					aliveCivilians.Remove(aliveCivilians[i]);
				}
			}
		}
		for(int i = 0; i < aliveCivilians.Count; i++)
		{
			RaycastHit hit;
			if(Physics.Raycast(transform.position, aliveCivilians[i].transform.position - transform.position, out hit, 20f) && (hit.transform.CompareTag("Civilian") || hit.transform.CompareTag("Player")) && !targets.Contains(hit.transform.gameObject))
			{
				targets.Add(hit.transform.gameObject);
			}
		}
		for(int i = 0; i < targets.Count; i++)
		{
			RaycastHit hit;
			if(targets[i].CompareTag("Civilian"))
			{
				if(targets[i].GetComponent<CivilianAI>().health <= 0 || !Physics.Raycast(transform.position, targets[i].transform.position - transform.position, out hit, 20f) || !hit.transform.CompareTag("Civilian"))
				{
					targets.Remove(targets[i]);
				}
			}else if(targets[i].CompareTag("Player")){
				if(targets[i].GetComponent<PlayerControl>().health <= 0 || !Physics.Raycast(transform.position, targets[i].transform.position - transform.position, out hit, 20f) || !hit.transform.CompareTag("Player"))
				{
					targets.Remove(targets[i]);
				}
			}
		}
		if(targets.Count == 0)
		{
			agent.SetDestination(GetClosestCivilian(aliveCivilians).transform.position);
			agent.stoppingDistance = 4f;
			lookAtTarget = false;
			shouldShoot = false;
			state = 2;
		}else{
			agent.SetDestination(agent.transform.position);
			state = 0;
			shouldShoot = true;
			int temp = Random.Range(0, targets.Count);
			target = targets[temp];
			float dist = Vector3.Distance(target.transform.position, transform.position);
			dir = target.transform.position - bulletEmitter.transform.position;
			lookAtTarget = true;
		}
	}

	GameObject GetClosestCivilian (List<GameObject> civiliansArr)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        for(int i = 0; i < civiliansArr.Count; i++)
        {
            Vector3 directionToTarget = civiliansArr[i].transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = civiliansArr[i];
            }
        }
     
        return bestTarget;
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
