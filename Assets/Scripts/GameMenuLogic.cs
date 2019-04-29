using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameMenuLogic : NetworkBehaviour
{
	[SerializeField]
	private GameObject _pausePanel;
	[SerializeField]
	private GameObject _buttonPause;
	[SerializeField]
	private Text _textScore;
	[SerializeField]
	private Text _textPause;
	private bool _isPause = false;
	private float _timeout = 3f;
	private float _time = 0;

	private Network _network;
	
	void Awake()
	{
		_network = FindObjectOfType<Network>();
		if(isClient){
			Button btn = _buttonPause.GetComponent<Button>();
			btn.interactable = false;
		}
	}
	
	public bool IsPause(){
		return _isPause;
	}
	
	[Command]
	public void CmdPauseOn(){
		RpcPauseOn();
	}
	[ClientRpc]
	public void RpcPauseOn(){
		PauseOn();
	}
	
	public void PauseOn(){
		_pausePanel.SetActive(true);
		Time.timeScale = 0;
		_isPause = true;
		SetPauseText();
	}

	void SetPauseText(){
		if (isServer){ 
			_textPause.text = "Pause";
		}else{
			_textPause.text = "Server's pause";
		}			
	}

	[Command]
	public void CmdPauseOff(){
		RpcPauseOff();
	}
	[ClientRpc]
	public void RpcPauseOff(){
		PauseOff();
	}

	public void PauseOff(){
		_pausePanel.SetActive(false);
		_buttonPause.SetActive(true);
		Time.timeScale = 1;
		_isPause = false;
	}
	
	public void SetTextScore(int score){
		_textScore.text = "Score: " + score;
	}
	
	void Update(){
		bool connected = _network.manager.IsClientConnected();
		if(connected){
			_time = 0;
			SetPauseText();
		}else{
			_time+= 0.03f;
			_textPause.text = "Connecting...";
			if (_isPause == false){
				CmdPauseOn();
			}
		}	
			
		if(Input.GetButtonDown("Cancel") && connected){
			SwitchPause();
		}

		if(_time >= _timeout){
			Application.LoadLevel("MainMenu");
		}			
	}
	
	public void SwitchPause(){
		Debug.Log("Switch pause");
			if(_isPause){
				CmdPauseOff();
			}else{
				CmdPauseOn();
			}
	}
	
}
