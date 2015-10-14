using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager instance_ = null;

	#region References
	public Player player = null;
	#endregion

	bool isGameActive = false;

	const float	MIN_SPEED	= 0.1f,
				MAX_SPEED	= 7.5f;

	private	float speedScale	= 2.0f;

	#region Accessors
	public float SpeedScale
	{
		get
		{
			float speed = speedScale + (GameData.elapsedTime / 2);
			return Mathf.Clamp( speed,MIN_SPEED,MAX_SPEED );
		}
		private set {}
	}

	public int GetTime
	{
		get{ return Mathf.FloorToInt(GameData.elapsedTime); }
		private set{}
	}
	#endregion

	void Awake()
	{
		// singleton stuff
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
		GameData.Update();
	}

	void AddScore( int amount ) { GameData.AddScore (amount); }

	IEnumerator IncrementScorePerSecond()
	{
		while ( isGameActive )
		{
			yield return new WaitForSeconds( 1.0f );
			GameData.score++;
		}
	}

	public void BeginGame()
	{
		RefreshReferences();
		isGameActive = true;
		GameData.Reset();
		speedScale = 2.0f;
		StartCoroutine( IncrementScorePerSecond() );

		// subscribe events
		player.onPlayerDeath += EndGame;
	}

	public void EndGame()
	{
		// unsubscribe events
		player.onPlayerDeath -= EndGame;


		player = null;
		//

		// move on
		isGameActive = false;
		Application.LoadLevel( "GameOver" );
	}

	void RefreshReferences()
	{
		#region player
		if ( !GameObject.FindObjectOfType<Player>().GetComponent<Player>() )
		{
			Debug.LogError (name + ": Player not found you dolt!");
			Debug.Break ();
		}
		else
			player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
		#endregion
	}
}
