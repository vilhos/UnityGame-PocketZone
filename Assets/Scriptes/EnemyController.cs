using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies = new GameObject[3];  // Массив префабов врагов для создания
    [SerializeField] private Transform target;                          // Цель, к которой двигаются враги
    [SerializeField] private float attackDistance = 5f;                 // Расстояние, на котором враги начинают атаковать игрока
    [SerializeField] private float moveSpeed = 0.1f;                    // Скорость перемещения врагов

    // Границы случайного перемещения врагов по X и Y координатам
    [SerializeField] private Vector2 moveBoundsMin = new Vector2(-5f, 0f);
    [SerializeField] private Vector2 moveBoundsMax = new Vector2(5f, 2f);

    // Массивы для хранения аниматоров, целевых точек перемещения, временных меток для перемещения и компонентов SpriteRenderer врагов
    private Animator[] animators;
    private Vector2[] moveTargets;
    private float[] moveTimes;
    private SpriteRenderer[] enemyRenderers;

    void Start()
    {
        InitializeArrays();
        InstantiateEnemies();
    }

    void Update()
    {
        Vector2 playerPosition = target.position;        // Позиция игрока

        for (int i = 0; i < enemies.Length; i++)         // Обновление перемещения каждого врага
        {
            if (enemies[i] == null) continue;            // Пропуск врага, если он удален          
            if (Time.time > moveTimes[i])                // Обновление целевой точки перемещения в зависимости от времени
            {
                UpdateMoveTarget(i);
                moveTimes[i] = Time.time + Random.Range(1f, 3f);
            }

            float distanceToPlayer = Vector2.Distance(enemies[i].transform.position, playerPosition);          // Расчет расстояния до игрока

            // Перемещение к игроку или к целевой точке в зависимости от расстояния
            if (distanceToPlayer < attackDistance)
            {
                MoveToPlayer(enemies[i], animators[i], playerPosition);
            }
            else
            {
                MoveToTarget(enemies[i], moveTargets[i], animators[i], playerPosition);
            }
        }
    }

    // Инициализация массивов
    void InitializeArrays()
    {
        moveTargets = new Vector2[enemies.Length];
        moveTimes = new float[enemies.Length];
        enemyRenderers = new SpriteRenderer[enemies.Length];
        animators = new Animator[enemies.Length];
    }

    // Создание врагов
    void InstantiateEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                moveTargets[i] = GetRandomPosition(); // Случайная целевая точка перемещения
                moveTimes[i] = Time.time + Random.Range(1f, 3f); // Случайное время до следующего перемещения
                enemies[i] = Instantiate(enemies[i], moveTargets[i], Quaternion.identity); // Создание врага
                animators[i] = enemies[i].GetComponent<Animator>(); // Получение компонента аниматора врага
                enemyRenderers[i] = enemies[i].GetComponent<SpriteRenderer>(); // Получение компонента SpriteRenderer врага
            }
        }
    }

    // Перемещение к целевой точке
    void MoveToTarget(GameObject enemy, Vector2 targetPosition, Animator animator, Vector2 playerPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)enemy.transform.position).normalized;
        Vector2 newPosition = Vector2.MoveTowards(enemy.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        enemy.transform.position = newPosition;

        if (Vector2.Distance(enemy.transform.position, playerPosition) <= attackDistance)
        {
            animator.SetBool("IsAttacking", true); // Установка анимации атаки
        }
        else
        {
            animator.SetBool("IsAttacking", false); // Отключение анимации атаки
        }
        RotateTowardsTarget(enemy, direction); // Поворот в сторону цели
    }

    // Перемещение к игроку
    void MoveToPlayer(GameObject enemy, Animator animator, Vector2 playerPosition)
    {
        Vector2 direction = (playerPosition - (Vector2)enemy.transform.position).normalized;
        Vector2 newPosition = Vector2.MoveTowards(enemy.transform.position, playerPosition, moveSpeed * Time.deltaTime);
        enemy.transform.position = newPosition;

        if (Vector2.Distance(enemy.transform.position, playerPosition) > attackDistance)
        {
            animator.SetBool("IsAttacking", false);
        }
        RotateTowardsTarget(enemy, direction);
    }

    // Поворот в сторону цели
    void RotateTowardsTarget(GameObject enemy, Vector2 direction)
    {
        int index = System.Array.IndexOf(enemyRenderers, enemy.GetComponent<SpriteRenderer>());
        if (index == -1 || index >= enemyRenderers.Length || enemyRenderers[index] == null) return;

        if (direction.x > 0 && enemyRenderers[index].flipX)
        {
            enemyRenderers[index].flipX = false; // Поворот вправо
        }
        else if (direction.x < 0 && !enemyRenderers[index].flipX)
        {
            enemyRenderers[index].flipX = true; // Поворот влево
        }
    }

    // Получение случайной позиции в заданных границах
    Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(moveBoundsMin.x, moveBoundsMax.x);
        float randomY = Random.Range(moveBoundsMin.y, moveBoundsMax.y);
        return new Vector2(randomX, randomY);
    }

    // Обновление целевой точки перемещения
    void UpdateMoveTarget(int index)
    {
        moveTargets[index] = GetRandomPosition();
    }
}
