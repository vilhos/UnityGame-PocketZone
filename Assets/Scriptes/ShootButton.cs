using UnityEngine;
using UnityEngine.UI;

// Управление кнопкой стрельбы
public class ShootButton : MonoBehaviour
{
    [SerializeField] private GameObject player;                 // Ссылка на игрока
    [SerializeField] private PlayerShooting playerShooting;     // Ссылка на компонент стрельбы игрока
    private Button shootButton;                                 // Кнопка стрельбы

    private void Start()
    {
        playerShooting = player.GetComponent<PlayerShooting>(); // Получаем компонент стрельбы игрока
        shootButton = GetComponent<Button>();                   // Получаем компонент кнопки
        shootButton.onClick.AddListener(Fire);                  // Привязываем метод Fire к событию клика кнопки
    }

    // Метод для выполнения стрельбы при нажатии кнопки
    void Fire()
    {
        playerShooting.Shoot(); // Вызываем метод стрельбы у игрока
    }

}
