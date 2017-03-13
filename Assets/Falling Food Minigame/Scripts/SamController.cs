using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Sam (Player) controller
/// </summary>
public class SamController : MonoBehaviour
{

    //the game controller
    public FallingFoodController controller;

    //counts of healthy and unhealthy food that Sam has collected
    private int healthyFood, unhealthyFood;
    private int score = 0;

    // The current lane that Sam is on. Default is middle lane.
    private int lane = 1;

    // Update is called once per frame
    void Update()
    {
        float vertSpeed = Input.GetAxisRaw("Vertical") * Time.deltaTime;

        Vector3 position = this.transform.position;

        if (vertSpeed != 0.0f)
        {
            position.y += vertSpeed;
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxisRaw("Horizontal") < 0 && lane > 0)
            {
                --lane;
                position.x -= 0.5f;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0 && lane < 2)
            {
                ++lane;
                position.x += 0.5f;
            }
        }

        this.transform.position = position;

        // Time slow effect.
        if(Input.GetButtonDown("Time Stop"))
        {
            Time.timeScale = 0.3f;
        }
        
        if(Input.GetButtonUp("Time Stop"))
        {
            Time.timeScale = 1.0f;
        }

#if FALSE
        //checks if mouse is being pressed
        if (Input.GetMouseButton(0))
        {

            //sets an initial position to Sam's current location
            Vector3 newPosition = this.transform.position;

            //sets x position of new position to the mouse's x position in world
            newPosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            newPosition.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

            //sets Sam's x position to the mouse position
            this.transform.position = newPosition;
            samPosition = newPosition;

        }
#endif
    }

    public Vector3 getSamPosition()
    {
        return this.transform.position;
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
        else
            Debug.Log("Collided with Brandon.");
    }


    /// <summary>
    /// Grabs the food.
    /// </summary>
    /// <param name="food">Food that Sam collided with.</param>
    private void grabFood(Food food)
    {

        //creates an Audio Source on Sam if there isn't one already
        if (!GetComponent<AudioSource>())
            gameObject.AddComponent<AudioSource>();

        //stores Sam's audio source
        AudioSource audioSource = GetComponent<AudioSource>();

        //plays the audio on the food
        audioSource.clip = food.getAudio();
        audioSource.Play();

        //checks if food is healthy, and if so, increments counter and increases speed
        if (food.isHealthy())
        {

            healthyFood++;
            score++;
            controller.updateScrollSpeed(1);
            controller.updateScore(score);

        }

        //checks if food is unhealthy, and if so increments counter and decreases speed
        else
        {

            unhealthyFood++;
            if (score > 0)
            {
                score--;
            }
            controller.updateScore(score);
            controller.updateScrollSpeed(-1);

        }

        //removes the food from existence
        food.remove();

    }

    public float getSpeed()
    {
        return controller.getSpeed();
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
