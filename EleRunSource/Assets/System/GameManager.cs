﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager	instance_ = null;

	#region References
	public Player player;
	#endregion

	bool isGameActive = false;

	const float	minSpeed	= 0.1f,
				maxSpeed	= 7.5f;

	private	float speedScale	= 2.0f;

	public static int score = 0;
	public static int highScore = 0;
	private float elapsedTime = 0.0f;
	// TODO store time for the game with the high score

	#region Accessors
	public float SpeedScale
	{
		get
		{
			float speed = speedScale + (elapsedTime / 2);
			return Mathf.Clamp( speed,minSpeed,maxSpeed );
		}
		private set {}
	}

	public int GetTime
	{
		get{ return Mathf.FloorToInt(elapsedTime); }
		private set{}
	}
	#endregion

	void Awake()
	{
		#region References
		player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
		#endregion

		if (instance_)
		{
			Destroy (gameObject);
		}
		else
		{
			instance_ = this;
			DontDestroyOnLoad (this);
		}
	}

	void Start()
	{
		BeginGame();
	}

	void Update()
	{
		elapsedTime += Time.deltaTime;

		if ( score > highScore )
			highScore = score;
	}

	void AddScore( int amount ) { score += amount; }

	IEnumerator IncrementScorePerSecond()
	{
		while ( isGameActive )
		{
			yield return new WaitForSeconds( 1.0f );
			score++;
		}
	}

	public void BeginGame()
	{
		Application.LoadLevel( "Main" );

		isGameActive = true;
		score = 0;
		elapsedTime = 0.0f;
		speedScale = 2.0f;
		StartCoroutine( IncrementScorePerSecond() );

		// subscribe events
		player.onPlayerDeath += this.EndGame;
		player.onSwapAttunement += this.AddScore(5);
	}

	public void EndGame()
	{
		// unsubscribe events
		player.onPlayerDeath -= this.EndGame;
		player.onSwapAttunement -= this.AddScore(5);

		// move on
		isGameActive = false;
		Application.LoadLevel( "GameOver" );
	}
}
