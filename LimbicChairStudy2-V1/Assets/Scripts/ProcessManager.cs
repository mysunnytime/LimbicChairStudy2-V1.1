using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEditor;
using System.IO;
using UnityEngine.UI;

public class ProcessManager : MonoBehaviour
{
	public enum ProcessState
	{
		PREPAREATION,
		SECTION_1,
		INTERSECTION_1,
		SECTION_2,
		INTERSECTION_2,
		SECTION_3,
		INTERSECTION_3
	}

	public ProcessState state = ProcessState.PREPAREATION;
	float time = 0.0f;
	public string ParticipantID = "____";
	public ChestControl chest;
	public Flying flying;

	// Use this for initialization
	void Start ()
	{
		WriteString ("\nParticipant ID: " + ParticipantID + "\tDate: " + System.DateTime.Now);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log ("state: " + state);
		if (state == ProcessState.SECTION_1) {
			if (chest.foundTrigger) {
				WriteString ("time to find the Chest at position 1: " + time + "s.");
				state = ProcessState.INTERSECTION_1;
				chest.foundTrigger = false;
			}
		} else if (state == ProcessState.SECTION_2) {
			if (chest.foundTrigger) {
				WriteString ("time to find the Chest at position 2: " + time + "s.");
				state = ProcessState.INTERSECTION_2;
				chest.foundTrigger = false;
			}
		} else if (state == ProcessState.SECTION_3) {
			if (chest.foundTrigger) {
				WriteString ("time to find the Chest at position 3: " + time + "s.");
				state = ProcessState.INTERSECTION_3;
				time = 0;
				chest.foundTrigger = false;
			}
		}

		time += Time.deltaTime;

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			time = 0;
			chest.ToggleOpeness ();
			chest.transform.position = chest.position1.position;
			chest.transform.rotation = chest.position1.rotation;
			state = ProcessState.SECTION_1;
		}

		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			time = 0;
			chest.ToggleOpeness ();
			chest.transform.position = chest.position2.position;
			chest.transform.rotation = chest.position2.rotation;
			state = ProcessState.SECTION_2;
		}

		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			time = 0;
			chest.ToggleOpeness ();
			chest.transform.position = chest.position3.position;
			chest.transform.rotation = chest.position3.rotation;
			state = ProcessState.SECTION_3;
		}
	}

	void OnApplicationQuit ()
	{		
		if (!chest.found)
			WriteString ("Chest not found. Time of the session: " + time + " s.");
		WriteString ("\n");
	}

	void WriteString (string s)
	{
		string path = "Assets/Data.txt";

		StreamWriter writer = new StreamWriter (path, true);
		writer.WriteLine (s);
		writer.Close ();
	}

}
