using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public GameObject impact, prijective;

    public float speed = 20f; // �������� ����
    public float maxDistance = 100f; // ������������ ��������� ������
    public LayerMask hitMask; // ����� �����, � �������� ���� ����� �����������

    private Vector3 _previousPosition;
    private float _traveledDistance = 0f;

    void Start()
    {
        _previousPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 direction = transform.forward;
        float stepDistance = speed * Time.deltaTime;

        // ��������� ������������ � ������� ����
        if (Physics.Raycast(_previousPosition, direction, out RaycastHit hit, stepDistance, hitMask))
        {
            // ������������ ���������
            OnHit(hit);
            return; // ������������� ���� ��� ���������
        }

        // ���������� ���� ������
        transform.position += direction * stepDistance;
        _traveledDistance += stepDistance;

        // ����������� �� ���������
        if (_traveledDistance >= maxDistance)
        {
            Destroy(gameObject);
        }

        _previousPosition = currentPosition;
    }

    void OnHit(RaycastHit hit)
    {
        Debug.Log($"���� ������ � {hit.collider.name} � ����� {hit.point}");
        Destroy(gameObject); // ������� ���� ����� ���������
    }
}
