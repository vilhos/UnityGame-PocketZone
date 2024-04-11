using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DeleteButton : MonoBehaviour
{
    public GameObject[] deleteButtons; // Массив кнопок "Delete"
    public PlayerCollect playerCollect; // Ссылка на скрипт PlayerCollect
    private GameObject activeDeleteButton; // Переменная для хранения активной кнопки "Delete"
    public bool isDeleteButtonActive; // Переменная для управления активностью кнопки "Delete"

    void Start()
    {
        UpdateDeleteButtonsVisibility();
    }

    void Update()
    {
        UpdateDeleteButtonsVisibility();
    }

    private void UpdateDeleteButtonsVisibility()
    {
        for (int i = 0; i < deleteButtons.Length; i++)
        {
            bool hasItems = CheckForItems(playerCollect.InventoryItems, $"Item{i + 1}");
            bool isButtonActive = isDeleteButtonActive && deleteButtons[i] == activeDeleteButton; // Проверяем, является ли кнопка активной
            deleteButtons[i].SetActive(hasItems && isButtonActive);
        }
    }

    private bool CheckForItems(List<string> items, string itemName)
    {
        foreach (string item in items)
        {
            if (item == itemName)
            {
                return true;
            }
        }
        return false;
    }
    public void OnItemButtonClicked(GameObject clickedButton)
    {
        // Если уже есть активная кнопка "Delete", деактивируем ее
        isDeleteButtonActive = !isDeleteButtonActive;

        // Устанавливаем новую активную кнопку "Delete"
        activeDeleteButton = clickedButton;
        activeDeleteButton.SetActive(true);
    }

    public void OnDeleteButtonClicked(GameObject clickedButton)
    {
        isDeleteButtonActive = !isDeleteButtonActive;

        // Устанавливаем новую активную кнопку "Delete"
        activeDeleteButton = clickedButton;
        activeDeleteButton.SetActive(true);

        // Получаем индекс нажатой кнопки "Delete" в массиве deleteButtons
        int index = 0;
        for (int i = 0; i < deleteButtons.Length; i++)
        {
            if (deleteButtons[i] == clickedButton)
            {
                index = i;
                break;
            }
        }

        // Обнуляем InventoryItems
        if (index == 0)
        {
            playerCollect.item1List.Clear();
        }
        else if (index == 1)
        {
            playerCollect.item2List.Clear();
        }
        else if (index == 2)
        {
            playerCollect.item3List.Clear();
        }
    }
}