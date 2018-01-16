﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

	public GameObject pinSet;

	private Animator animator;
	private PinCounter pinCounter;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		pinCounter = GameObject.FindObjectOfType<PinCounter> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
		
	public void RaisePins(){
		foreach(Pin pin in GameObject.FindObjectsOfType<Pin>()){
			pin.RaiseIfStanding ();
		}
	}

	public void LowerPins(){
		// lower pins back down
		//Debug.Log("Lowering pins");

		foreach(Pin pin in GameObject.FindObjectsOfType<Pin>()){
			pin.Lower();
		}
	}

	public void RenewPins(){
		//Debug.Log ("Recharging pins");
		//Instantiate (pinSet,new Vector3(0,20,1829),Quaternion.identity);
		GameObject newPins = Instantiate(pinSet);
		newPins.transform.position += new Vector3 (0,20,0);
	}

	public void PerformAction(ActionMaster.Action action){
		if(action == ActionMaster.Action.Tidy){
			animator.SetTrigger ("tidyTrigger");
		} else if (action==ActionMaster.Action.EndTurn){
			animator.SetTrigger ("resetTrigger");
			pinCounter.Reset ();
		} else if (action==ActionMaster.Action.Reset){
			animator.SetTrigger ("resetTrigger");
			pinCounter.Reset ();
		} else if (action==ActionMaster.Action.EndTurn){
			throw new UnityException ("Don't know how to handle end game yet");
		}
	}
}