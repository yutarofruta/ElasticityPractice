﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeController : MonoBehaviour {

    Animator anim;

    // Use this for initialization
    void Start () {
        this.anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void SetJumpTrigger() {
        anim.SetTrigger("JumpTrigger");
    }

}
