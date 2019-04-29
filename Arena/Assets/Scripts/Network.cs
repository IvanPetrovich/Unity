using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Network : NetworkBehaviour
{
	public NetworkManager manager;
	public bool isHost;
	public string ip;
	public int port;
	
	void Awake(){
		manager = GetComponent<NetworkManager>();
	}
    public void StartServer(){
        manager.StartHost();
    }

    public void JoinServer(){
        manager.StartClient();
    }
	
    public void StopServer(){
        manager.StopHost();
    }
	
    public void StopClient(){
        manager.StopClient();
    }

}
