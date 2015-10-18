using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
	private Vector2 cellSpawnPoint = new Vector2( 14.0f, 3.6f );

	List<LevelCell> allCells = new List<LevelCell>();
	List<LevelCell> possibleCells = new List<LevelCell>();

	int difficultyScale = 4;

	public float spawnRateInSeconds = 1.5f;
	bool isSpawnActive = false;

	void Awake()
	{
		allCells = LoadAllLevelCells();

		if ( allCells.Count == 0 )
		{
			Debug.LogError( name + ": No cells found in " + allCells );
			Debug.Break();
		}
	}

	void Start()
	{
		isSpawnActive = true;
		StartCoroutine( PulseGenerate() );
	}

	#region LevelCell list management
	List<LevelCell> LoadAllLevelCells( )
	{
		List<LevelCell> lcl = new List<LevelCell>();
		lcl.AddRange( Resources.LoadAll<LevelCell>("LevelCells/") );

		// uses 'for' due to 'foreach' enumerators breaking from modifying the list in real time
		// checks each loaded cell to make sure nothing's just garbage or otherwise
		// not meant to be used
		for (int i = 0; i < lcl.Count; i++)
		{
			LevelCell lc = lcl [i];
			if (!lc.IsUsable)
				lcl.Remove (lc);
		}

		return lcl;
	}

	void ReassignPossibleCells( int difficultyValue ) 
	{
		possibleCells.Clear();
		foreach ( LevelCell cell in allCells )
		{
			if ( cell.difficultyValue <= difficultyValue )
				possibleCells.Add( cell );
		}
	}
	#endregion

	#region Generation
	IEnumerator PulseGenerate()
	{
		ReassignPossibleCells( difficultyScale );
		while ( isSpawnActive )
		{
			Spawn( difficultyScale );
			yield return new WaitForSeconds( spawnRateInSeconds );
		}
	}

	LevelCell SelectCell( int difficultyValue )
	{
		LevelCell selected = possibleCells[ Random.Range( 0, possibleCells.Count ) ];
		return selected;
	}

	void Spawn( int difficultyValue )
	{
		LevelCell selectedCell = SelectCell( difficultyValue );
		Instantiate( selectedCell, cellSpawnPoint,Quaternion.identity );
	}
	#endregion
}
