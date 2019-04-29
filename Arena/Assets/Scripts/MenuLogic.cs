using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MenuLogic : NetworkBehaviour
{
	[SerializeField]
	private GameObject _mainMenuPanel;
	[SerializeField]
	private Network _network;
	[SerializeField]
	private Text _textIP;
	[SerializeField]
	private GameObject _connectPanel;

	void Awake(){
		_network = FindObjectOfType<Network>();
	}
		
	public void StartGame(){
		_network.isHost = true;
		_network.StopServer();
		Application.LoadLevel("Game02");
	}

	public void ConnectToServer(){
		_network.manager.networkAddress = _textIP.text;
		_network.isHost = false;
		Application.LoadLevel("Game02");
	}

	public void QuitGame(){
		Application.Quit();
	}
	
	public void OpenConnectPanel(){
		_connectPanel.SetActive(true);
		_mainMenuPanel.SetActive(false);
	}

	public void CloseConnectPanel(){
		_connectPanel.SetActive(false);
		_mainMenuPanel.SetActive(true);
	}
	
}
