﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour {

    public GameObject goal;
    public GameObject gameDirector;
    public GameObject questionManager;
    public Text text;

    public string answerTag;
    public bool isTouchable = true;
        
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void OnDrag() {

        //ドラッグされている間は拡大
        transform.localScale = new Vector3(10, 10, 2);

        //最前面に出す
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;

        //Touchableでないときはオブジェクトを移動させない
        if (!isTouchable) {
            return;
        }

        //動かしているオブジェクトとゴールの場所までの距離
        float distance = Vector3.Distance(transform.position, goal.transform.position);

        //正解オブジェクトのタグをQuestionManagerから取得
        answerTag = questionManager.GetComponent<QuestionManager>().answerTag.ToString();

        //正解のオブジェクトがゴールの十分近くに来たら、静止してCLEARに移る
        if (distance < 0.7f && gameObject.tag == answerTag) {
            transform.position = goal.transform.position;
            gameDirector.GetComponent<GameDirector>().GoNextState();
            
            gameDirector.GetComponent<GameDirector>().clearedSprite = this.gameObject;
            transform.localScale = new Vector3(8, 8, 1);

        }
        else {  //オブジェクトがタッチについてくる

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
        //オブジェクト移動中はSpringJointを無効にする    
        gameObject.GetComponent<SpringJoint2D>().enabled = false;
    }

    public void EndDrag() {

        //ドラッグが終わったら縮小
        transform.localScale = new Vector3(8, 8, 1);

        //元のOrder In Layerに戻す
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;

        //ドラッグが終わったらSpringJointを有効にする
        if (transform.position != goal.transform.position) {
            gameObject.GetComponent<SpringJoint2D>().enabled = true;
        }
    }

    public void PointerDown() {

        if(!isTouchable) {
            return;
        }

        //最前面に出す
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;

        //オブジェクトが触られている間は拡大
        transform.localScale = new Vector3(10, 10, 2);
    }

    public void PointerUp() {

        //元のOrder In Layerに戻す
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;

        //オブジェクトが離されたら縮小
        transform.localScale = new Vector3(8, 8, 1);
    }

}
