using UnityEngine;
using UnityEngine.UI;
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

    // The current lane that Sam is on. Default is middle lane.
    private int lane = 1;

    // Reference to the animation.
    Animator charAnim;

    // Reference to the healthy and unhealthy food texts.
    public Text healthyText;
    public Text unhealthyText;

    public Text streakText;

    private bool isJumping = false;
    private float distanceTraveled = 0.0f;
    private float jumpSpeed = 1.0f;
    private bool jumpDirection = false; // false if going left, true if going right.

    private Vector3 leftLanePos;
    private Vector3 middleLanePos;
    private Vector3 rightLanePos;

    private Vector3 scoreSize;  // Original size of the score labels.
    private Vector3 streakSize;

    private int healthyStreak = 0;
        
    void Start()
    {
        charAnim = this.GetComponent<Animator>();
        leftLanePos = this.transform.position - new Vector3(0.5f, 0.0f, 0.0f);
        middleLanePos = this.transform.position;
        rightLanePos = this.transform.position + new Vector3(0.5f, 0.0f, 0.0f);
        scoreSize = healthyText.transform.localScale;
        streakSize = streakText.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Resize text size elements if not at original size.
        healthyText.transform.localScale = Vector3.Lerp(healthyText.transform.localScale, scoreSize, 2.0f * Time.deltaTime);
        unhealthyText.transform.localScale = Vector3.Lerp(unhealthyText.transform.localScale, scoreSize, 2.0f * Time.deltaTime);
        streakText.transform.localScale = Vector3.Lerp(streakText.transform.localScale, streakSize, 0.5f * Time.deltaTime);
        streakText.color = Color.Lerp(streakText.color, Color.clear, 2.0f * Time.deltaTime);

        // Obtain vertical key input.
        float vertSpeed = Input.GetAxisRaw("Vertical");

        if (vertSpeed != 0.0f)
        {
            Vector3 position = this.transform.position;
            position.y += vertSpeed * Time.deltaTime;
            if (position.y < 0.15f)
                position.y = 0.15f;
            if (position.y > 0.50f)
                position.y = 0.50f;
            this.transform.position = position;
        }
        else
        {
            Vector3 position = this.transform.position;
            position.y -= 0.01f;
            if (position.y < 0.15f)
                position.y = 0.15f;
            if (position.y > 0.50f)
                position.y = 0.50f;
            this.transform.position = position;
        }

        if (lane == 0)
        {
            if(isJumping)
            {
                // Vector3 position = leftLanePos;
                // position.x = Mathf.Lerp(leftLanePos.x, middleLanePos.x, jumpSpeed * Time.deltaTime);
                Vector3 position = this.transform.position;
                position.x += 0.10f;
                this.transform.position = position;
                if (Mathf.Approximately(this.transform.position.x, middleLanePos.x))
                {
                    ++lane;
                    isJumping = false;
                }
            }
            else if(Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
            {
                isJumping = true;
                charAnim.speed = 0.1f;
            }
        }
        else if(lane == 1)
        {
            if (isJumping)
            { 
                Vector3 position = this.transform.position;
                position.x += (jumpDirection ? 0.10f : -0.10f);
                this.transform.position = position;
                if (Mathf.Approximately(this.transform.position.x, (jumpDirection ? rightLanePos.x : leftLanePos.x)))
                {
                    if (jumpDirection)
                        ++lane;
                    else
                        --lane;
                    isJumping = false;
                }
            }
            else if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
            {
                isJumping = true;
                jumpDirection = false;
                charAnim.speed = 0.1f;
            }
            else if(Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
            {
                isJumping = true;
                jumpDirection = true;
                charAnim.speed = 0.1f;
            }
        }
        else if(lane == 2)
        {
            if (isJumping)
            {
                Vector3 position = this.transform.position;
                position.x += -0.10f;
                this.transform.position = position;
                if (Mathf.Approximately(this.transform.position.x, middleLanePos.x))
                {
                    isJumping = false;
                    --lane;
                }
            }
            else if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
            {
                isJumping = true;
                charAnim.speed = 0.1f;
            }
        }
                
        // Based the character's speed, adjust the animation speed to match it.
        if(charAnim != null && !isJumping)
        {
            // Create a lerp to match speed with animation speed proportionally.
            charAnim.speed = Mathf.Lerp(0.7f, 2.5f, (getSpeed() / 100.0f));
        }
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
            grabFood(other.GetComponent<Food>());
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
            ++healthyStreak;
            if (healthyStreak >= 3 && healthyStreak < 7)
                ++healthyFood;
            else if (healthyStreak >= 7 && healthyStreak < 12)
                healthyFood += 2;
            else if (healthyStreak >= 12 && healthyStreak < 20)
                healthyFood += 3;
            else if (healthyStreak >= 20)
                healthyFood += 4;
            controller.updateScrollSpeed(1);
            healthyText.text = healthyFood.ToString();
            healthyText.transform.localScale = scoreSize * 4.5f;
        }

        //checks if food is unhealthy, and if so increments counter and decreases speed
        else
        {
            unhealthyFood++;
            healthyStreak = 0;
            controller.updateScrollSpeed(-1);
            unhealthyText.text = unhealthyFood.ToString();
            unhealthyText.transform.localScale = scoreSize * 4.5f;
        }

        // Update streak text.
        if(healthyStreak >= 3 && healthyStreak < 7)
        {
            streakText.text = "HEALTHY\n2X BONUS";
            streakText.color = Color.white;
            streakText.transform.localScale = streakSize * 1.5f;
        }
        else if(healthyStreak >= 7 && healthyStreak < 12)
        {
            streakText.text = "HEALTH MASTER\n3X BONUS";
            streakText.color = Color.cyan;
            streakText.transform.localScale = streakSize * 1.5f;
        }
        else if (healthyStreak >= 12 && healthyStreak < 20)
        {
            streakText.text = "HEALTH DEMI-GOD\n4X BONUS";
            streakText.color = new Color(19, 239, 140);
            streakText.transform.localScale = streakSize * 1.5f;
        }
        else if (healthyStreak >= 20)
        {
            streakText.text = "HEALTH GOD\n5X BONUS";
            streakText.color = new Color(255, 173, 33);
            streakText.transform.localScale = streakSize * 1.5f;
        }

        controller.updateScore(healthyFood);
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
