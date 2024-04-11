using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed = 0.5f;
    [SerializeField] private Vector2 horizontalLimits = new Vector2(-5f, 5f); // Горизонтальные лимиты
    [SerializeField] private Vector2 verticalLimits = new Vector2(-3f, 3f); // Вертикальные лимиты

    private void Update()
    {
        Vector3 cameraPosition = target.position;
        cameraPosition.z = transform.position.z; 
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, horizontalLimits.x, horizontalLimits.y); // Применяем горизонтальные лимиты
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, verticalLimits.x, verticalLimits.y); // Применяем вертикальные лимиты
        transform.position = Vector3.Lerp(transform.position, cameraPosition, followSpeed * Time.deltaTime);
    }
}
