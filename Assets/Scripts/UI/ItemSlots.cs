using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlots : MonoBehaviour
{
    public Image Icon;
    public GameObject equipped;
    public Item item;

    public int index;

    public void OnClickItem()
    {
        SetPopUp();
        UIManager.Instance.EquipItemPopUp();
    }

    public void Clear()
    {
        item = null;
        equipped.SetActive(false);
        Icon.gameObject.SetActive(false);
    }

    public void Set(Item item)
    {
        this.item = item;
        Icon.gameObject.SetActive(true);
        Icon.sprite = item.itemData.itemSprite;
    }

    public void SetEquip()
    {
        if (item.isEquiped)
        {
            equipped.SetActive(true);
        }
        else
        {
            equipped.SetActive(false);
        }
    }

    public void SetPopUp()
    {
        UIManager.Instance.curItem = GetComponent<ItemSlots>();
        UIManager.Instance.itemSprite.sprite = Icon.sprite;
        UIManager.Instance.itemName.text = item.itemData.itemName.ToString();
        UIManager.Instance.itemType.text = item.itemData.GetItemType();
        UIManager.Instance.itemDescription.text = item.itemData.itemDescription.ToString();
        UIManager.Instance.itemSpec.text = item.itemData.GetSpecString();

        if (item.isEquiped)
        {
            UIManager.Instance.equipButton.SetActive(false);
            UIManager.Instance.unEquipButton.SetActive(true);
        }
        else
        {
            UIManager.Instance.equipButton.SetActive(true);
            UIManager.Instance.unEquipButton.SetActive(false);
        }
    }
}
