using Base.Threading;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class TestScheduler : MonoBehaviour {

    private Scheduler s = new Scheduler();

    public GameObject sprite;
	// Use this for initialization
	void Start () {
        
        SerialPort sp = new SerialPort("COM7", 9600);
        sp.Open();
        sp.WriteLine("Event Note 60 5");
        sp.BaseStream.Flush();
        
        s.AddDelayed(() => {
            sprite.SetActive(false);
            sp.WriteLine("Event Note 60 5");
            sp.BaseStream.Flush();
        },
        6f);
	}
	
	// Update is called once per frame
	void Update () {
        s.Update();
	}
}
