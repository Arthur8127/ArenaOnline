using Photon.Pun;

using UnityEngine;

public class WeaponPlayer : MonoBehaviourPun
{
    public Transform cameraHolder;
    public BulletBase bulletPrefab;



    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPrefab, cameraHolder.position, cameraHolder.rotation);
        }
    }



}
