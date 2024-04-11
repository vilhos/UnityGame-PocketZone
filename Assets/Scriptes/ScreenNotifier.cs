using UnityEngine;

// Уведомление о состоянии экрана
public class ScreenNotifier : MonoBehaviour
{
    public GameObject gameOverWindow;      // Ссылка на окно "Game Over"

    private void Start()
    {
        gameOverWindow.SetActive(false);   // Делаем окно "Game Over" неактивным при старте
    }
}
