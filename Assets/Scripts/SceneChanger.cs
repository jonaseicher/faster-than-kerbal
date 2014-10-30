using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour {

    public void ToRecruitment()
    {
        PhotonNetwork.LoadLevel(Scenes.RecruitmentZone);
        //Application.LoadLevel(Scenes.RecruitmentZone);
    }

    public void ToMainMenu()
    {
        PhotonNetwork.LoadLevel(Scenes.MainMenu);
    }

    public void GameRoom()
    {
        PhotonNetwork.LoadLevel(Scenes.GameRoom);
    }

}
