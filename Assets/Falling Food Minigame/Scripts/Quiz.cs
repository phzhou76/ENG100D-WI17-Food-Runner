using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;








public class Quiz : MonoBehaviour {




	public UnityEngine.UI.Text question;
	public static bool answer;




	void Start() {


		switch(QuizController.quizChosen) {


		case 1:
			question.text = "Carrots are good for your eyesight.";
			answer = true;
			break;




		case 2:
			question.text = "Carrots that are more orange have more vitamin A.";
			answer = true;
			break;




		case 3:
			question.text = "Carrots do not have much water content.";
			answer = false;
			break;




		case 4:
			question.text = "Carrots are good for hearing.";
			answer = false;
			break;




		case 5:
			question.text = "Bananas have high water content.";
			answer = true;
			break;




		case 6:
			question.text = "Bananas are a great source of potassium.";
			answer = true;
			break;




		case 7:
			question.text = "Unripe bananas are mostly starch.";
			answer = true;
			break;




		case 8:
			question.text = "Ripe bananas are mostly starch.";
			answer = false;
			break;




		case 9:
			question.text = "Broccoli has low water content.";
			answer = false;
			break;




		case 10:
			question.text = "Broccoli is not a good source of fiber.";
			answer = false;
			break;




		case 11:
			question.text = "Broccoli is good for you.";
			answer = true;
			break;




		case 12:
			question.text = "Fiber can increase risk of disease.";
			answer = false;
			break;




		case 13:
			question.text = "High amounts of cholesterol will lead to a healthy life.";
			answer = false;
			break;




		case 14:
			question.text = "Pizza is a good source of potassium.";
			answer = false;
			break;




		case 15:
			question.text = "Burgers are high in protein.";
			answer = true;
			break;




		default:
			question.text = "DEFAULT: " + NutritionFacts.quizChosen;
			break;
		}








	} 




}












