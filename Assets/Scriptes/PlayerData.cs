using System.Collections.Generic;
using UnityEngine;

public class PlayerData        
{
    public Vector3 PlayerPosition;            // Позиция игрока
    public int HealthPoints;                  // Количество очков здоровья игрока
    public int BulletCount;                   // Количество патронов у игрока
    public List<string> InventoryItems;       // Инвентарь игрока (список предметов)
    public int Item1Count;                    // Количество предмета 1 в инвентаре
    public int Item2Count;                    // Количество предмета 2 в инвентаре
    public int Item3Count;                    // Количество предмета 3 в инвентаре


    // Конструктор класса для инициализации данных игрока
    public PlayerData(Vector3 playerPosition, int healthPoints, int bulletCount, List<string> inventoryItems,
                      int item1Count, int item2Count, int item3Count)
    {
        PlayerPosition = playerPosition;
        HealthPoints = healthPoints;
        BulletCount = bulletCount;
        InventoryItems = inventoryItems;
        Item1Count = item1Count;
        Item2Count = item2Count;
        Item3Count = item3Count;
    }
}
