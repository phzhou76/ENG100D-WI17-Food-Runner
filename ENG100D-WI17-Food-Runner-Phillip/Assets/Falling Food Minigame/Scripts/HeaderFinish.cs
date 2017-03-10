using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeaderFinish : MonoBehaviour {

	private bool isDisplayed = false;
	private TextMesh textMesh;
	// Use this for initialization
	private bool continueFlow = false;
	void Start () {
		textMesh = GetComponent<TextMesh> ();
		textMesh.text = "";
	}

	// Update is called once per frame
	void Update () {
		if (MonsterController.samCaughtFlag && !isDisplayed) {
			textMesh.text = "Brandon Wins";
			isDisplayed = true;
			StartCoroutine (Process ());
		} else if (FallingFoodController.samFinish && !isDisplayed){
			textMesh.text = "Sam Wins!";
			isDisplayed = true;
			StartCoroutine (Process ());
		}			
	}
		
	IEnumerator Process()
	{

		//Wait 1 second
		yield return StartCoroutine(Wait(5.0f));
		continueFlow = true;
		while (!continueFlow) {
		}
		//Do process stuff
	}

	IEnumerator Wait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
	}

}