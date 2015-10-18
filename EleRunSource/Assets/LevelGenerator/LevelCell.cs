using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelCell : MonoBehaviour
{
	public float despawnTime = 8.5f;

	public int difficultyValue = 0;
	public bool doNotUse = false;

	void Awake()
	{
		StartCoroutine( CountdownToDespawn( despawnTime ) );
	}

	public bool IsUsable
	{
		get
		{
			if ( difficultyValue > 0 && !doNotUse )
				return true;
			else return false;
		}
		private set{}
	}

	IEnumerator CountdownToDespawn( float seconds )
	{
		yield return new WaitForSeconds( seconds );
		Destroy( gameObject );
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube( transform.position,
		                new Vector3( 10.0f,8.0f ) );
		Gizmos.color = Color.green;
		Gizmos.DrawRay( transform.position, Vector3.left * 10.0f );
		Gizmos.color = Color.white;
	}
}
