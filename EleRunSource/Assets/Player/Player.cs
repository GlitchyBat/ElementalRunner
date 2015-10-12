using UnityEngine;
using System.Collections;

[ RequireComponent( typeof( Rigidbody2D ) ) ]
[ RequireComponent( typeof( BoxCollider2D ) ) ]
public class Player : MonoBehaviour
{
	#region References
	private	Rigidbody2D	rb = null;
	private SpriteRenderer render = null;
	#endregion

	#region Events
	public delegate void E_OnPlayerDeath();
	public E_OnPlayerDeath onPlayerDeath;
	public delegate void E_OnSwapAttunement();
	public E_OnSwapAttunement onSwapAttunement;
	#endregion

	#region Element and attuning
	public Attunement[] attunements = new Attunement[4];	// could use list, but for this case
															// actually want a fixed size
	private Attunement currentAttunement = null;
	#endregion

	#region Jumpstuff
	private float jumpForce = 10.0f;
	private float jumpTime = 0.5f;
	#endregion

	#region Accessors
	public Element GetElement
	{
		get{ return currentAttunement.element; }
		set{}
	}

	public bool IsAirborn
	{
		get
		{
			if ( Mathf.Abs(rb.velocity.y) < 0.01f )
				return false;
			else return true;
		}
		private set{}
	}
	#endregion

	void Awake()
	{
		#region References
		rb = GetComponent<Rigidbody2D>();
		render = GetComponent<SpriteRenderer>();
		#endregion

		// assign attunements to an element
		attunements[0].Init( Element.FIRE, Color.red );
		attunements[1].Init( Element.WATER, Color.blue );
		attunements[2].Init( Element.WIND, Color.gray );
		attunements[3].Init( Element.EARTH, Color.yellow );

		SwapToElement( Element.FIRE ); // Fire for initial element
	}

	void Update()
	{
		// Update attunement cooldowns
		foreach ( Attunement a in attunements )
			a.UpdateCooldown();

		#region Player input
		if ( Input.GetKeyDown(KeyCode.Space) && !IsAirborn)
			StartCoroutine( Jump() );

		if ( Input.GetKeyDown( KeyCode.A ) )
			SwapToElement(Element.FIRE);
		if ( Input.GetKeyDown( KeyCode.S ) )
			SwapToElement(Element.WATER);
		if ( Input.GetKeyDown( KeyCode.D ) )
			SwapToElement(Element.WIND);
		if ( Input.GetKeyDown( KeyCode.F ) )
			SwapToElement(Element.EARTH);
		#endregion
	}



	IEnumerator Jump()
	{
		// lazy jumping method. Probably a more controlled and better way to do it later.
		for ( float f=0; f <= jumpTime; f+=Time.deltaTime )
		{
			if ( Input.GetKey(KeyCode.Space) )
				rb.velocity = Vector2.up * jumpForce;
			else
				StopCoroutine( Jump () );
			yield return null;
		}
	}

	public void AttemptDamage( Hazard source )
	{
		if ( source.element != GetElement )
			Kill();
		else
		{
			// Some kind of feedback that player got by without damage
			// particles or sound or whatever.
			source.AddToScore();
			Destroy( source.gameObject );
		}
	}

	#region Element functions
	void SwapToElement( Element newElement )
	{
		print ( "Swap to " + newElement );
		if ( GetAttunementFromElement( newElement ).IsAvailable )
		{
			if ( onSwapAttunement != null )
				onSwapAttunement();

			if ( currentAttunement != null )			// check if currentAttunement was assigned before
				currentAttunement.OnSwapOut();	// trying to call its OnSwapOut()

			currentAttunement = GetAttunementFromElement( newElement );
			render.color = currentAttunement.color;

			currentAttunement.OnSwapIn();
		}
		else 
		{
			print (newElement + " not charged" );
		}
	}

	public Attunement GetAttunementFromElement( Element target )
	{
		// grab the attunement of respective element
		foreach ( Attunement a in attunements )
		{
			if ( a.element == target )
				return a;
		}
		// if no attunement had a matching element, something's really wrong
		Debug.LogError( name + ": GetAttunementFromElement() failed! What the hell did you do?" );
		Debug.Break();
		return null;
	}
	#endregion

	void Kill()
	{
		// TODO decent feedback that you died, before game over
		if ( onPlayerDeath != null )
			onPlayerDeath();
		//GameManager.instance_.EndGame ();
	}
}
