using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class BrandonController : MonoBehaviour {

    //the game controller
    public FallingFoodController controller;

    //counts of healthy and unhealthy food that Sam has collected
    private int healthyFood, unhealthyFood;
    private int score = 0;
    private static bool rand = false;
    private float max = 0.55f;
    private float min = -1.24f;
    private System.Random random = new System.Random();
    private static Int64 counter = 0; 
    
   


    // Brandons Position
    Vector3 brandonPosition;



    void Update()
    {
        
        Vector3 newPosition = this.transform.position;

        if (counter % 30 == 0)
        {
            float next = (float)random.NextDouble();
            float range = max - min;
            next = min + (next * range);
            float newPos = next;
            float change = brandonPosition.x - newPos;
            float ab = Math.Abs(change); 
            if ( ab > 0.8f)
            {
                newPos /= 4.0f; 
            }
            else if(ab > 0.2f && ab < 0.8f)
            {
                newPos /= 2.0f; 
            }

            newPosition.x = newPos;
            this.transform.position = newPosition;

            brandonPosition = newPosition;

            counter++;
        }
        else counter++; 
      
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
    /// Returns amount of healthy food Sam has grabbed
    /// </summary>
    /// <returns>The healthy food number.</returns>
    public int getHealthyFood()
    {

        return healthyFood;
    }

    /// <summary>
    /// Returns amount of unhealthy food Sam has grabbed
    /// </summary>
    /// <returns>The unhealthy food number.</returns>
    public int getUnhealthyFood()
    {

        return unhealthyFood;

    }


}
