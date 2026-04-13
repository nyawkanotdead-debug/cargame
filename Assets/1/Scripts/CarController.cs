using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [Header("Wheel Transforms")]
    [SerializeField] private Transform _transformFL;
    [SerializeField] private Transform _transformFR;
    [SerializeField] private Transform _transformBL;
    [SerializeField] private Transform _transformBR;

    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider _colliderFL;
    [SerializeField] private WheelCollider _colliderFR;
    [SerializeField] private WheelCollider _colliderBL;
    [SerializeField] private WheelCollider _colliderBR;

    [Header("Car Settings")]
    [SerializeField] private float _force = 1500f; // Обычная сила
    [SerializeField] private float _turboMultiplier = 2.5f; // Во сколько раз мощнее турбо
    [SerializeField] private float _maxAngle = 30f;
    [SerializeField] private float _brakeForce = 3000f;

    private bool _isTurboActive = false;
    private float _currentMotorPower = 0f;

    void Update()
    {
        // Проверка активации Турбо на клавишу T
        if (Keyboard.current.tKey.wasPressedThisFrame && !_isTurboActive)
        {
            StartCoroutine(ActivateTurbo());
        }
    }

    private void FixedUpdate()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        // Читаем ввод
        float vertical = 0f;
        if (kb.wKey.isPressed) vertical = 1f;
        else if (kb.sKey.isPressed) vertical = -1f;

        float horizontal = 0f;
        if (kb.dKey.isPressed) horizontal = 1f;
        else if (kb.aKey.isPressed) horizontal = -1f;

        // Рассчитываем итоговую силу мотора с учетом ТУРБО
        float turboBoost = _isTurboActive ? _turboMultiplier : 1f;
        _currentMotorPower = vertical * _force * turboBoost;

        // Прикладываем силу к ведущим колесам (задний привод)
        _colliderBL.motorTorque = _currentMotorPower;
        _colliderBR.motorTorque = _currentMotorPower;

        // Логика торможения
        if (kb.spaceKey.isPressed)
        {
            ApplyBrakes(_brakeForce);
        }
        else
        {
            ApplyBrakes(0f);
        }

        // Логика поворота (передние колеса)
        float steer = _maxAngle * horizontal;
        _colliderFL.steerAngle = steer;
        _colliderFR.steerAngle = steer;

        // Обновление визуальной части колес
        UpdateWheelVisuals(_colliderFL, _transformFL);
        UpdateWheelVisuals(_colliderFR, _transformFR);
        UpdateWheelVisuals(_colliderBL, _transformBL);
        UpdateWheelVisuals(_colliderBR, _transformBR);
    }

    IEnumerator ActivateTurbo()
    {
        _isTurboActive = true;
        Debug.Log("ТУРБО АКТИВИРОВАНО!");

        yield return new WaitForSeconds(10f);

        _isTurboActive = false;
        Debug.Log("Турбо закончилось.");
    }

    private void ApplyBrakes(float force)
    {
        _colliderFL.brakeTorque = force;
        _colliderFR.brakeTorque = force;
        _colliderBL.brakeTorque = force;
        _colliderBR.brakeTorque = force;
    }

    private void UpdateWheelVisuals(WheelCollider collider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
}