using UnityEngine;
using System.Collections;
using WindowsInput;

public class HookShot : MonoBehaviour {

	public float lifetime = 1f;
	public float strength = 100f;

	private bool anchored = false;
	private Vector3 delta;
	private Rigidbody hookRb;
	private Rigidbody playerRb;
	private UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController playerController;		

	void Start () 
	{
		hookRb = GetComponent <Rigidbody> ();
		playerRb = GameObject.FindGameObjectWithTag ("Player").GetComponent <Rigidbody> ();
		playerController = playerRb.GetComponent <UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController> ();

		hookRb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

		Invoke("Expire", lifetime);
	}
		
	void OnCollisionEnter (Collision col) 
	{
		hookRb.velocity = Vector3.zero;
		if (playerController.Grounded) {
			InputSimulator.SimulateKeyPress (VirtualKeyCode.SPACE);
		}

		anchored = true;
		playerRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
	}

	void FixedUpdate ()
	{
		if (anchored) {
			PullPlayer ();
		}
	}

	void PullPlayer () 
	{
		Vector3 deltaPos = transform.position - playerRb.position;
		float distance = deltaPos.magnitude;

		if (distance < 2) {
			playerRb.velocity = Vector3.zero;
			anchored = false;
			Expire ();
		} else {
			playerRb.velocity = deltaPos.normalized * strength;
		}
	}

	void Expire()
	{
		if (!anchored) {
			Destroy (gameObject);
		}
	}
}
