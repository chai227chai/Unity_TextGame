using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item
{
    public ItemData itemData;
    public bool isEquiped = false;

    public void SetEquip()
    {
        if (isEquiped)
        {
            isEquiped = false;
        }
        else
        {
            isEquiped = true;
        }
    }
}

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public int maxIndex;
    public int nowIndex;

    public TextMeshProUGUI maxTXT;
    public TextMeshProUGUI nowTXT;

    public ItemSlots[] itemSlotList;
    public Item[] itemList;
    public ItemData[] TestitemDataList;

    private PlayerInfo playerStat;

    private Dictionary<ItemType, Item> equipedItem = new Dictionary<ItemType, Item>();

    private void Awake()
    {
        instance = this;
        playerStat = GetComponent<PlayerInfo>();

        UIManager.Instance.onEquipButton += SetEquipedITem;
        UIManager.Instance.onPurchaseButton += AddItem;
    }

    // Start is called before the first frame update
    private void Start()
    {
        maxIndex = itemSlotList.Length;
        itemList = new Item[maxIndex];

        for(int i = 0; i < maxIndex; i++)
        {
            itemList[i] = new Item();
            itemSlotList[i].Clear();
        }

        maxTXT.text = maxIndex.ToString();

        TestItemSet();
    }

    private void SetIndexTXT()
    {
        int cnt = 0;
        foreach (var item in itemList)
        {
            if(item.itemData != null)
            {
                cnt++;
            }
        }
        nowIndex = cnt;
        nowTXT.text = nowIndex.ToString();
    }

    private void UpdateItemListUI()
    {
        for(int i = 0; i < itemSlotList.Length; i++)
        {
            if (itemList[i].itemData != null)
            {
                itemSlotList[i].Set(itemList[i]);
            }
            else
            {
                itemSlotList[i].Clear();
            }
        }

        SetIndexTXT();
    }

    private void UpdateEquipUI()
    {
        for(int i = 0; i < itemSlotList.Length; i++)
        {
            if (itemSlotList[i].item != null)
            {
                itemSlotList[i].SetEquip();
            }
        }
    }

    private void TestItemSet()
    {
        for (int i = 0; i < itemSlotList.Length; i++)
        {
            if (TestitemDataList[i] != null)
            {
                itemList[i].itemData = TestitemDataList[i];
            }
        }

        UpdateItemListUI();
    }

    public void AddItem(ItemData item)
    {
        Item emptyslot = GetEmptySlot();

        if (emptyslot != null)
        {
            emptyslot.itemData = item;
            UpdateItemListUI();
            UIManager.Instance.PopPurchasePopUp();
            GetComponent<PlayerInfo>().UseGold(item.price);
        }
        else
        {
            UIManager.Instance.PopInventoryFullPopUp();
        }

        SetIndexTXT();
    }

    private Item GetEmptySlot()
    {
        for(int i = 0; i < itemList.Length; i++)
        {
            if(itemList[i].itemData == null)
            {
                return itemList[i];
            }
        }

        return null;
    }

    public void SetEquipedITem(Item item)
    {
        if (equipedItem.ContainsKey(item.itemData.itemType))
        {
            if (equipedItem[item.itemData.itemType] != item)
            {
                equipedItem[item.itemData.itemType].SetEquip();
                equipedItem.Remove(item.itemData.itemType);

                equipedItem.Add(item.itemData.itemType, item);
                item.SetEquip();
            }
            else if (equipedItem[item.itemData.itemType] == item)
            {
                item.SetEquip();
                equipedItem.Remove(item.itemData.itemType);
            }
        }
        else
        {
            equipedItem.Add(item.itemData.itemType, item);
            item.SetEquip();
        }

        EquipedItemSet();
        UpdateEquipUI();
    }

    private void EquipedItemSet()
    {
        playerStat.equipedItem.Clear();

        foreach(Item item in Array.FindAll(itemList, item => item.isEquiped)){
            playerStat.equipedItem.Add(item);
        }

        playerStat.UpdatePlayerStat();
    }
}
