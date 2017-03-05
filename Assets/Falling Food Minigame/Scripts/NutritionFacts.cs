using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class NutritionFacts : MonoBehaviour
{
    public static int quizChosen;
    public UnityEngine.UI.Text nutritionFact;


    void Start()
    {
        quizChosen = Random.Range(1, 15);


        string nutritionFactStr;


        if (quizChosen == 1)
        {
            nutritionFactStr = "fewfew";
        }
        else if (quizChosen == 2)
        {
            nutritionFactStr = "weewf";
        }
        else if (quizChosen == 3)
        {
            nutritionFactStr = "fefwefw";
        }
        else if (quizChosen == 4)
        {
            nutritionFactStr = "four";
        }
        else if (quizChosen == 5)
        {
            nutritionFactStr = "five";
        }
        else if (quizChosen == 6)
        {
            nutritionFactStr = "six";
        }
        else if (quizChosen == 7)
        {
            nutritionFactStr = "seven";
        }
        else if (quizChosen == 8)
        {
            nutritionFactStr = "eight";
        }
        else if (quizChosen == 9)
        {
            nutritionFactStr = "ffwfwe";
        }
        else if (quizChosen == 10)
        {
            nutritionFactStr = "nine";
        }
        else if (quizChosen == 11)
        {
            nutritionFactStr = "eleven";
        }
        else if (quizChosen == 12)
        {
            nutritionFactStr = "twelve";
        }
        else if (quizChosen == 13)
        {
            nutritionFactStr = "thirteen";
        }
        else if (quizChosen == 14)
        {
            nutritionFactStr = "fourteen";
        }
        else
        {
            nutritionFactStr = "fifteen";
        }


        nutritionFact.text = nutritionFactStr;
    }
}




