﻿using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

/// <summary>
/// Script that is needed to give functionality to in-game UI. It updates all texts displayed
/// in the in-game UI with the data received from the bike and game.
/// </summary>
public class UIOverlay : MonoBehaviour
{
	public static BikeController bikeController;

	public TimeSpan currentTime;

	public GameObject startCollider;

	public GameObject finishCollider;

	public static void SetBikeController(BikeController bikeController)
	{
		UIOverlay.bikeController = bikeController;

	}

    private StandardUI stdUI;

	void Start()
	{
        // Get the Standard UI
        this.stdUI = GameObject.FindObjectOfType<StandardUI>();
        if (this.stdUI == null)
        {
            Debug.LogWarning("[UIOverlay] StandardUI not found! UIOverlay is DISABLED.");
            return;
        }

    }

	void Update()
	{
		GameObject startCollider = GameObject.FindGameObjectWithTag("Start");
		GameObject finishCollider = GameObject.FindGameObjectWithTag("Finish");

		if (startCollider == null && finishCollider != null) {
			if (bikeController._timerStarted == false) {
				bikeController._timerStarted = true;
				bikeController._refTime = DateTime.Now;
			}

			if (this.stdUI == null)
				return;

			// Timer
			if (bikeController.TimerStarted) {
				

				currentTime = DateTime.Now - bikeController.ReferenceTime;

				this.stdUI.TimeText.text = String.Format ("{3:#0}:{2:00}:{1:00}.{0:00}",
					Mathf.Floor ((float)currentTime.Milliseconds / 10),
					Mathf.Floor ((float)currentTime.Seconds),
					Mathf.Floor ((float)currentTime.Minutes),
					Mathf.Floor ((float)currentTime.TotalHours));
				
			} else {
				
				this.stdUI.TimeText.text = "--:--:--.--";

			}

			// Distance (convert from m to km)
			String distanceText = String.Format ("{0:#0.0}", bikeController.DistanceTravelled / 1000f);
			this.stdUI.DistText.text = distanceText;

			// Velocity (convert from m/s to km/h)
			this.stdUI.VelText.text = String.Format ("{0:#0.0}", bikeController.bikePhysics.Velocity * 3.6f);

			// Power (watts)
			this.stdUI.PWRText.text = String.Format ("{0:#0.0}", bikeController.bikeData.BikePWR);

			// No Heart Rate in this version
			this.stdUI.HRText.text = "--";
		}
	}
}
