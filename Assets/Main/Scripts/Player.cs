using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	public enum Button
	{
		UP = 0, DOWN, LEFT, RIGHT, A, B
	}

	public int playerNumber;
	public Color captureColor;
	public float groundForce = 1.0f;
	public float airForce = 1.0f;
	public float jumpForce = 1.0f;
	public float distToGround = 1.0f;

	Dictionary<Button,string> _buttons;
	Vector3 _pushDirection;
	Rigidbody _rigidbody;
	bool _isGrounded;

	void Start()
	{
		_rigidbody = gameObject.GetComponent<Rigidbody>();
		SetupButtonNames();
	}

	void SetupButtonNames()
	{
		_buttons = new Dictionary<Button, string>();
		Button[] enumValues = (Button[])Enum.GetValues(typeof(Button));
		for (int i = 0; i < enumValues.Length; i++)
		{
			_buttons.Add(enumValues[i], "P"+ playerNumber +"_"+enumValues[i]);
		}
	}

	void Update ()
	{
		_pushDirection = Vector3.zero;

		//Joystick directions
		if( Input.GetButton( _buttons[Button.UP] ) )
		{
			_pushDirection += new Vector3(0, 0, 1);
		}
		if( Input.GetButton( _buttons[Button.DOWN] ) )
		{
			_pushDirection += new Vector3(0, 0, -1);
		}
		if( Input.GetButton( _buttons[Button.LEFT] ) )
		{
			_pushDirection += new Vector3(-1, 0, 0);
		}
		if( Input.GetButton( _buttons[Button.RIGHT] ) )
		{
			_pushDirection += new Vector3(1, 0, 0);
		}

		//Action buttons
		if( Input.GetButtonDown( _buttons[Button.A] ) && _isGrounded )
		{
			//Jump
			_rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
		if( Input.GetButtonDown( _buttons[Button.B] ) )
		{
			Debug.Log(_buttons[Button.B]);
		}
	}

	void FixedUpdate()
	{
		_isGrounded = Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
		_rigidbody.AddForce(_pushDirection.normalized * (_isGrounded ? groundForce : airForce) );
	}
}