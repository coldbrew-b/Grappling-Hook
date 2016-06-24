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
	private CapsuleCollider playerCollider;
	private RigidbodyFirstPersonController playerController;		

	void Start () 
	{
		hookRb = GetComponent <Rigidbody> ();

		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		playerRb = player.GetComponent <Rigidbody> ();
		playerController = player.GetComponent <RigidbodyFirstPersonController> ();
		playerCollider = player.GetComponent <CapsuleCollider>();

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
//			playerCollider.height = 0.5f;
			PullPlayer ();
		} else {
//			playerCollider.height = 1.6f;
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
