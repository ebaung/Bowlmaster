using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	public Vector3 launchVelocity;
	public bool inPlay = false;

	private Rigidbody rigidBody;
	private AudioSource ballSound;
	private AudioSource strike;
	private Vector3 ballStartPos;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		rigidBody.useGravity = false;
		ballStartPos = transform.position;
	}

	// Launch (launchVelocity);
	public void Launch (Vector3 velocity)
	{
		inPlay = true;

		rigidBody.useGravity = true;
		rigidBody.velocity = velocity;

		ballSound = GetComponent<AudioSource>();
		ballSound.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.z <= 1829f) {  //want to access CameraControl.stopPosition, but can't!
			strike = GetComponent<AudioSource> ();
			strike.Play ();
		} else {
			return;
		}
	}

	public void Reset(){
		Debug.Log ("Resetting ball");
		inPlay = false;
		transform.position = ballStartPos;
		transform.rotation = Quaternion.identity; // this is the same as Quaternion.Euler(0,0,0)
		rigidBody.velocity = Vector3.zero;
		rigidBody.angularVelocity = Vector3.zero;
		rigidBody.useGravity = false;
	}
}
