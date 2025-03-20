using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    public static Loader instance;
    [SerializeField] private Image progress;
    [SerializeField] private WindowsBase windows;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    internal void LoadScene(int scene)
    {
        PhotonNetwork.LoadLevel(scene);
        StartCoroutine(AsyncLoad());
    }

    internal void LoadScene(string scene)
    {
        PhotonNetwork.LoadLevel(scene);
        StartCoroutine(AsyncLoad());
    }

    IEnumerator AsyncLoad()
    {
        windows.ShowWindows();
        progress.fillAmount = 0;

        while (PhotonNetwork.LevelLoadingProgress < 1)
        {
            yield return null;
            progress.fillAmount = PhotonNetwork.LevelLoadingProgress;
        }

        windows.HideWindows();
    }
}
