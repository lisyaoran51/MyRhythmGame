using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearScrollingContainer : ScrollingContainer {
    

    // Update is called once per frame
    void Update () {
		
	}

    internal ScrollingContainer construct(ControlPoint controlPoint) {
        ControlPoint = controlPoint;
        return this;
    }
}
