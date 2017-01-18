using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public Text txtP1;
	public Text txtP2;
	public Text txtTimer;
	public Text txtWin;
	public int gameDuration = 99;

	int _scoreP1;
	int _scoreP2;
	int _timer;
	int _numCapturePoints;

	void Awake()
	{
		_scoreP1 = 0;
		_scoreP2 = 0;

		CapturePoint.scoreAddEvent += OnScoreAddEvent;
		CapturePoint.scoreRemoveEvent += OnScoreRemoveEvent;

		_numCapturePoints = GameObject.FindObjectsOfType<CapturePoint>().Length;
		_timer = gameDuration;

		UpdateScoreVisuals();
		txtTimer.text = _timer.ToString("00");

		StartCoroutine("CountdownCR");
	}

	IEnumerator CountdownCR()
	{
		while (true)
		{
			yield return new WaitForSeconds(1.0f);
			--_timer;
			txtTimer.text = _timer.ToString("00");
			if(_timer == 0)
			{
				GameOver();
			}
		}
	}

	void GameOver()
	{
		StopCoroutine("CountdownCR");
		if(_scoreP1 > _scoreP2) txtWin.text = "RED WINS!";
		else if(_scoreP1 < _scoreP2) txtWin.text = "BLUE WINS!";
		else txtWin.text = "DRAW!";

		Time.timeScale = 0.0f;

		StartCoroutine("GameOverCR");
	}

	void OnDisable()
	{
		Time.timeScale = 1.0f;
		CapturePoint.scoreAddEvent -= OnScoreAddEvent;
		CapturePoint.scoreRemoveEvent -= OnScoreRemoveEvent;
		StopCoroutine("CountdownCR");
		StopCoroutine("GameOverCR");
	}

	IEnumerator GameOverCR()
	{
		yield return new WaitForSecondsRealtime(5.0f);
		SceneManager.LoadScene("MainMenu");
	}

	void OnScoreAddEvent (int playerId)
	{
		if(playerId == 1) { ++_scoreP1; }
		else if(playerId == 2) { ++_scoreP2; }
		UpdateScoreVisuals();

		if(_scoreP1 == _numCapturePoints || _scoreP2 == _numCapturePoints)
		{
			GameOver();
		}
	}

	void OnScoreRemoveEvent (int playerId)
	{
		if(playerId == 1) { --_scoreP1; }
		else if(playerId == 2) { --_scoreP2; }
		UpdateScoreVisuals();
	}

	void UpdateScoreVisuals()
	{
		txtP1.text = _scoreP1.ToString();
		txtP2.text = _scoreP2.ToString();
	}
}
