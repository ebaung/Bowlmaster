using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private List <int> rolls = new List<int>();

	private PinSetter pinSetter;
	private Ball ball;
	private ScoreDisplay scoreDisplay;

	// Use this for initialization
	void Start () {
		pinSetter = GameObject.FindObjectOfType<PinSetter> ();
		ball = GameObject.FindObjectOfType<Ball> ();
		scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay> ();
	}
	
	public void Bowl(int pinFall){
		try  {
			rolls.Add (pinFall);
			ball.Reset ();
		
			ActionMaster.Action nextAction = ActionMaster.NextAction (rolls);
			pinSetter.PerformAction (nextAction);
		} catch {
			Debug.LogWarning ("Something went awry in GameManager.Bowl()");
		}

		try {
			scoreDisplay.FillRolls (rolls);
			scoreDisplay.FillFrames (ScoreMaster.ScoreCumulative(rolls));
		} catch {
			Debug.LogWarning ("Something went awry in GameManger.Bowl() line 32 [scoreDisplay.FillRollCard (bowls);]");
		}
	}
}
