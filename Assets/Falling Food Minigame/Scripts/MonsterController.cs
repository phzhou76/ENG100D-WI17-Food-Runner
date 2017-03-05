using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour {

	// Use this for initialization
	private float tempSamSpeed;
	public SamController samController;
	private bool monsterFlag = false;
	public static bool samCaughtFlag = false;
	Vector3 newPosition;

	// Brandon calculations
	private float brandonSpeed;
	private int speedStage = 0;


	void Start () {
		//MeshRenderer m = this.GetComponent<MeshRenderer>();
		//m.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		//CatchSam ();

		if (!monsterFlag && samController.getSpeed() >= 25) {
			brandonSpeed = 1;            
			monsterFlag = true;
			SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
			sr.enabled = true;
		}
		if (monsterFlag && !samCaughtFlag && samController.getSpeed() <= brandonSpeed/*samController.getSpeed () <= 30*/) {
			CatchSam ();
			samCaughtFlag = true;
		} 
		if (samCaughtFlag)
			CatchSam ();
		if ((samController.getSpeed() % 5 <= 1) && samController.getSpeed() > tempSamSpeed) {
			tempSamSpeed = samController.getSpeed ();
			brandonSpeed += 1;
		}

		/*
		if (samController.getSpeed () >= 50 && speedStage == 0) {
			brandonSpeed = 46;
			speedStage++;
		} else if (samController.getSpeed () >= 60 && speedStage == 1) {
			brandonSpeed = 56;
			speedStage++;
		} else if (samController.getSpeed () >= 70 && speedStage == 2) {
			brandonSpeed = 66;
			speedStage++;
		} else if (samController.getSpeed () >= 80 && speedStage == 3) {
			brandonSpeed = 76;
			speedStage++;
		} else if (samController.getSpeed () >= 90 && speedStage == 4) {
			brandonSpeed = 86;
			speedStage++;
		}*/

		/*if (monsterFlag) {
			newPosition.x = this.transform.position.x + Random.Range (-.2f,.2f) * Time.deltaTime;
			this.transform.position = newPosition;
		}*/
	
	}

	private void moveBrandonSideways() {
		if (newPosition.x < -.7f) {
			newPosition.x = this.transform.position.x + Random.Range (.2f, .2f) * Time.deltaTime;
		} else if (newPosition.x >= .5f) {
			newPosition.x = this.transform.position.x +  Random.Range (-.2f, -.2f) * Time.deltaTime;
		} else {
			newPosition.x = this.transform.position.x + Random.Range (-.2f,.2f) * Time.deltaTime;
		}
		this.transform.position = newPosition;
	}
		

	void CatchSam() {
		newPosition = this.transform.position;
		newPosition.y = this.transform.position.y + .5f * Time.deltaTime;
		this.transform.position = newPosition;
	}

	public bool isSamCaught () {
		return samCaughtFlag;
	}
		

}
