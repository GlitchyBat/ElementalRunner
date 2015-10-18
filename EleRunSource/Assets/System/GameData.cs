using UnityEngine;
using System.Collections;

[System.Serializable]
public static class GameData
{
	public static int highScore = 0;
	public static float bestElapsedTime = 0.0f;

	public static int score = 0;
	public static float elapsedTime = 0.0f;

	public static void AddScore( int value ) { score += value; }

	public static void OnGameUpdate()
	{
		elapsedTime += Time.deltaTime;
		
		if ( score > highScore )
		{
			highScore = score;
			bestElapsedTime = elapsedTime;
		}
	}

	public static void Reset()
	{
		score = 0;
		elapsedTime = 0.0f;
	}

	public static bool Save() { return false; }
	public static void Load() {  }
}
