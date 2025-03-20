using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviourPunCallbacks
{

    [Header("Links")]
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private SpawnPoint[] spawnPoints;
    [SerializeField] private GameObject PlayerPrefab;

    public static Main instance;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if(!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("Lobby");
            return;
        }
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
        PhotonNetwork.Instantiate(PlayerPrefab.name, point.position, point.rotation);
    }

    internal void SepupCamera(Transform parrent)
    {
        if(parrent == null)
        {
            parrent = cameraHolder;
        }
        cameraTransform.SetParent(parrent, false);
    }
}
