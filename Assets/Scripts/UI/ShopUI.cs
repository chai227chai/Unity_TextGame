using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ShopUI : MonoBehaviour
{
    public ItemData itemData;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemPrice;
    public Image icon;

    public void OnDetailButton()
    {
        SetDetailPopUp();
        UIManager.Instance.ItemDetailPopUp();
    }

    public void SetDetailPopUp()
    {
        UIManager.Instance.curItemData = itemData;
        UIManager.Instance.itemDetailSprite.sprite = itemData.itemSprite;
        UIManager.Instance.itemDetailName.text = itemData.itemName.ToString();
        UIManager.Instance.itemDetailType.text = itemData.GetItemType();
        UIManager.Instance.itemDetailDescription.text = itemData.itemDescription.ToString();
        UIManager.Instance.itemDetailSpec.text = itemData.GetSpecString();
    }

}
