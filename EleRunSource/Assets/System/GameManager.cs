using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	#region References
	public Player player = null;
	#endregion

	bool isGameActive = false;

	#region Accessors
	public int GetTime
	{
		get{ return Mathf.FloorToInt(GameData.elapsedTime); }
		private set{}
	}
	#endregion


	void Start()
	{
		if ( GameObject.FindObjectOfType<Player>() )
			player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();

		if (player)
			player.onPlayerDeath += EndGame;

		BeginGame();
	}

	void Update()
	{
		GameData.OnGameUpdate();
	}

	void AddScore( int amount ) { GameData.AddScore (amount); }

	IEnumerator IncrementScorePerSecond()
	{
		while ( isGameActive )
		{
			yield return new WaitForSeconds( 1.0f );
			GameData.AddScore(1);
		}
	}

	public void BeginGame()
	{
		isGameActive = true;
		GameData.Reset();
		StartCoroutine( IncrementScorePerSecond() );
	}

	public void EndGame()
	{
		// Remove any event subscriptions
		player.onPlayerDeath -= EndGame;

		// move on
		isGameActive = false;
		Application.LoadLevel( "GameOver" );
	}
}
