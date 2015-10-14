using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayGameInfo : MonoBehaviour
{
	Text text = null;
	Player player = null;

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
		// temp writing
		text.text =
			"[Current Element] " + player.GetElement + "\n" +
			"[Elapsed Time] " + GameManager.instance_.GetTime + "\n" +
			"[Score] " + GameData.score + "[Hi] " + GameData.highScore + "\n" +
			"Cooldowns:\n" +
			"[Fire]" + player.GetAttunementFromElement( Element.FIRE ).GetCooldown() + "\n" +
			"[Water]" + player.GetAttunementFromElement( Element.WATER ).GetCooldown() + "\n" +
			"[Wind]" + player.GetAttunementFromElement( Element.WIND ).GetCooldown() + "\n" +
			"[Earth]" + player.GetAttunementFromElement( Element.EARTH ).GetCooldown() + "\n";
	}
}
