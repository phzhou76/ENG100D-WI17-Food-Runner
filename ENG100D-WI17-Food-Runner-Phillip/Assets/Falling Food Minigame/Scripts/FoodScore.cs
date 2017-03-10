using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FoodScore : MonoBehaviour
{

    public Text HealthyFoodCount;
    public Text HealthyFoodScore;
    public Text UnhealthyFoodCount;
    public Text UnhealthyFoodScore;
    public Text QuizScore;
    public Text TotalScore;

    private int healthyF;
    private int unhealthyF;
    private int totalScore;
    private bool answered = false;
    private Timer localGameTimer;

    public void Initialize(Timer gameTimer, int healthyFood, int unhealthyFood)
    {
        healthyF = healthyFood;
        unhealthyF = unhealthyFood;
        localGameTimer = gameTimer;
        HealthyFoodCount.text = "x" + healthyFood.ToString();
        UnhealthyFoodCount.text ="x" + unhealthyFood.ToString();
        HealthyFoodScore.text = healthyFood.ToString();
        UnhealthyFoodScore.text = "-" + unhealthyFood.ToString();





        GlobalScore.addScore(healthyFood - unhealthyFood);

    }

    // Use this for initialization
    void Start()
    {
        totalScore = healthyF - unhealthyF;
    }

    // Update is called once per frame
    void Update()
    {

        if (TFButton.isCorrect && !answered)
        {
            totalScore += 5;
            answered = true;
            int score = 5;
            QuizScore.text = score.ToString();
            GlobalScore.addScore(5);
            TotalScore.text = totalScore.ToString();
        }

        TotalScore.text = totalScore.ToString();
    }

    void returnToCity()
    {

        SceneManager.LoadScene(0);

    }
}
