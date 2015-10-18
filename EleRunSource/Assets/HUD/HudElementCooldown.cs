using UnityEngine;
using System.Collections;

public class HudElementCooldown : MonoBehaviour 
{
	//Transform _transform = null;
	Renderer _renderer = null;

	Player player = null;

	public Element element = Element.UNINITIALIZED;

	void Awake()
	{
		//_transform = GetComponent<Transform>();
		_renderer = GetComponent<Renderer>();
	}

	void Start()
	{
		player = GameObject.FindObjectOfType<Player>();
	}

	void Update()
	{
		// TODO _transform scale to full size from nothing if cooldown for this attunement is 0.0f 

		if ( player.GetAttunementFromElement(element).IsAvailable )
			_renderer.enabled = true;
		else
			_renderer.enabled = false;
	}
}
