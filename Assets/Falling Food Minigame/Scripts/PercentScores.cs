﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PercentScores : MonoBehaviour, SpeedListener
{
    public Text currentSpeedText;

    public RectTransform raceCompletedBar;

    float minSpeed, maxSpeed;

    string raceCompletedString = "Race Completed: ";
    string currentSpeedString = "Current Speed: ";
    string percentString = "%";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Initializes the percent scores on the screen
    /// </summary>
    /// <param name="minSpeed">Minimum speed.</param>
    /// <param name="maxSpeed">Max speed.</param>
    /// <param name="startSpeed">Start speed.</param>
    public void Initialize(float minSpeed, float maxSpeed, float startSpeed)
    {

        //sets min and max speeds for calculating speed percent
        this.minSpeed = minSpeed;
        this.maxSpeed = maxSpeed;

        //displays the speed percent
        displaySpeedPercent(calculatePercent(minSpeed, maxSpeed, startSpeed));

    }

    /// <summary>
    /// Updates the scroll speed.
    /// </summary>
    /// <param name="speed">Speed.</param>
    public void updateScrollSpeed(float speed)
    {

        //displays new speed percent when speed is updated
        displaySpeedPercent(calculatePercent(minSpeed, maxSpeed, speed));

    }

    /// <summary>
    /// Displays the race completion.
    /// </summary>
    /// <param name="percent">Percent.</param>
    public void displayRaceCompletion(float percent)
    {
        if(percent < 100.0f)
        {
            raceCompletedBar.localScale = new Vector3(percent * 0.01f, raceCompletedBar.localScale.y, raceCompletedBar.localScale.z);
        }
        else
        {
            raceCompletedBar.localScale = new Vector3(1.0f, raceCompletedBar.localScale.y, raceCompletedBar.localScale.z);
        }
    }

    /// <summary>
    /// Displays the speed percent.
    /// </summary>
    /// <param name="percent">Percent.</param>
    public void displaySpeedPercent(float percent)
    {
        currentSpeedText.text = currentSpeedString + (int)percent + percentString;
    }

    /// <summary>
    /// Calculates the percent.
    /// </summary>
    /// <returns>The percent.</returns>
    /// <param name="minSpeed">Minimum speed.</param>
    /// <param name="maxSpeed">Max speed.</param>
    /// <param name="currentSpeed">Current speed.</param>
    private float calculatePercent(float minSpeed, float maxSpeed, float currentSpeed)
    {
        return (((currentSpeed - minSpeed) / (maxSpeed - minSpeed)) * 100);

    }

    public void displayScore(int score)
    {
    }
}
