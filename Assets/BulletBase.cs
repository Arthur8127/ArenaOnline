using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public GameObject impact, prijective;

    public float speed = 20f; // Скорость пули
    public float maxDistance = 100f; // Максимальная дистанция полета
    public LayerMask hitMask; // Маска слоев, с которыми пуля может столкнуться

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

        // Проверяем столкновение с помощью луча
        if (Physics.Raycast(_previousPosition, direction, out RaycastHit hit, stepDistance, hitMask))
        {
            // Обрабатываем попадание
            OnHit(hit);
            return; // Останавливаем пули при попадании
        }

        // Перемещаем пулю вперед
        transform.position += direction * stepDistance;
        _traveledDistance += stepDistance;

        // Ограничение по дальности
        if (_traveledDistance >= maxDistance)
        {
            Destroy(gameObject);
        }

        _previousPosition = currentPosition;
    }

    void OnHit(RaycastHit hit)
    {
        Debug.Log($"Пуля попала в {hit.collider.name} в точке {hit.point}");
        Destroy(gameObject); // Удаляем пулю после попадания
    }
}
