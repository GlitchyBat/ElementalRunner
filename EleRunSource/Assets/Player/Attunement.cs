using UnityEngine;
using System.Collections;

[System.Serializable]
public class Attunement
{
	const float baseCooldown = 1.0f;
	private float cooldownTimer = 0.0f;
	bool isAttuned = false;

	// temp
	public Color color = Color.black;

	public Element element = Element.UNINITIALIZED;

	#region Accessors
	public bool IsAvailable
	{
		get
		{
			if ( cooldownTimer == 0.0f )
				return true;
			else
				return false;
		}
		private set {}
	}

	// mainly used by HUD for showing cooldown
	public float GetCooldown() { return cooldownTimer; }
	#endregion

	public void Init( Element element, Color color )
	{
		this.element = element;
		this.color = color;
	}

	#region Called on player swapping attunements
	public void OnSwapIn()
	{
		cooldownTimer = baseCooldown;
		isAttuned = true;
	}

	public void OnSwapOut()
	{
		cooldownTimer = baseCooldown;
		isAttuned = false;
	}
	#endregion

	// called by player in Update when not the current attunement
	public void UpdateCooldown()
	{
		if ( !isAttuned )
		{
			cooldownTimer = Mathf.Clamp( cooldownTimer, 0.0f, 2.0f );
			if (cooldownTimer > 0.0f)
				cooldownTimer -= Time.deltaTime;
		}
	}
}
