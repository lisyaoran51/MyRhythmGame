using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class TestLEDEvent : MonoBehaviour {
    SerialPort sp;
    int[] note = new int[] { 55, 57, 60, 64, 67,69, 71, 72, 74, 76, 77, 80, 83 };
    int i = 0;
    // Use this for initialization
    void Start () {
        sp = new SerialPort("COM7", 9600);
        sp.Open();

        sp.WriteLine("Event Note 60 5");
        sp.BaseStream.Flush();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("down") || Input.GetKey(KeyCode.S)) {
            sp.WriteLine("Event Note "+ note[i] + " 5");
            //sp.WriteLine("Event Note 77 5");
            sp.BaseStream.Flush();
            i++;
        }
        if (Input.GetKeyDown("up")) i--;
    }
}
