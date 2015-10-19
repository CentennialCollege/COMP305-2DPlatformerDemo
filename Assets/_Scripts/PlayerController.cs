using UnityEngine;
using System.Collections;

// VELOCITYRANGE UTILITY CLASS +++++++++++++++++++++++++
[System.Serializable]
public class VelocityRange {
	// PUBLIC INSTANCE VARIABLES
	public float vMin, vMax;

	// CONSTRUCTOR ++++++++++++++++++++++++++++++++
	public VelocityRange(float vMin, float vMax) {
		this.vMin = vMin;
		this.vMax = vMax;
	}
}

// PLAYERCONTROLLER CLASS +++++++++++++++++++++++++++++++++++++
public class PlayerController : MonoBehaviour {
	//PUBLIC INSTANCE VARIABLES
	public float speed = 50f;
	public float jump = 500f;
	public VelocityRange velocityRange = new VelocityRange (300f, 1000f);



	//PRIVATE INSTANCE VARIABLES
	private AudioSource[] _audioSources;
	private AudioSource _coinSound;
	private Rigidbody2D _rigidbody2D;
	private Transform _transform;
	private Animator _animator; // for later use

	private float _moving = 0;

	// Use this for initialization
	void Start () {
		this._rigidbody2D = gameObject.GetComponent<Rigidbody2D> ();
		this._transform = gameObject.GetComponent<Transform> ();


		this._audioSources = gameObject.GetComponents<AudioSource> ();
		this._coinSound = this._audioSources[0];
	
	}

	void FixedUpdate () {
		float forceX = 0f;
		float forceY = 0f;

		float absVelX = Mathf.Abs (this._rigidbody2D.velocity.x);
		float absVelY = Mathf.Abs (this._rigidbody2D.velocity.y);
	
		this._moving = Input.GetAxis ("Horizontal"); // gives moving variable a value of -1 to 1

		if (this._moving != 0) { // player is moving
			if(this._moving > 0) {
				// move right
				if(absVelX < this.velocityRange.vMax) {
					forceX = this.speed;
					// make sure he is facing right
				}
			} else if (this._moving < 0) {
				// move left
				if(absVelX < this.velocityRange.vMax) {
					forceX = -this.speed;
					// make sure he is facing right
				}
			}

		} else {
			// set our idle animation here
		}

		this._rigidbody2D.AddForce (new Vector2 (forceX, forceY));
	}

	void OnCollisionEnter2D(Collision2D otherCollider) {
		if (otherCollider.gameObject.CompareTag ("Coin")) {
			this._coinSound.Play();
		}
	}
}
