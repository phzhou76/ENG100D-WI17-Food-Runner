using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class NutritionFact : MonoBehaviour
{
	public int quizChosen;
	public UnityEngine.UI.Text nutritionFact;




	void Start()
	{
		// quizChosen = Random.Range(1, 15);
		quizChosen = QuizController.quizChosen;




		string nutritionFactStr;




		if (quizChosen == 1)
		{
			nutritionFactStr = "Carrots are good for your eyesight.";
		}
		else if (quizChosen == 2)
		{
			nutritionFactStr = "The deeper orange the carrot is, the more vitamin A it contains!";
		}
		else if (quizChosen == 3)
		{
			nutritionFactStr = "Carrots are 88% water";
		}
		else if (quizChosen == 4)
		{
			nutritionFactStr = "Carrots are good for your eye’s health";
		}
		else if (quizChosen == 5)
		{
			nutritionFactStr = "Bananas are 75% water";
		}
		else if (quizChosen == 6)
		{
			nutritionFactStr = "Bananas are a great source of potassium";
		}
		else if (quizChosen == 7)
		{
			nutritionFactStr = "Unripe bananas are mostly starch ";
		}
		else if (quizChosen == 8)
		{
			nutritionFactStr = "Ripe bananas are mostly contain sugars";
		}
		else if (quizChosen == 9)
		{
			nutritionFactStr = "Broccoli is 89% water";
		}
		else if (quizChosen == 10)
		{
			nutritionFactStr = "Broccoli is a good source of fiber";
		}
		else if (quizChosen == 11)
		{
			nutritionFactStr = "Broccoli is also rich in vitamin C, vitamin K, iron and potassium";
		}
		else if (quizChosen == 12)
		{
			nutritionFactStr = "Fiber is good for you.";
		}
		else if (quizChosen == 13)
		{
			nutritionFactStr = "High amounts of cholesterol can lead to heart disease.";
		}
		else if (quizChosen == 14)
		{
			nutritionFactStr = "A typical pepperoni pizza slice has high amounts of cholesterol.";
		}
		else
		{
			nutritionFactStr = "Burgers are usually high in protein.";
		}




		nutritionFact.text = nutritionFactStr;
	}
}










