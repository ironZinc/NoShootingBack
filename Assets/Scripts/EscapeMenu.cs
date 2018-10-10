using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EscapeMenu : MonoBehaviour {


    public GameObject Escape;
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstperson;
    public bool escapeBool = false;
	// Use this for initialization
	void Start () {
        firstperson = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {

       // Debug.Log("testing");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escapeBool = !escapeBool;
        }

        if (escapeBool == true)
        {
            Time.timeScale = 0f;
            Escape.SetActive(true);
            firstperson.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (escapeBool == false)
        {
            Time.timeScale = 1f;
            Escape.SetActive(false);
            firstperson.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    public void ButtonContinue()
    {
        escapeBool = false;
    }
    public void ButtonBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
