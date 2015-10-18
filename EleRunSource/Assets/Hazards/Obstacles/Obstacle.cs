using UnityEngine;
using System.Collections;

public class Obstacle : Hazard
{
	private Transform _transform = null;

	void Awake()
	{
		_transform = GetComponent<Transform>();
	}

	protected override void ProcessBehavior ()
	{
		base.ProcessBehavior ();
		_transform.Translate( -Vector2.right * BASE_SPEED * Time.deltaTime );
	}
}
