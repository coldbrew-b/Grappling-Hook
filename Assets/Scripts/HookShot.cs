using UnityEngine;
using System.Collections;

public class HookShot : MonoBehaviour {

	public float lifetime = 1f;
	public float strength = 100f;
	public float smoothing = 5f;

	private bool anchored = false;
	private float distance;
	private Vector3 delta;
	private Rigidbody hookRb;
	private Rigidbody playerRb;


	void Start () 
	{
		hookRb = GetComponent <Rigidbody> ();
		playerRb = GameObject.FindGameObjectWithTag ("Player").GetComponent <Rigidbody> ();

		Invoke("Expire", lifetime);
	}
		
	void OnCollisionEnter (Collision col) 
	{
		hookRb.velocity = Vector3.zero;
		anchored = true;

		playerRb.AddForce(Vector3.up * 50, ForceMode.Impulse);
		playerRb.AddForce ((transform.position - playerRb.position).normalized * strength, ForceMode.VelocityChange);
//		playerRb.velocity = (transform.position - playerRb.position).normalized * strength;
	}

	void FixedUpdate ()
	{
		if (anchored) {
			PullPlayer ();
		}
	}

	void PullPlayer () 
	{
		distance = (playerRb.position - transform.position).magnitude;
		if (distance > 2) {
			playerRb.AddForce ((transform.position - playerRb.position).normalized * strength);
		} else {
			delta = playerRb.position - transform.position;
			delta.y = 0;
			playerRb.AddForce ((playerRb.position - transform.position).normalized * smoothing, ForceMode.VelocityChange);
//			playerRb.velocity = Vector3.up + delta.normalized;

			Destroy (gameObject);
		}
	}

	void Expire()
	{
		if (!anchored) {
			Destroy (gameObject);
		}
	}
}
