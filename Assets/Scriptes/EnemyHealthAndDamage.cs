using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthAndDamage : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 10;
    public GameObject item;
    public Slider healthSlider;

    private void Start()
    {
            healthSlider.maxValue = health;        // Установка максимального значения для Slider
            healthSlider.value = health;           // Установка начального значения для Slider
            item.SetActive(false);                 // Скрыть предмет при старте
    }

    private void Update()
    {
        //  Позиция шкалы жизни врага

            Vector3 sliderPosition = transform.position + Vector3.up * 1.5f;     
            healthSlider.transform.position = Camera.main.WorldToScreenPoint(sliderPosition);
    }

    public void TakeDamage()
    {
        health -= damage;
        healthSlider.value = health; // Обновление значения Slider

        if (health <= 0)
        {
            Destroy(gameObject);
            SpawnItem();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }

    private void SpawnItem()
    {
            item.SetActive(true);      // Показать предмет после смерти врага
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }
