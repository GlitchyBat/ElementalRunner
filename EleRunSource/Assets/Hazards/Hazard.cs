using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( BoxCollider2D ) ) ]
public abstract class Hazard : MonoBehaviour
{
	LevelCell creator = null;

	protected const float BASE_SPEED = 7.5f;

	public Element element = Element.UNINITIALIZED;
	public int pointValue = 0;		// typically used for if player matches element on collision or something, would go to score

	private void SetLevelCell( LevelCell master )
	{
		creator = master;
	}

	protected virtual void Update()
	{
		// destroy when left from camera frustrum
		if ( transform.position.x < -10.0f )
			Destroy( gameObject );

		ProcessBehavior();
	}

	protected virtual void ProcessBehavior() {}

	void OnTriggerEnter2D( Collider2D other )
	{
		if ( other.GetComponent<Player>() )
		{
			other.GetComponent<Player>().RecieveDamage( this );
		}
	}

	protected void OnDestroy()
	{
		//creator.
	}

	#region Score
	public void AddToScore()
	{
		GameData.AddScore( pointValue );
	}

	public void AddToScore( int value )
	{
		GameData.AddScore (value);
	}
	#endregion
}
