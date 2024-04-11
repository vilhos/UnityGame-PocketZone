using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerCollect : MonoBehaviour
{
    public List<GameObject> item1List = new List<GameObject>();      // Список для предметов первого типа
    public List<GameObject> item2List = new List<GameObject>();      // Список для предметов второго типа
    public List<GameObject> item3List = new List<GameObject>();      // Список для предметов третьего типа
    public Text[] itemTexts = new Text[3];                           // Массив для хранения текстовых полей
    public Image[] itemImages = new Image[3];                        // Массив для хранения изображений предметов

    private void Update()
    {
        UpdateUI();
    }
    public List<string> InventoryItems
    {
        get
        {
            List<string> items = new List<string>();
            foreach (GameObject item in item1List)
            {
                items.Add("Item1");
            }
            foreach (GameObject item in item2List)
            {
                items.Add("Item2");
            }
            foreach (GameObject item in item3List)
            {
                items.Add("Item3");
            }
            return items;
        }
        set
        {
            // Очистка списков перед установкой новых данных
            item1List.Clear();
            item2List.Clear();
            item3List.Clear();

            // Добавление предметов в соответствующие списки на основе переданных данных
            foreach (string item in value)
            {
                if (item == "Item1")
                {
                    item1List.Add(null); // Здесь нужно передать GameObject предмета, но в данном контексте можно передать null
                }
                else if (item == "Item2")
                {
                    item2List.Add(null);
                }
                else if (item == "Item3")
                {
                    item3List.Add(null);
                }
            }
        }
    }

    public Text[] ItemTexts
    {
        get { return itemTexts; }
        set { itemTexts = value; }
    }

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
        itemList.Add(itemToCollect);          // Добавляем собранный предмет в соответствующий список
    }

    public void UpdateUI()
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
                    itemImages[i].enabled = item2List.Count > 0; // Отображаем изображение, если есть предметы в списке item2List
                    itemTexts[i].gameObject.SetActive(item2List.Count > 1);
                    break;
                case 2:
                    itemTexts[i].text = item3List.Count.ToString();
                    itemImages[i].enabled = item3List.Count > 0; // Отображаем изображение, если есть предметы в списке item3List
                    itemTexts[i].gameObject.SetActive(item3List.Count > 1);
                    break;
                default:
                    break;
            }
        }
    }
}