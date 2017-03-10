using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TFButton : MonoBehaviour
{
	public bool setValue;
	public static bool isCorrect;

	//Score calculation


	public void checkValue() {
		if (setValue == Quiz.answer) {
			//Do score calculation
			isCorrect = true;
		} else {
			isCorrect = false;
		}
	}


}


