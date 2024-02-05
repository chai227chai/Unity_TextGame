using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject player;

    [SerializeField] private GameObject sideButtons;
    [SerializeField] private GameObject statusWindow;
    [SerializeField] private GameObject Inventory;

    [Header("Inventory")]
    public Image itemSprite;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemType;
    public TextMeshProUGUI itemSpec;
    public TextMeshProUGUI itemDescription;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public ItemSlots curItem;

    [Header("ShopItemDetail")]
    public Image itemDetailSprite;
    public TextMeshProUGUI itemDetailName;
    public TextMeshProUGUI itemDetailType;
    public TextMeshProUGUI itemDetailSpec;
    public TextMeshProUGUI itemDetailDescription;
    public ItemData curItemData;

    [Header("PopUp")]
    public GameObject popUpBG;
    public GameObject itemEquipPopUp;
    public GameObject shop;
    public GameObject itemDetailPopUp;
    public GameObject purchaseItemPopUp;
    public GameObject inventoryFullPopUp;
    public GameObject noGoldPopUp;

    public event Action<Item> onEquipButton;
    public event Action<ItemData> onPurchaseButton;

    public void Awake()
    {
        Instance = this;
    }


    public void OnStatusButton()
    {
        statusWindow.SetActive(true);
        sideButtons.SetActive(false);
    }

    public void OnInventoryButton()
    {
        Inventory.SetActive(true);
        sideButtons.SetActive(false);
    }

    public void OnShopButton()
    {
        shop.SetActive(true);
    }

    public void OnGoBackButton(GameObject nowobj)
    {
        nowobj.SetActive(false);
        sideButtons.SetActive(true);
    }

    public void OnCloseButton()
    {
        popUpBG.SetActive(false);
    }

    public void OnCancelButton(GameObject obj)
    {
        //StartCoroutine(CancelPopUp(obj));
        obj.SetActive(false);
    }

    public void EquipItemPopUp()
    {
        popUpBG.SetActive(true);
        itemEquipPopUp.SetActive(true);
        //StartCoroutine("PopEquipPopUp");//이부분 코루틴이 작동을 안하는데 이유를 모르겠습니다.
    }

    public void ItemDetailPopUp()
    {
        itemDetailPopUp.SetActive(true);
    }

    public void CloseDetailPopUp()
    {
        shop.SetActive(true);
        itemDetailPopUp.SetActive(false);
    }

    public void OnClickEquip()
    {
        onEquipButton?.Invoke(curItem.item);
        popUpBG.SetActive(false);
    }

    public void OnClickPurchase()
    {
        if(player.GetComponent<PlayerInfo>().gold - curItemData.price >= 0)
        {
            onPurchaseButton?.Invoke(curItemData);
        }
        else
        {
            PopNoGoldPopUp();
        }
    }

    public void PopPurchasePopUp()
    {
        purchaseItemPopUp.SetActive(true);
    }

    public void PopInventoryFullPopUp()
    {
        inventoryFullPopUp.SetActive(true);
    }

    public void PopNoGoldPopUp()
    {
        noGoldPopUp.SetActive(true);
    }

    IEnumerator PopEquipPopUp()
    {
        itemEquipPopUp.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(new Vector3(0, 1000, 0), new Vector3(0, 0, 0), Time.deltaTime * 0.5f);

        yield return null;
    }

    IEnumerator CancelPopUp(GameObject obj)
    {
        obj.transform.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(itemEquipPopUp.transform.position, new Vector3(0, 1000, 0), Time.deltaTime * 5f);
        yield return new WaitForSeconds(1);
        obj.SetActive(false);
    }
}
