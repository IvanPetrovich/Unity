using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
	private int _score;
	private bool _isPlaying;
	private CoinCollectionManager _coinColManager;
	private PlayerController _player;
	private UIController _uiController;
	private float _timeLeft;
	[SerializeField]
	private float _maxSeconds = 60;
	
	void Start()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;//Portrait;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.AutoRotation;

		_player = FindObjectOfType<PlayerController>();
		_isPlaying = false;
		_coinColManager = FindObjectOfType<CoinCollectionManager>();
		_uiController = FindObjectOfType<UIController>();
		
	}

	void Update()
	{
		if(_isPlaying)
		{
			_timeLeft -= Time.deltaTime;
			
			if(_timeLeft <= 0)
			{
				_timeLeft = 0;
				_isPlaying = false;
				_player.CanMove(false);
				_uiController.SetWinLoseText("Time is over!");
				_uiController.FinishGame();
			}
		}	
	}
	
	public int GetScore()
	{
		return _score;
	}
	
	public void AddScore(int val)
	{
		_score += val;
		_uiController.SetScoreText("Score: " + _score);
	}
	
	public void ClearScore()
	{
		_score = 0;	
		AddScore(0);
	}
	
	public bool IsPlaying()
	{
		return _isPlaying;
	}
	
	public void StartGame()
	{
		ClearScore();
		_coinColManager.DestroyCoins();
		_coinColManager.CreateCoins();
		_player.SetDefaultPosition();
		_isPlaying = true;
		_uiController.StartGame();
		_timeLeft = _maxSeconds;
		_player.CanMove(true);
	}
	
	public void WinGame()
	{
		_isPlaying = false;
		_uiController.SetWinLoseText("You are winner! Your score: " + _score);
		_player.CanMove(false);
		_uiController.FinishGame();
	}
	
	
	public float GetTimeLeft()
	{
		return _timeLeft;
	}

}
