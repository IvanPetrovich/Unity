using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
	
	[SerializeField]
	private Text _scoreText;
	[SerializeField]
	private Text _winLoseText;
	[SerializeField]
	private Text _timeLeft;
	[SerializeField]
	private GameObject _panelPause;
	
	private GameLogic _gameLogic;
	
	void Start()
	{
		_gameLogic = FindObjectOfType<GameLogic>();
	}
	
	public void SetScoreText(String text)
	{
		_scoreText.text = text;
	}

	public void SetTimeLeftText(String text)
	{
		_timeLeft.text = text;
	}


	public void SetWinLoseText(String text)
	{
		_winLoseText.text = text;
	}

    void Update()
    {
        float timeLeft = _gameLogic.GetTimeLeft();
		SetTimeLeftText(timeLeft.ToString("0.00"));
    }
	
	public void StartGame()
	{
		_panelPause.SetActive(false);
	}
	
	public void FinishGame()
	{
		_panelPause.SetActive(true);
	}
	
}
