using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class RecruitmentZone : MonoBehaviour
{

    public Text StatusText;
    public Text PlayerNameText;
    public Text GameNameText;
	
	public void Awake()
	{

		// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
		PhotonNetwork.automaticallySyncScene = true;
		
		// the following line checks if this client was just created (and not yet online). if so, we connect
		if (PhotonNetwork.connectionStateDetailed == PeerState.PeerCreated)
		{
			// Connect to the photon master-server. We use the settings saved in PhotonServerSettings (a .asset file in this project)
			PhotonNetwork.ConnectUsingSettings("0.9");
		}
		
		// if you wanted more debug out, turn this on:
		// PhotonNetwork.logLevel = NetworkLogLevel.Full;
        
        if (String.IsNullOrEmpty(PhotonNetwork.playerName))
        {
            PhotonNetwork.playerName = "Guest" + UnityEngine.Random.Range(1, 9999);
            PlayerNameText.text = PhotonNetwork.playerName;
        }
    }

    public void SubmitName()
    {
        PhotonNetwork.playerName = PlayerNameText.text;
        Debug.Log("Player Name changed: " + PlayerNameText.text);
    }

    public void RefreshGames()
    {
        if (PhotonNetwork.GetRoomList().Length == 0)
        {
            //StatusText.text = "Currently no games are available. Create one.";
        }
        else
        {
            StatusText.text = PhotonNetwork.GetRoomList() + " currently available. Join either:";

            // Room listing: simply call GetRoomList: no need to fetch/poll whatever!
            foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
            {
                GUILayout.BeginHorizontal();
                //StatusText.text = roomInfo.name + " " + roomInfo.playerCount + "/" + roomInfo.maxPlayers;
                if (GUILayout.Button("Join"))
                {
                    PhotonNetwork.JoinRoom(roomInfo.name);
                }

                GUILayout.EndHorizontal();
            }


        }
    }
	
	public void OnGUI()
	{
        if (!PhotonNetwork.connected)
        {
            StatusText.color = Color.yellow;
            if (PhotonNetwork.connecting)
            {
                StatusText.text = "Connecting to: " + PhotonNetwork.ServerAddress;
            }
            else
            {
                StatusText.text = "Not connected. Check console output. Detailed connection state: " + PhotonNetwork.connectionStateDetailed + " Server: " + PhotonNetwork.ServerAddress;
            }
        }
        else
        {
            StatusText.color = Color.green;
            StatusText.text = PhotonNetwork.countOfPlayers + " users are online in " + PhotonNetwork.countOfRooms + " games.";
        }
		
	}

    public void CreateGame() 
    {
			PhotonNetwork.CreateRoom(this.GameNameText.text, new RoomOptions() { maxPlayers = 5 }, null);
    }

    public void ToMainMenu()
    {
        PhotonNetwork.LoadLevel(Scenes.MainMenu);

    }

    public void JoinRoom() 
		{
			PhotonNetwork.JoinRandomRoom();
			//PhotonNetwork.JoinRoom(this.GameName);
		}
		
	
		
		
	
	// We have two options here: we either joined(by title, list or random) or created a room.
	public void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
	}
	
	
	public void OnPhotonCreateRoomFailed()
	{
		this.StatusText.text = "Error: Can't create room (room name maybe already used).";
		Debug.Log("OnPhotonCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
	}
	
	public void OnPhotonJoinRoomFailed()
	{
		this.StatusText.text = "Error: Can't join room (full or unknown room name).";
		Debug.Log("OnPhotonJoinRoomFailed got called. This can happen if the room is not existing or full or closed.");
	}
	public void OnPhotonRandomJoinFailed()
	{
		this.StatusText.text = "Error: Can't join random room (none found).";
		Debug.Log("OnPhotonRandomJoinFailed got called. Happens if no room is available (or all full or invisible or closed). JoinrRandom filter-options can limit available rooms.");
	}
	
	public void OnCreatedRoom()
	{
		Debug.Log("OnCreatedRoom");
		PhotonNetwork.LoadLevel(Scenes.GameRoom);
	}
	
	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("Disconnected from Photon.");
	}
	
	public void OnFailedToConnectToPhoton(object parameters)
	{
		Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + parameters + " ServerAddress: " + PhotonNetwork.networkingPeer.ServerAddress);
	}
}
