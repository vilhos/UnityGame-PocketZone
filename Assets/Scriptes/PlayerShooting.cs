using UnityEngine;
using UnityEngine.UI;


public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;          // Префаб снаряда
    [SerializeField] private Text bulletText;                  // Текст для отображения количества патронов
    [SerializeField] private Transform firePoint;              // Точка выстрела
    [SerializeField] private float bulletForce = 5f;           // Сила выстрела
 //   [SerializeField] private float rotationSpeed = 20f;        // Скорость поворота
    private int bulletCount = 1000;                            // Начальное количество патронов
    private Animator anim;                                     // Аниматор игрока

    private void Start()
    {
        anim = GetComponent<Animator>();   
        UpdateBulletCountUI();                                 // Обновляем отображение количества патронов
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && bulletCount > 0) // Проверяем нажатие клавиши и наличие патронов
        {
            anim.SetInteger("State", 3);                       
            Shoot();
        }
    }

    public void Shoot()
    {
        anim.SetInteger("State", 1);                             // Устанавливаем состояние анимации для ожидания
        Vector3 spawnPosition = firePoint.right;                 // Получаем направление выстрела
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position + spawnPosition / 2, firePoint.rotation); // Создаем снаряд
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse); // Применяем силу выстрела к снаряду

        bulletCount--;                                           
        UpdateBulletCountUI(); 
        Destroy(bullet, 0.4f); 
    }

    void UpdateBulletCountUI()
    {
        bulletText.text = bulletCount.ToString();               // Обновляем текст с количеством патронов
    }

    // Свойство для доступа к количеству патронов
    public int BulletCount
    {
        get { return bulletCount; }
        set { bulletCount = value; }
    }
}
