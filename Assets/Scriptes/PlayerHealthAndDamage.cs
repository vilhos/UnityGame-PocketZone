using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Управление здоровьем игрока и получением урона
public class PlayerHealthAndDamage : MonoBehaviour
{
    [SerializeField] private int health = 100;         // Здоровье игрока
    [SerializeField] private int damage = 5;           // Урон в секунду
    [SerializeField] private Slider lifeBar;           // Шкала здоровья в интерфейсе
    public ScreenNotifier screenNotifier;              // Класс для управления уведомлениями

    private int healthPoints;                          // Текущее количество здоровья
    private bool isTakingDamage = false;               // Флаг для определения, получает ли игрок урон
    private Coroutine damageCoroutine;                 // Ссылка на корутину урона

    private void Start()
    {
        lifeBar.value = health;                        // Установка начального значения шкалы здоровья
        healthPoints = health;                         // Инициализация текущего здоровья
        UpdateUI();                                    // Обновление интерфейса
    }

    // Обработка столкновений с врагами
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }

    // Продолжение получения урона во время столкновения с врагом
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isTakingDamage)
        {
            damageCoroutine = StartCoroutine(ApplyDamageOverTime());
        }
    }

    // Прекращение получения урона при окончании столкновения с врагом
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isTakingDamage = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
            }
        }
    }

    // Применение урона по времени
    private IEnumerator ApplyDamageOverTime()
    {
        isTakingDamage = true;
        while (health > 0)
        {
            TakeDamage();
            yield return new WaitForSeconds(1f);
        }
        isTakingDamage = false;
    }

    // Получение урона
    private void TakeDamage()
    {
        health -= damage;
        lifeBar.value = health;
        if (health <= 0)
        {
            Die();
        }
    }

    // Свойство для доступа к здоровью игрока
    public int HealthPoints
    {
        get { return health; }
        set { health = value; }
    }

    // Обновление интерфейса
    private void UpdateUI()
    {
        lifeBar.value = health;
    }

    // Действия при смерти игрока
    void Die()
    {
        gameObject.SetActive(false); // Деактивация игрового объекта игрока
        screenNotifier.gameOverWindow.SetActive(true); // Вывод окна "Game Over"
    }
}
