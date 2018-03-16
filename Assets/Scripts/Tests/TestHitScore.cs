using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHitScore : MonoBehaviour {

    public Text Hit;

    public Text Score;
    public int score = -100;
	public Image scoreImage;

    public GameObject es;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(es);
		scoreImage.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Hit.text = "Good!";
            Hit.color = Color.green;
            score += 100;
            Score.text = score.ToString();
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            Hit.text = "Miss!";
            Hit.color = Color.red;
        }
		if (Input.GetKey(KeyCode.Alpha2)) {
			Score.text = "";
			Hit.text = "";
			scoreImage.gameObject.SetActive(true);
		}
        
    }
}
