using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public List<ItemData> itemDataList;
    public GameObject scrollContentsParent;
    public GameObject itemPrefabs;

    private void Start()
    {
        foreach (ItemData item in itemDataList)
        {
            SetSaleItem(item);
        }
    }

    private void SetSaleItem(ItemData itemData)
    {
        GameObject itemInfo = Instantiate(itemPrefabs, scrollContentsParent.transform);

        itemInfo.GetComponent<ShopUI>().itemData = itemData;
        itemInfo.GetComponent<ShopUI>().itemName.text = itemData.itemName;
        itemInfo.GetComponent<ShopUI>().itemDescription.text = itemData.itemDescription;
        itemInfo.GetComponent<ShopUI>().itemPrice.text = itemData.price.ToString("N0");
        itemInfo.GetComponent<ShopUI>().icon.sprite = itemData.itemSprite;
    }
}
