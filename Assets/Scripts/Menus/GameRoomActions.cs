using UnityEngine;
using System.Collections;

public class GameRoomActions : MonoBehaviour {

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom (local)");
        PhotonNetwork.LoadLevel(Scenes.RecruitmentZone);
    }
}
