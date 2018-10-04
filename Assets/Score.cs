/*
 * Copyright (c) Julian McNeill
 * http://julianmcneill.com
*/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    int score = 0;
    Text scoreDisplay;

	void Start()
	{
        scoreDisplay.text = score.ToString();
	}
	
	void Update()
	{
		
	}

    public void AddScore(int points)
    {
        score += points;
        scoreDisplay.text = score.ToString();
    }
}