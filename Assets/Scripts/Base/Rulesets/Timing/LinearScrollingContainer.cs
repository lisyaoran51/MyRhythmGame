using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearScrollingContainer : ScrollingContainer {

    private float speed;
    private float screenHeight;
    // Update is called once per frame
    void Update () {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
	}

    private void construct(ControlPoint controlPoint) {
        ControlPoint = controlPoint;

    }

    private void load(ScreenConfig screenConfig) {
        screenHeight = screenConfig.ScreenHeight;

        speed = -screenHeight / VisibleTimeRange;
        transform.position = new Vector2(0f, speed * ControlPoint.StartTime + TargetLineHeight);
    }
}
