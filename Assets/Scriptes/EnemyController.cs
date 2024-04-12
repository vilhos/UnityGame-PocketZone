using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies = new GameObject[3];        // Массив для хранения объектов врагов
    [SerializeField] private Transform target;                                // Целевой Transform для следования врагов
    [SerializeField] private float attackDistance = 5f;                       // Дистанция, на которой враги атакуют
    [SerializeField] private float moveSpeed = 0.1f;                          // Скорость движения врагов

    // Минимальные и максимальные границы случайного перемещения врагов по координатам X и Y
    [SerializeField] private Vector2 moveBoundsMin = new Vector2(-5f, 0f);    
    [SerializeField] private Vector2 moveBoundsMax = new Vector2(5f, 2f);   

    // Массивы для хранения аниматоров, целевых точек перемещения, временных меток для перемещения и компонентов SpriteRenderer врагов
    private Animator[] animators;                              
    private Vector2[] moveTargets;          
    private float[] moveTimes;              
    private SpriteRenderer[] enemyRenderers;    

    void Start()
    {
        InitializeArrays();                                                   // Инициализация массивов
        InstantiateEnemies();                                                 // Создание экземпляров врагов
    }

    void Update()
    {
        Vector2 playerPosition = target.position;

        for (int i = 0; i < enemies.Length; i++)                               // Обновление перемещения каждого врага
        {
            if (enemies[i] == null) continue;                                  // Пропуск врага, если он удален
            if (Time.time > moveTimes[i])                                      // Обновление целевой точки перемещения в зависимости от времени
            {
                moveTargets[i] = GetRandomPosition();
                moveTimes[i] = Time.time + Random.Range(1f, 3f);
            }

            MoveEnemy(enemies[i], animators[i], playerPosition);                // Вызов метода для перемещения врага
        }
    }

    void InitializeArrays()
    {
        animators = new Animator[enemies.Length];
        moveTargets = new Vector2[enemies.Length];
        moveTimes = new float[enemies.Length];
        enemyRenderers = new SpriteRenderer[enemies.Length];
    }

    // Создание экземпляров врагов
    void InstantiateEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null) continue;

            GameObject instantiatedEnemy = Instantiate(enemies[i], GetRandomPosition(), Quaternion.identity);
            animators[i] = instantiatedEnemy.GetComponent<Animator>();
            enemyRenderers[i] = instantiatedEnemy.GetComponent<SpriteRenderer>();
            moveTimes[i] = Time.time + Random.Range(1f, 3f);
            enemies[i] = instantiatedEnemy;
        }
    }

    // Метод для перемещения врага
    void MoveEnemy(GameObject enemy, Animator animator, Vector2 playerPosition)
    {
        Vector2 direction = playerPosition - (Vector2)enemy.transform.position;   // Вычисление направления движения к игроку
        float distanceToPlayer = direction.magnitude;                             // Вычисление расстояния до игрока
        direction.Normalize();                                                    

        // Определение целевой позиции для перемещения в зависимости от расстояния до игрока
        Vector2 targetPosition = distanceToPlayer < attackDistance ? playerPosition : moveTargets[System.Array.IndexOf(enemies, enemy)];
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, targetPosition, moveSpeed * Time.deltaTime); // Перемещение к целевой позиции

        animator.SetBool("IsAttacking", distanceToPlayer < attackDistance);       // Установка параметра анимации для атаки, если игрок близко
        RotateTowardsTarget(enemy, direction);                                    // Поворот в сторону цели
    }

    // Метод для поворота в сторону цели
    void RotateTowardsTarget(GameObject enemy, Vector2 direction)
    {
        SpriteRenderer renderer = enemy.GetComponent<SpriteRenderer>();
        renderer.flipX = direction.x < 0;
    }

    // Получение случайной позиции
    Vector2 GetRandomPosition()
    {
        return new Vector2(Random.Range(moveBoundsMin.x, moveBoundsMax.x), Random.Range(moveBoundsMin.y, moveBoundsMax.y));
    }
}
