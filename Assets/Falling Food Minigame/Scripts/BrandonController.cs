﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BrandonController : MonoBehaviour
{

    //the game controller
    public FallingFoodController controller;

    /*counts of healthy and unhealthy food that Sam has collected
    private int score = 0;
    private static bool rand = false;
    private float max = 0.55f;
    private float min = -1.24f;
    private System.Random random = new System.Random();
    */

    // Brandons Position
    Vector3 brandonPosition;
    private int lane = 1;
    private static Int64 counter = 0;
    private int healthyFood, unhealthyFood;

    void Update()
    {

        Vector3 newPosition = this.transform.position;

        if (counter % 30 == 0)
        {

            int moveLane = UnityEngine.Random.Range(0, 3);

            switch (moveLane)
            {
                case 0:  //case to move left
                    if (lane == 0) break;
                    newPosition.x += -0.5f;
                    lane--;
                    break;
                case 1:
                    if (lane == 2) break;
                    newPosition.x += 0.5f;
                    lane++;
                    break;
                case 2:
                default:
                    break;
            }


            this.transform.position = newPosition;
            counter++;
        }
        else counter++;
        Debug.Log("Brandon's position: " + this.transform.position);
    }


    public Vector3 getBrandonPosition()
    {
        return this.brandonPosition;
    }

    /// <summary>
    /// Checks if Sam has entered a food trigger
    /// </summary>
    /// <param name="other">Food that Sam collided with.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        //if the collision was indeed with a piece of food
        if (other.GetComponent<Food>())
        {

            //grab the food
            grabFood(other.GetComponent<Food>());

        }
    }

    private void grabFood(Food food)
    {
        /*
        Vector3 newPosition = this.transform.position;
        if (food.isHealthy())
        {
            newPosition.y += 0.1f; 
            this.transform.position = newPosition;
            brandonPosition = newPosition; 
        }

        //checks if food is unhealthy, and if so increments counter and decreases speed
        else
        {

            newPosition.y -= 0.1f;
            this.transform.position = newPosition;
            brandonPosition = newPosition;

        }
        //removes the food from existence
        */
        food.remove();

    }



    /// <summary>
    /// Returns amount of healthy food Brandon has grabbed
    /// </summary>
    /// <returns>The healthy food number.</returns>
    public int getHealthyFood()
    {

        return healthyFood;
    }

    /// <summary>
    /// Returns amount of unhealthy food Brandon has grabbed
    /// </summary>
    /// <returns>The unhealthy food number.</returns>
    public int getUnhealthyFood()
    {

        return unhealthyFood;

    }


}
