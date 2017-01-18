using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable : MonoBehaviour
{
	public Transform respawnPosition;

	Rigidbody _rigidbody;

	void Awake()
	{
		_rigidbody = gameObject.GetComponent<Rigidbody>();
	}

	void Start()
	{
		Respawn();
	}

	void Respawn()
	{
		_rigidbody.velocity = Vector3.zero;
		_rigidbody.angularVelocity = Vector3.zero;
		transform.position = respawnPosition.position;
		transform.rotation = Quaternion.identity;
	}

	void OnTriggerEnter(Collider trigger)
	{
		if(trigger.CompareTag("Kill"))
		{
			Respawn();
		}
	}
}