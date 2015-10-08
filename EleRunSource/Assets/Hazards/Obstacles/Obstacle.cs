using UnityEngine;
using System.Collections;

public class Obstacle : Hazard
{
	private Transform _transform = null;

	private float speedScale = 0.0f;

	void Awake()
	{
		_transform = GetComponent<Transform>();
	}

	protected override void ProcessBehavior ()
	{
		base.ProcessBehavior ();
		speedScale = GameManager.instance_.SpeedScale;
		_transform.Translate( -Vector2.right * speedScale * Time.deltaTime );
	}
}
