using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance; // Ссылка на себя для доступа из других скриптов

    public List<GameObject> item1List = new List<GameObject>(); // Список для предметов первого типа
    public List<GameObject> item2List = new List<GameObject>(); // Список для предметов второго типа
    public List<GameObject> item3List = new List<GameObject>(); // Список для предметов третьего типа
    public Text[] itemTexts = new Text[3]; // Массив для хранения текстовых полей
    public Image[] itemImages = new Image[3]; // Массив для хранения изображений предметов

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item1"))
        {
            CollectItem(item1List, other.gameObject);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Item2"))
        {
            CollectItem(item2List, other.gameObject);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Item3"))
        {
            CollectItem(item3List, other.gameObject);
            Destroy(other.gameObject);
        }

        UpdateUI();
    }


    private void CollectItem(List<GameObject> itemList, GameObject itemToCollect)
    {
        itemList.Add(itemToCollect); // Добавляем собранный предмет в соответствующий список
    }

    private void UpdateUI()
    {
        for (int i = 0; i < itemTexts.Length; i++)
        {
            switch (i)
            {
                case 0:
                    itemTexts[i].text = item1List.Count.ToString();
                    itemImages[i].enabled = item1List.Count > 0; // Отображаем изображение, если есть предметы в списке item1List
                    itemTexts[i].gameObject.SetActive(item1List.Count > 1);
                    break;
                case 1:
                    itemTexts[i].text = item2List.Count.ToString();
                    itemImages[i].enabled = item1List.Count > 0; // Отображаем изображение, если есть предметы в списке item2List
                    itemTexts[i].gameObject.SetActive(item2List.Count > 1);
                    break;
                case 2:
                    itemTexts[i].text = item3List.Count.ToString();
                    itemImages[i].enabled = item1List.Count > 0; // Отображаем изображение, если есть предметы в списке item3List
                    itemTexts[i].gameObject.SetActive(item3List.Count > 1);
                    break;
                default:
                    break;
            }
        }
    }
}
