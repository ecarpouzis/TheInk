﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    float timeToStart = 1f;
    float timeSinceStart = 0f;
    int maxGameTime = 60;
    public CameraController CameraController;
    public CharacterController2D CharacterController;
    TimeController characterTimeController;
    public static GameController G;
    public bool isRewinding = false;
    public bool isPlaying = false;
    float timePlaying = 0f;
    float timeRewinding = 0f;
    public float currentTimePoint = 0f;
    public UnityEngine.UI.Text TimeLeft;
    public float percThroughTime;

    public float timeSinceRewind = 0f;
    float minTimeBetweenRewinds = .25f;

    private void Awake()
    {
        characterTimeController = CharacterController.GetComponent<TimeController>();
        G = this;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceRewind += Time.deltaTime;
        timeSinceStart += Time.deltaTime;
        if (timeSinceStart > timeToStart)
        {
            StartPlaying();
        }
        if (isPlaying)
        {
            if (isRewinding)
            {
                timeRewinding += Time.deltaTime;
            }
            else
            {
                timePlaying += Time.deltaTime;
            }

            //Total time elapsed since game start
            currentTimePoint = timePlaying - timeRewinding;
            percThroughTime = ((currentTimePoint - 0) / (maxGameTime - 0));

            TimeLeft.text = GetSecondsLeftAsString();

            if (GameController.G.currentTimePoint > 0)
            {
                if (Input.GetButton("Rewind") && timeSinceRewind > minTimeBetweenRewinds)
                {
                    isRewinding = true;
                }
                if (Input.GetButtonUp("Rewind"))
                {
                    timeSinceRewind = 0;
                    isRewinding = false;
                }
            }
            else
            {
                timeSinceRewind = 0;
                isRewinding = false;
            }

        }
    }

    int GetSecondsLeft()
    {
        return maxGameTime - (int)currentTimePoint;
    }
    string GetSecondsLeftAsString()
    {
        return (maxGameTime - (int)currentTimePoint).ToString();
    }



    void StartPlaying()
    {
        isPlaying = true;
    }
}
