﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Game Controller for the falling food game.
/// </summary>
public class FallingFoodController : MonoBehaviour, SpeedChanger
{
    //coroutine flag
    private bool continueFlow = false;
    
    public SamController sam;

    public MonsterController monster;
    
    //track prefab to spawn tracks from
    public Track regularTrackPrefab;

    //array of food prefabs
    public Food[] foodPrefabs;

    public Sprite finishLineSprite;

    //scoreboard visible on screen
    public PercentScores scoreTracker;

    public FoodScore finalScore;

    //all speed listeners current listening for speed changes
    List<SpeedListener> speedListeners;

    // speed of character
    private float characterSpeed = 25;

    //speed of everything that scrolls
    private float scrollSpeed;

    //the percent speed that the player starts at
    private float startSpeedPercent = 25;

    //the duration of the race *at starting speed*, per second
    //i.e. faster speeds completes sooner, slower takes longer.
    [SerializeField]
    private float totalRaceDuration = 300;

    //current progress of the race, out of totalRaceDuration
    private float raceProgress = 0;

    //how much food, on average, will be on the screen at a given time
    private int numFoodOnScreen = 5;

    //the speed (in %) that will change each time food is collected
    private float speedChangePercent = 2;

    //min and max speed values - not in percent
    private float minSpeed = 0.1f, maxSpeed = 2;

    //depth of track from the camera - ensures it's visible
    private float trackOffsetFromCamera = 10;

    //array of two tracks that will be looping to simulate scroll
    private Track[] scrollingTracks;

    //indeces for the track array
    private const int LOWER_TRACK_INDEX = 0, UPPER_TRACK_INDEX = 1;

    private Timer gameTimer;

    public static bool samFinish = false;

    private float cameraSize;   // Size of camera in orthographic view.

    // Use this for initialization
    void Start()
    {

        //Sets the initial scroll speed based on desired starting percent value
        scrollSpeed = ((startSpeedPercent / 100) * (maxSpeed - minSpeed)) + minSpeed;

        //initializes array of speed listeners
        speedListeners = new List<SpeedListener>();

        //spawns two tracks that will be used throughout the game
        scrollingTracks = spawnTracks();
        
        //initializes the score tracker with correct speeds
        scoreTracker.Initialize(minSpeed, maxSpeed, scrollSpeed);

        //registers the score tracker as a speed listener
        registerSpeedListener(scoreTracker);

        gameTimer = new Timer();

        cameraSize = Camera.main.orthographicSize * 2;

        // Start coroutine for spawning food.
        StartCoroutine(SpawnFood());
    }

    void Update()
    {
        //updates race completion value
        updateCompletion();
    }

    /// <summary>
    /// Spawns the two initial tracks that will loop for rest of minigame
    /// </summary>
    private Track[] spawnTracks()
    {

        Track[] scrollingTracks = new Track[2];

        //the location where the track will spawn- starting with camera position
        Vector3 trackSpawnPosition = Camera.main.transform.position;

        //changes depth to ensure track is visible
        trackSpawnPosition.z += trackOffsetFromCamera;

        //loops twice to spawn two tracks
        for (int trackNum = 0; trackNum < 2; trackNum++)
        {

            //creates and stores a new track
            scrollingTracks[trackNum] = (Track)GameObject.Instantiate(regularTrackPrefab, trackSpawnPosition, new Quaternion());

            //registers the new track as a speed listener - will get speed updates
            registerSpeedListener(scrollingTracks[trackNum]);

            //initializes track values (only speed, in this case)
            scrollingTracks[trackNum].Initialize(scrollSpeed);

            //increases track spawn position for next track piece
            trackSpawnPosition.y += scrollingTracks[trackNum].getTrackHeight();

        }

        return scrollingTracks;

    }

    IEnumerator SpawnFood()
    {
        // Determine the location of where to spawn food.
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        while (!samFinish)
        {
            // Pick one of three lanes to spawn food.
            int spawnLane = Random.Range(0, 3);

            /* Three types of spawn options: All healthy food, all unhealthy food, or a mix of both. */
            int spawnType = Random.Range(0, 3);

            // Determine how many foods to spawn for this iteration.
            int spawnAmt = Random.Range(1, 4);

            // Grab a random location relative to camera boundaries, then overwrite the x-axis value.
            Vector3 randomLocation = MyRandom.Location2D(Camera.main.transform.position, cameraWidth, cameraHeight);

            switch (spawnLane)
            {
                case 0:
                    randomLocation.x = -0.84f;
                    break;
                case 1:
                    randomLocation.x = -0.34f;
                    break;
                case 2:
                default:
                    randomLocation.x = 0.16f;
                    break;
            }

            // Add camera offset to food location to move it out of camera view.
            randomLocation.y += cameraSize;

            // Change depth of new location to ensure spawned food will be visible and above track.
            randomLocation.z = Camera.main.transform.position.z + trackOffsetFromCamera - 1;

            if(spawnType == 0)
            {
                // Spawn all healthy food.
                for(int i = 0; i < spawnAmt; ++i)
                {
                    // Get a random healthy food.
                    Food randomFood = foodPrefabs[Random.Range(0, 3)];

                    // Instantiate that food.
                    Food newFood = (Food)GameObject.Instantiate(randomFood, new Vector3(randomLocation.x,
                        randomLocation.y + 0.15f * i, randomLocation.z), new Quaternion());
                    newFood.Initialize(scrollSpeed, this);
                    registerSpeedListener(newFood);
                }
            }
            else if(spawnType == 1)
            {
                // Spawn all unhealthy food.
                for (int i = 0; i < spawnAmt; ++i)
                {
                    // Get a random healthy food.
                    Food randomFood = foodPrefabs[Random.Range(3, 6)];

                    // Instantiate that food.
                    Food newFood = (Food)GameObject.Instantiate(randomFood, new Vector3(randomLocation.x,
                        randomLocation.y + 0.15f * i, randomLocation.z), new Quaternion());
                    newFood.Initialize(scrollSpeed, this);
                    registerSpeedListener(newFood);
                }
            }
            else if(spawnType == 2)
            {
                // Spawn a mix of healthy and unhealthy food.
                for (int i = 0; i < spawnAmt; ++i)
                {
                    // Get a random healthy food.
                    Food randomFood = foodPrefabs[Random.Range(0, 6)];

                    // Instantiate that food.
                    Food newFood = (Food)GameObject.Instantiate(randomFood, new Vector3(randomLocation.x,
                        randomLocation.y + 0.15f * i, randomLocation.z), new Quaternion());
                    newFood.Initialize(scrollSpeed, this);
                    registerSpeedListener(newFood);
                }
            }
            
            yield return new WaitForSeconds(0.7f);
        }
        yield return null;
    }

    /// <summary>
    /// Updates the race completion value and displays on scoreboard
    /// </summary>
    private void updateCompletion()
    {
        //current rate of track completion as a numerical value, based on current speed
        float rateOfCompletion = (100 / startSpeedPercent) * ((scrollSpeed - minSpeed) / (maxSpeed - minSpeed));

        //increments track progress based on current rate of completion
        raceProgress += (rateOfCompletion) * (Time.deltaTime);

        //calculates completion percentage out of total race duration
        float completionPercentage = ((raceProgress / totalRaceDuration) * 100);

        if (completionPercentage >= 100 || monster.isSamCaught())
        {

            if (!monster.isSamCaught())
            {
                samFinish = true;
            }
            StartCoroutine(Process());
            if (continueFlow)
            {
                completionPercentage = 100;
                scrollingTracks[UPPER_TRACK_INDEX].setFinish(finishLineSprite);
                finalScore.gameObject.SetActive(true);
                finalScore.Initialize(gameTimer, sam.getHealthyFood(), sam.getUnhealthyFood());
                scrollSpeed = 0;
                updateSpeedListener();
            }

        }


        //displays the new completion percentage on the score tracker
        scoreTracker.displayRaceCompletion(completionPercentage);

    }

    /// <summary>
    /// Registers a speed listener.
    /// </summary>
    /// <param name="speedListenerObject">Speed listener object.</param>
    public void registerSpeedListener(SpeedListener speedListenerObject)
    {
        //registers the speedListenerObject as a speedListener to get updates
        speedListeners.Add(speedListenerObject);

    }

    /// <summary>
    /// Removes a speed listener.
    /// </summary>
    /// <param name="speedListenerObject">Speed listener object.</param>
    public void removeSpeedListener(SpeedListener speedListenerObject)
    {
        //removes the speedListenerObject from the speedListener list
        speedListeners.Remove(speedListenerObject);
    }

    /// <summary>
    /// Updates all speed listeners
    /// </summary>
    public void updateSpeedListener()
    {
        //updates all speedListeners with the new and correct speed
        foreach (SpeedListener speedListenerObject in speedListeners)
        {

            speedListenerObject.updateScrollSpeed(scrollSpeed);

        }
    }

    /// <summary>
    /// Updates the scroll speed.
    /// </summary>
    /// <param name="percentChange">Positive or negative speed change:
    /// -1 if negative and 1 if positive
    /// </param>
    public void updateScrollSpeed(int change)
    {
        //initially calculates current speed as a percent
        float newPercentSpeed = 100 * ((this.scrollSpeed - minSpeed) / (maxSpeed - minSpeed));

        //increments speed by the desired percent (either positive or negative
        newPercentSpeed += (change * speedChangePercent);

        //calculates new speed based on desired speed percent
        float newSpeed = (newPercentSpeed / 100) * (maxSpeed - minSpeed) + minSpeed;

        //if the new calculated speed is within max/min bounds
        if (newSpeed >= minSpeed && newSpeed <= maxSpeed)
        {
            //update scroll speed in this class
            this.scrollSpeed = newSpeed;

            //notify all speed listeners of speed change
            updateSpeedListener();
        }

        //if speed change is too high
        else if (newSpeed >= maxSpeed)
        {
            //display a speed percent of 100 by default, exceeded max
            scoreTracker.displaySpeedPercent(100);
        }

        //if speed change is too low
        else if (newSpeed <= minSpeed)
        {
            //display a speed percent of 1 by default, exceeded min
            scoreTracker.displaySpeedPercent(1);
        }

        //updates Sam's speed
        this.characterSpeed = newPercentSpeed;
    }

    // Get Character's Speed
    public float getSpeed()
    {
        return characterSpeed;
    }

    /// <summary>
    /// Removes food from the game after it's been used or scrolled past
    /// </summary>
    /// <param name="food">Food to remove.</param>
    public void removeFood(Food food)
    {

        // Removes this food from the list of speedListeners
        speedListeners.Remove(food);

        // Destroys the food gameObject
        GameObject.Destroy(food.gameObject);
    }
    public void updateScore(int score)
    {
        scoreTracker.displayScore(score);
    }

    IEnumerator Process()
    {
        //Wait 1 second
        yield return StartCoroutine(Wait(5.0f));
        continueFlow = true;
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}
