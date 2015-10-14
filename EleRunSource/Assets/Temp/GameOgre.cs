using UnityEngine;
using System.Collections;

public class GameOgre : MonoBehaviour
{
	// temp for throwing player into a game over screen and letting them get back in
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
			Application.LoadLevel ("Main");
			GameManager.instance_.BeginGame ();
	}
}
