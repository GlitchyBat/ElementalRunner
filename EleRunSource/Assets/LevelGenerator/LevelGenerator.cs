using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// HACK this whole thing is one
public class LevelGenerator : MonoBehaviour
{
	// level cells not implimented yet
	//public List<LevelCell> cells = new List<LevelCell>();

	#region temporary - for bare bones level generator
	public List<Transform> spawnPoints = new List<Transform>();
	public List<Hazard> hazards = new List<Hazard>();
	#endregion

	public float spawnRateInSeconds = 1.5f;
	bool isSpawnActive = false;

	void Awake()
	{
		#region safety with lists
		// remove null entries in hazards
		foreach ( Hazard h in hazards )
		{
			if ( !h )
				hazards.Remove( h );
		}

		// if no hazards
		if ( hazards.Count == 0 )
		{
			Debug.LogError( name + ": No hazards are in list! Screeeeach!" );
			Debug.Break();
		}

		// if no positions
		if ( spawnPoints.Count == 0 )
		{
			Debug.LogError( name + ": No spawn points to use! Screeeeach!" );
			Debug.Break();
		}
		#endregion
	}

	void Start()
	{
		isSpawnActive = true;
		StartCoroutine( PulseGenerate() );
	}

	IEnumerator PulseGenerate()
	{
		while ( isSpawnActive )
		{
			Spawn();
			yield return new WaitForSeconds( spawnRateInSeconds );
		}
	}

	void Spawn()
	{
		// temp- used for tracking unattunable block spawns so you never have a wall of three unblockables
		bool unattunableSpawned = false;

		foreach ( Transform p in spawnPoints )
		{
			// throwaway code just used to get unblockables fairly working - only spawn 1 unattunable per line
			if ( !unattunableSpawned )
			{
				Hazard h = (Hazard)Instantiate(
					hazards[Random.Range( 0,hazards.Count )],
					p.position, Quaternion.identity );
				if ( h.element == Element.UNINITIALIZED )
					unattunableSpawned = true;
			}
			else
			{
				Instantiate(
					hazards[Random.Range( 0,hazards.Count-1 )], // assumes 0-3 are element blocks and 4 is no element
					p.position, Quaternion.identity );
			}
		}
	}
}
