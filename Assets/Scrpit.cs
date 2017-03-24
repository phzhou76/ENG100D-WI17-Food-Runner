using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrpit : MonoBehaviour {

    public Animator samantha;
    public object playerTransform;

    // Use this for initialization
    void Start () {


        samantha = gameObject.GetComponent<Animator>();
        samantha.runtimeAnimatorController = Resources.Load("Assets/Falling Food Minigame/Animation/Samantha") as RuntimeAnimatorController;


    }

    // Update is called once per frame
    void Update () {
		
	}
}
