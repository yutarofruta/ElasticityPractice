﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour {

    public GameObject goal;
    public Text text;

    private int qNum;

    private enum PlayerState{WAIT, PLAY, CLEAR};
    private PlayerState playerState;
    
	// Use this for initialization
	void Start () {
        qNum = 0;
        text.GetComponent<Text>().text = "2 - 1 = ?";
    }
	
	// Update is called once per frame
	void Update () {
        switch(playerState) {
            case PlayerState.WAIT:
                break;

            case PlayerState.PLAY:
                break;

            case PlayerState.CLEAR:
                break;
        }
    }

    public void OnDrag() {

        float distance = Vector3.Distance(transform.position, goal.transform.position);
        Debug.Log(distance);

        if (distance < 0.7f && gameObject.tag == "answer") {

            transform.position = goal.transform.position;
            text.GetComponent<Text>().text = "Correct!";
        
        } else {
            if (Input.touchCount > 0) {

                Touch touch = Input.GetTouch(0);
                Vector3 vec = touch.position;
                vec.z = 10f;
                vec = Camera.main.ScreenToWorldPoint(vec);
                transform.position = vec;

            }
            else if (Input.GetMouseButton(0)) {

                Vector3 vec = Input.mousePosition;
                vec.z = 10f;
                vec = Camera.main.ScreenToWorldPoint(vec);
                transform.position = vec;

            }
        }

        gameObject.GetComponent<SpringJoint2D>().enabled = false;

    }

    public void EndDrag() {
        if(transform.position != goal.transform.position) {
            gameObject.GetComponent<SpringJoint2D>().enabled = true;
        }
    }

   
}
