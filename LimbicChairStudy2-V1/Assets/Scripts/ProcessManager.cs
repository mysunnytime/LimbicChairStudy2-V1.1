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
        TASK_1, // take two pictures
        INTERSECTION_1,
        TASK_2, // find the treasure chest
        INTERSECTION_2,
    }

    public ProcessState state = ProcessState.PREPAREATION;
    float time = 0.0f;
    public string ParticipantID = "____";
    public ChestControl chest;
    public Flying flying;

    // Use this for initialization
    void Start()
    {
        WriteString("\nParticipant ID: " + ParticipantID + "\tDate: " + System.DateTime.Now);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("state: " + state);

        // reset to task one or task two
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            state = ProcessState.PREPAREATION;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            state = ProcessState.INTERSECTION_1;
        }

        // transition between states
        if (state == ProcessState.PREPAREATION) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                time = 0;
                chest.gameObject.SetActive(false);
                state = ProcessState.TASK_1;
            }
        }
        else if (state == ProcessState.TASK_1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                state = ProcessState.INTERSECTION_1;
            }
        }
        else if (state == ProcessState.INTERSECTION_1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                time = 0;
                chest.gameObject.SetActive(true);
                chest.Open();
                chest.transform.position = chest.position.position;
                chest.transform.rotation = chest.position.rotation;
                state = ProcessState.TASK_2;
            }
        }
        else if (state == ProcessState.TASK_2)
        {
            if (chest.foundTrigger)
            {
                WriteString("time to find the chest: \t" + (int)time);
                state = ProcessState.INTERSECTION_2;
                chest.foundTrigger = false;
            }
            else if(time > 180)
            {
                WriteString("did not find the chest in " + (int)time + " s.");
                chest.gameObject.SetActive(false);
                state = ProcessState.INTERSECTION_2;
            }
            time += Time.deltaTime;
        }
    }

    void OnApplicationQuit()
    {
        WriteString("\n\n");
    }

    void WriteString (string s)
	{
		string path = "Data/Data.txt";
		StreamWriter writer = new StreamWriter (path, true);
		writer.WriteLine (s);
		writer.Close ();
	}
}
