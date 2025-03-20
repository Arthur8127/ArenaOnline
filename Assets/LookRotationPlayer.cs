using Photon.Pun;
using UnityEngine;

public class LookRotationPlayer : MonoBehaviourPun, IPunObservable
{
    [Header("Links")]
    [SerializeField] private Transform cameraHodler;
    [SerializeField] private Transform body;


    [Header("Settings")]
    [SerializeField] private float minClamp;
    [SerializeField] private float maxClamp;
    [SerializeField] private float sensitytivity;

    private float mouseY;
    private float mouseX;

    private void Start()
    {
        if(photonView.IsMine)
        {
            Main.instance.SepupCamera(cameraHodler);
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            mouseY -= Input.GetAxis("Mouse Y") * sensitytivity * Time.deltaTime;
            mouseY = Mathf.Clamp(mouseY, minClamp, maxClamp);

            mouseX = Input.GetAxis("Mouse X") * sensitytivity * Time.deltaTime;

            body.Rotate(Vector3.up * mouseX);

            cameraHodler.localRotation = Quaternion.Euler(Vector3.right * mouseY);
        }
        else
        {
            cameraHodler.localRotation = Quaternion.Lerp(cameraHodler.localRotation, Quaternion.Euler(Vector3.right * mouseY), Time.deltaTime * 10f);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(mouseY);
        }
        else
        {
            mouseY = (float)stream.ReceiveNext();
        }
    }
}
