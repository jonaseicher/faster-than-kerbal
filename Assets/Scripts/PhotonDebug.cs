using UnityEngine;
using System.Collections;

public class PhotonDebug : MonoBehaviour {

	public void Awake()
	{
		if (!PhotonNetwork.connected)
		{
			Application.LoadLevel(Scenes.RecruitmentZone);
			return;
		}
	}

	public void OnLeftRoom()
	{
		Debug.Log("OnLeftRoom (local)");
		Application.LoadLevel(Scenes.RecruitmentZone);
	}
	
	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("OnDisconnectedFromPhoton");
		Application.LoadLevel(Scenes.RecruitmentZone);
	}
	
	public void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		Debug.Log("OnPhotonInstantiate " + info.sender);    // you could use this info to store this or react
	}
	
	public void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Debug.Log("OnPhotonPlayerConnected: " + player);
	}
	
	public void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log("OnPlayerDisconneced: " + player);
	}
	
	public void OnFailedToConnectToPhoton()
	{
		Debug.Log("OnFailedToConnectToPhoton");
		Application.LoadLevel(Scenes.RecruitmentZone);
	}
}
