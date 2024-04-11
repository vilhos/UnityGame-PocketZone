using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Управление сохранением и загрузкой игры
public class SaveLoadManager : MonoBehaviour
{
    public PlayerController playerController;           // Ссылка на контроллер игрока
    public PlayerHealthAndDamage playerHealth;          // Ссылка на здоровье игрока
    public PlayerShooting playerShooting;               // Ссылка на стрельбу игрока
    public PlayerCollect playerCollect;                 // Ссылка на сбор предметов игрока
    public Button loadDefaultSettingsButton;            // Кнопка для загрузки настроек по умолчанию

    private void Start()
    {
        List<string> inventoryItems = playerCollect.InventoryItems;         // Получаем инвентарь игрока
        Text[] itemTexts = playerCollect.itemTexts;                         // Получаем тексты предметов из PlayerCollect
        playerCollect.ItemTexts = itemTexts;                                // Используем сеттер для обновления текстов предметов
        loadDefaultSettingsButton.onClick.AddListener(LoadDefaultSettings); // Привязываем метод загрузки настроек по умолчанию к кнопке
        LoadGame();                                                         // Загружаем сохраненную игру
    }

    // Загрузка игры из файла
    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/save.json";        // Путь к файлу сохранения
        if (File.Exists(path))                                              // Проверяем, существует ли файл
        {
            string json = File.ReadAllText(path);                           // Читаем данные из файла
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);       // Десериализуем данные из JSON

            // Применяем данные к объектам игрока
            playerController.PlayerPosition = data.PlayerPosition;
            playerHealth.HealthPoints = data.HealthPoints;
            playerShooting.BulletCount = data.BulletCount;
            playerCollect.InventoryItems = data.InventoryItems;

            // Обновляем тексты предметов с помощью сеттера в PlayerCollect
            playerCollect.ItemTexts[0].text = data.Item1Count.ToString();
            playerCollect.ItemTexts[1].text = data.Item2Count.ToString();
            playerCollect.ItemTexts[2].text = data.Item3Count.ToString();

       //     playerCollect.UpdateUI(); // Обновляем интерфейс игрока
        }
    }

    // Сохранение игры в файл
    public void SaveGame()
    {
        PlayerData data = new PlayerData(
            playerController.PlayerPosition,
            playerHealth.HealthPoints,
            playerShooting.BulletCount,
            playerCollect.InventoryItems,
            playerCollect.item1List.Count,          // Количество собранных предметов первого типа
            playerCollect.item2List.Count,          // Количество собранных предметов второго типа
            playerCollect.item3List.Count           // Количество собранных предметов третьего типа
        );

        string json = JsonUtility.ToJson(data);     // Сериализуем данные в JSON
        File.WriteAllText(Application.persistentDataPath + "/save.json", json); // Записываем данные в файл
    }

    // Загрузка настроек по умолчанию
    public void LoadDefaultSettings()
    {
        string saveFilePath = Application.persistentDataPath + "/save.json"; // Путь к файлу сохранения
        File.Delete(saveFilePath);                                           // Удаляем файл сохранения
        SceneManager.LoadScene("SampleScene");                               // Загружаем сцену с настройками по умолчанию
    }
}
