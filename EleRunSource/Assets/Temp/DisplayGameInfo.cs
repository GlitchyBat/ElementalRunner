using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayGameInfo : MonoBehaviour
{
	Text text = null;
	Player player = null;

	// Temporary thing for displaying all info on a single text UI element. 0/10 would not keep
	string absolutelyEverything = string.Empty;

	void Awake()
	{
		text = GetComponent<Text>();
	}

	void Start()
	{
		player = GameObject.FindObjectOfType<Player>();
	}

	void Update()
	{
		absolutelyEverything =
				"[Current Element] " + player.GetElement + "\n" +
				"[Elapsed Time] " + GameManager.instance_.GetTime + "\n" +
				"[Score] " + GameManager.score + "[Hi] " + GameManager.highScore + "\n" +
				"Cooldowns:\n" +
				"[Fire]" + player.GetAttunementFromElement( Element.FIRE ).GetCooldown() + "\n" +
				"[Water]" + player.GetAttunementFromElement( Element.WATER ).GetCooldown() + "\n" +
				"[Wind]" + player.GetAttunementFromElement( Element.WIND ).GetCooldown() + "\n" +
				"[Earth]" + player.GetAttunementFromElement( Element.EARTH ).GetCooldown() + "\n";

		text.text = absolutelyEverything;
	}
}
