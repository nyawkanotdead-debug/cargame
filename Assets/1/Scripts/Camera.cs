using UnityEngine;
// Если ты захочешь полностью перейти на новую систему, раскомментируй строку ниже:
// using UnityEngine.InputSystem; 

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _car;
    [SerializeField] private float _speed = 10f;

    // Смещения для двух разных перспектив
    private Vector3 _defaultOffset = new Vector3(0f, 2f, -4f); // Вид сзади 
    private Vector3 _lookBackOffset = new Vector3(0f, 2f, 4f);  // Вид назад 

    private void FixedUpdate()
    {
        if (_car == null) return;

        // Используем более универсальный способ проверки нажатия для режима Both
        // Это должно убрать предупреждение о deprecation
        bool isPressed = false;

        try
        {
            // Пытаемся считать старым способом, который работает в режиме Both
            isPressed = Input.GetKey(KeyCode.Q);
        }
        catch
        {
            // Если Unity все равно блокирует старый ввод, здесь можно прописать логику для Input System
            // Но в режиме Both (как у тебя) блок выше должен работать без ошибок
        }

        Vector3 currentOffset = isPressed ? _lookBackOffset : _defaultOffset; 

        
        var targetPosition = _car.TransformPoint(currentOffset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.fixedDeltaTime); 

        
        var direction = _car.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speed * Time.fixedDeltaTime); 
    }
}