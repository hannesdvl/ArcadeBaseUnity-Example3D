using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CapturePoint : MonoBehaviour
{
	public static event Action<int> scoreAddEvent;
	public static event Action<int> scoreRemoveEvent;

	public Renderer pointRenderer;

	int _playerId;

	void Start()
	{
		_playerId = 0;
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("Player"))
		{
			Player player = col.gameObject.GetComponent<Player>();
			if(_playerId != player.playerNumber)
			{
				if(_playerId > 0)
				{
					scoreRemoveEvent(_playerId);
				}
				_playerId = player.playerNumber;
				scoreAddEvent(_playerId);

				pointRenderer.material.color = player.captureColor;
			}
		}
	}
}
