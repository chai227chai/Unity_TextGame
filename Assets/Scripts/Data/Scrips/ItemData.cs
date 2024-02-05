using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public enum SpecType
{
    MaxHealth,
    Attack,
    Defence,
    Speed,
    Crit_Rate,
    Crit_DMG,
    Evasion
}

public enum SpecChangeType
{
    Add,
    Multiple,
    Override,
}

public enum ItemType
{
    Weapon,
    Armor,
    Shield
}

[System.Serializable]
public class ItemSpec
{
    public SpecChangeType changeType;
    public SpecType specType;
    public int value;

    public string GetValueString()
    {
        switch (changeType)
        {
            case SpecChangeType.Add:
                return (value > 0 ? " + " : " - ") + "<color=yellow>" + value.ToString() + "</color>";
            case SpecChangeType.Multiple:
                return " <color=yellow>" + ((float)value / 100).ToString() + "</color>" + " 배";
            case SpecChangeType.Override:
                return " 고정 " + "<color=yellow>" + value.ToString() + "</color>";
            default:
                return "";
        }
    }

    public float GetValue()
    {
        if (changeType == SpecChangeType.Multiple)
        {
            return (float)value / 100;
        }
        else
        {
            return value;
        }
    }

    public string GetSpecType()
    {
        switch (specType)
        {
            case SpecType.Attack:
                return "공격력";
            case SpecType.Defence:
                return "방어력";
            case SpecType.Speed:
                return "속  도";
            case SpecType.MaxHealth:
                return "최대 체력";
            case SpecType.Crit_Rate:
                return "치명타 확률";
            case SpecType.Crit_DMG:
                return "치명타 데미지";
            case SpecType.Evasion:
                return "회피율";
            default:
                return "";
        }
    }
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public List<ItemSpec> itemSpecs;
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public int price;

    public string GetItemType()
    {
        switch (itemType)
        {
            case ItemType.Weapon:
                return "무기";
            case ItemType.Armor:
                return "갑옷";
            case ItemType.Shield:
                return "방패";
            default:
                return "";
        }
    }

    private string GetSpec(ItemSpec spec)
    {
        StringBuilder stringStat = new StringBuilder();

        stringStat.Append(spec.GetSpecType());
        stringStat.Append(spec.GetValueString());
        stringStat.Append("\n");

        return stringStat.ToString();
    }

    public string GetSpecString()
    {
        StringBuilder stringSpec = new StringBuilder();

        foreach(ItemSpec spec in itemSpecs)
        {
            stringSpec.Append(GetSpec(spec));
        }

        return stringSpec.ToString();
    }
}
