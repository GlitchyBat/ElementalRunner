using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayGameInfo : MonoBehaviour
{
	Text text = null;

	GameManager gameManager = null;
	Player player = null;

	void Awake()
	{
		text = GetComponent<Text>();
	}

	void Start()
	{
		gameManager = GameObject.FindObjectOfType<GameManager>();
		player = GameObject.FindObjectOfType<Player>();
	}

	void Update()
	{
		// temp writing
		text.text =
			"[Current Element] " + player.GetElement + "\n" +
			"[Elapsed Time] " + gameManager.GetTime + "\n" +
			"[Score] " + GameData.score + "[Hi] " + GameData.highScore + "\n" +
			"Cooldowns:\n" +
			"[Fire]" + player.GetAttunementFromElement( Element.FIRE ).GetCooldown() + "\n" +
			"[Water]" + player.GetAttunementFromElement( Element.WATER ).GetCooldown() + "\n" +
			"[Wind]" + player.GetAttunementFromElement( Element.WIND ).GetCooldown() + "\n" +
			"[Earth]" + player.GetAttunementFromElement( Element.EARTH ).GetCooldown() + "\n";
	}
}
