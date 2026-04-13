using UnityEngine;
using System.Collections.Generic;

public class CarAI : MonoBehaviour
{
    // Ёто тот самый список, который мы видим на твоем скриншоте
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private float _speed = 12f;
    [SerializeField] private float _rotationSpeed = 5f;

    private int _currentWaypointIndex = 0;

    private void Update()
    {
        // ѕроверка: если список пуст или не назначен, выходим, чтобы не было ошибок
        if (_waypoints == null || _waypoints.Count == 0) return;

        // Ѕерем текущую точку из твоего списка
        Transform target = _waypoints[_currentWaypointIndex];
        if (target == null) return;

        // –ассчитываем позицию (игнорируем высоту Y, чтобы машина не взлетала)
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);

        // 1. ѕлавный поворот к точке
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
        }

        // 2. ƒвижение вперед к точке
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

        // 3. ≈сли доехали до точки (дистанци€ меньше 2 метров), переходим к следующей
        if (Vector3.Distance(transform.position, targetPosition) < 2.0f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Count;
        }
    }
}