using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBuilding : MonoBehaviour {

	public List<GameObject> rooms;
	public List<GameObject> room11Props;
	public List<GameObject> room22Props;
	public List<GameObject> room32Props;
	public GameObject floor;
	public GameObject lastFloor;
	public GameObject[] pointsSimple;
	public GameObject[,] points = new GameObject[5, 5];
	private int floorNum = 5;//4 to start with for now
	public GameObject civilian;

	void Start () 
	{
		// generateRooms();
		generateFloors();	
	}

	public void generateFloors()
	{
		for(int i = 1; i < floorNum - 1; i++)
		{
			Instantiate(floor, new Vector3(0, 7 * i, 0), transform.rotation);
			generateRooms();
		}
		Instantiate(lastFloor, new Vector3(0, 7 * (floorNum - 1), 0), transform.rotation);
		generateRooms();
	}

	public void generateRooms()
	{
		pointsSimple = GameObject.FindGameObjectsWithTag("Point");
		int x = 0;
		for(int i = 0; i < points.GetLength(0); i++)
		{
			for(int j = 0; j < points.GetLength(1); j++)
			{
				points[i, j] = pointsSimple[x];
				x++;
			}
		}

		int hallway = Random.Range(0, 3);
		Debug.Log(hallway);
		if(hallway == 0)
		{
			points[4, 0].SetActive(false);
			points[4, 0] = null;
			points[4, 1].SetActive(false);
			points[4, 1] = null;
			points[4, 2].SetActive(false);
			points[4, 2] = null;
			points[4, 3].SetActive(false);
			points[4, 3] = null;
			points[4, 4].SetActive(false);
			points[4, 4] = null;
			points[0, 0].SetActive(false);
			points[0, 0] = null;
			points[0, 1].SetActive(false);
			points[0, 1] = null;
			points[0, 2].SetActive(false);
			points[0, 2] = null;
			points[0, 3].SetActive(false);
			points[0, 3] = null;
			points[0, 4].SetActive(false);
			points[0, 4] = null;

		}else{
			points[0, 2].SetActive(false);
			points[0, 2] = null;
			points[1, 2].SetActive(false);
			points[1, 2] = null;
			points[2, 2].SetActive(false);
			points[2, 2] = null;
			points[3, 2].SetActive(false);
			points[3, 2] = null;
			points[4, 2].SetActive(false);
			points[4, 2] = null;
		}

		for(int i = 0; i < points.GetLength(0); i++)
		{
			for(int j = 0; j < points.GetLength(1); j++)
			{
				if(points[i, j] != null)
				{
					int temp = Random.Range(0, 5);
					if(temp >= 1 && temp <= 2 && points.GetLength(0) - i >= 2 && points.GetLength(1) - j >= 2){
						if(points[i + 1, j] != null && points[i, j + 1] != null && points[i + 1, j + 1] != null)
						{
							GameObject room22 = Instantiate(rooms[1], new Vector3((points[i, j].transform.position.x + points[i + 1, j + 1].transform.position.x) / 2, (points[i, j].transform.position.y + points[i + 1, j + 1].transform.position.y) / 2, (points[i, j].transform.position.z + points[i + 1, j + 1].transform.position.z) / 2), rooms[1].transform.rotation);
							room22.tag = "SafePlace";
							points[i, j].SetActive(false);
							points[i, j] = null;
							points[i + 1, j + 1].SetActive(false);
							points[i + 1, j + 1] = null;
							points[i + 1, j].SetActive(false);
							points[i + 1, j] = null;
							points[i, j + 1].SetActive(false);
							points[i, j + 1] = null;
							int prop = Random.Range(0, room22Props.Count);
							Instantiate(room22Props[prop], room22.transform.position, room22Props[prop].transform.rotation);
							if(Random.Range(0, 2) == 0)
							{
								Instantiate(civilian, room22.transform.position, room22.transform.rotation);
							}
						}else{
							GameObject room11 = Instantiate(rooms[0], points[i, j].transform.position, rooms[0].transform.rotation);
							room11.tag = "SafePlace";
							points[i, j].SetActive(false);
							points[i, j] = null;
							int prop = Random.Range(0, room11Props.Count);
							Instantiate(room11Props[prop], room11.transform.position, room11Props[prop].transform.rotation);
							if(Random.Range(0, 2) == 0)
							{
								Instantiate(civilian, room11.transform.position, room11.transform.rotation);
							}
						}
					}else if(temp >= 3 && temp <= 4 && points.GetLength(0) - i >= 3 && points.GetLength(1) - j >= 2){
						if(points[i + 1, j] != null && points[i, j + 1] != null && points[i + 1, j + 1] != null && points[i + 2, j] != null && points[i + 2, j + 1] != null)
						{
							GameObject room32 = Instantiate(rooms[2], new Vector3((points[i, j].transform.position.x + points[i + 2, j + 1].transform.position.x) / 2, (points[i, j].transform.position.y + points[i + 2, j + 1].transform.position.y) / 2, (points[i, j].transform.position.z + points[i + 2, j + 1].transform.position.z) / 2), rooms[2].transform.rotation);
							room32.tag = "SafePlace";
							points[i, j].SetActive(false);
							points[i, j] = null;
							points[i + 1, j + 1].SetActive(false);
							points[i + 1, j + 1] = null;
							points[i + 1, j].SetActive(false);
							points[i + 1, j] = null;
							points[i, j + 1].SetActive(false);
							points[i, j + 1] = null;
							points[i + 2, j + 1].SetActive(false);
							points[i + 2, j + 1] = null;
							points[i + 2, j].SetActive(false);
							points[i + 2, j] = null;
							int prop = Random.Range(0, room32Props.Count);
							Instantiate(room32Props[prop], room32.transform.position, room32Props[prop].transform.rotation);
							if(Random.Range(0, 2) == 0)
							{
								Instantiate(civilian, room32.transform.position, room32.transform.rotation);
							}
						}else if(points[i + 1, j] != null && points[i, j + 1] != null && points[i + 1, j + 1] != null){
							GameObject room22 = Instantiate(rooms[1], new Vector3((points[i, j].transform.position.x + points[i + 1, j + 1].transform.position.x) / 2, (points[i, j].transform.position.y + points[i + 1, j + 1].transform.position.y) / 2, (points[i, j].transform.position.z + points[i + 1, j + 1].transform.position.z) / 2), rooms[1].transform.rotation);
							room22.tag = "SafePlace";
							points[i, j].SetActive(false);
							points[i, j] = null;
							points[i + 1, j + 1].SetActive(false);
							points[i + 1, j + 1] = null;
							points[i + 1, j].SetActive(false);
							points[i + 1, j] = null;
							points[i, j + 1].SetActive(false);
							points[i, j + 1] = null;
							int prop = Random.Range(0, room22Props.Count);
							Instantiate(room22Props[prop], room22.transform.position, room22Props[prop].transform.rotation);
							if(Random.Range(0, 2) == 0)
							{
								Instantiate(civilian, room22.transform.position, room22.transform.rotation);
							}
						}else{
							GameObject room11 = Instantiate(rooms[0], points[i, j].transform.position, rooms[0].transform.rotation);
							room11.tag = "SafePlace";
							points[i, j].SetActive(false);
							points[i, j] = null;
							int prop = Random.Range(0, room11Props.Count);
							Instantiate(room11Props[prop], room11.transform.position, room11Props[prop].transform.rotation);
							if(Random.Range(0, 2) == 0)
							{
								Instantiate(civilian, room11.transform.position, room11.transform.rotation);
							}
						}
					}else{
						GameObject room11 = Instantiate(rooms[0], points[i, j].transform.position, rooms[0].transform.rotation);
						room11.tag = "SafePlace";
						points[i, j].SetActive(false);
						points[i, j] = null;
						int prop = Random.Range(0, room11Props.Count);
						Instantiate(room11Props[prop], room11.transform.position, room11Props[prop].transform.rotation);
						if(Random.Range(0, 2) == 0)
						{
							Instantiate(civilian, room11.transform.position, room11.transform.rotation);
						}
					}
				}
			}
		}
	}

}
