using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {
	public Text standingDisplay;

	private GameManager gameManager;
	private bool ballOutOfPlay = false;
	private int lastStandingCount = -1;
	private int lastSettledCount = 10; 
	private float lastChangeTime;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		//print (CountStanding());
		//print (ballOutOfPlay);
		standingDisplay.text = CountStanding().ToString();

		if(ballOutOfPlay){
			UpdateStandingCountAndSettle ();
			standingDisplay.color = Color.red; // RGBA(1,0,0,1) = RED
			//print (standingDisplay.color);
		}
	}

	public void Reset(){
		lastSettledCount = 10;
	}
		
	void OnTriggerExit (Collider collider) {
		if(collider.name == "Bowling Ball"){
			ballOutOfPlay = true;
		}
	}

	void UpdateStandingCountAndSettle(){
		// Update the lastStandingCount
		// Call PinsHaveSettled() when they have
		int currentStanding = CountStanding ();

		if (currentStanding != lastStandingCount){
			lastChangeTime = Time.time;
			lastStandingCount = currentStanding;
			return;
		}

		float settleTime = 3f; // How long ot wait ot consider pins settled
		//print(Time.time - lastChangeTime);
		if((Time.time - lastChangeTime) > settleTime){ // If last change > 3s ago
			// Call PinsHaveSettled
			PinsHaveSettled();
		}
	}

	void PinsHaveSettled(){
		print ("running PinsHaveSettled method");
		int standing = CountStanding ();
		int pinFall = lastSettledCount - standing;
		lastSettledCount = standing;

		gameManager.Bowl (pinFall);

		lastStandingCount = -1; // Indicates pins have settled, and ball not back in box
		ballOutOfPlay = false;
		standingDisplay.color = Color.green;
		//print (standingDisplay.color);	// RGBA(0,1,0,1) = GREEN
	}


	int CountStanding(){
		int standing = 0;

		foreach(Pin pin in GameObject.FindObjectsOfType<Pin>()){
			if(pin.IsStanding()){
				standing++;
			}
		}
		return standing;
	}
}
