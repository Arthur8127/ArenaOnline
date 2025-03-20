using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;

public class Lobby : MonoBehaviourPunCallbacks
{

    [Header("Links")]
    [SerializeField] private WindowsBase main;
    [SerializeField] private UiLogin login;
    [SerializeField] private TextMeshProUGUI txtNick;
    public static Lobby instance;


    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            login.Show();
        }
        else
        {
            ShowMain();
        }
    }

    private void ShowMain()
    {
        string nick = login.GetCurrentNick;
        txtNick.text = nick;
        main.ShowWindows();

    }

    internal void Connect()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.NickName = login.GetCurrentNick;
        ShowMain();
    }


    public void FastMatch()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions op = new RoomOptions();
        op.MaxPlayers = 6;
        string guid = Guid.NewGuid().ToString();
        PhotonNetwork.CreateRoom(guid, op);
    }

    public override void OnJoinedRoom()
    {
        Hashtable prop = new Hashtable();
        prop.Add("kils", 0);
        prop.Add("deads", 0);
        PhotonNetwork.LocalPlayer.SetCustomProperties(prop);
        Loader.instance.LoadScene("Arena");
    }
}
