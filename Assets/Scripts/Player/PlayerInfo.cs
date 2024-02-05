using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public enum Job
{
    Warrior,
    Wizard,
    Rogue
}

[System.Serializable]
public class PlayerStat
{
    [Header("PlayerStat")]
    public int maxHealth;
    public int attack;
    public int defence;
    public int speed;
    public int crit_damage;
    public int crit_rate;
    public int evasion;
}

public class PlayerInfo : MonoBehaviour
{
    [Header("PlayerInfo")]
    public string playerName;
    public int level;
    public Job job;
    public int gold;

    [SerializeField]private PlayerStat baseStat;
    public PlayerStat totalStat;

    public TextMeshProUGUI nameTXT;
    public TextMeshProUGUI jobTXT;
    public TextMeshProUGUI levelTXT;
    public TextMeshProUGUI goldTXT;

    [Header("Character Stat Text")]
    public TextMeshProUGUI maxHealth;
    public TextMeshProUGUI attack;
    public TextMeshProUGUI defence;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI crit_rate;
    public TextMeshProUGUI crit_damage;
    public TextMeshProUGUI evasion;

    [Header("CharacterStatChange")]
    public List<Item> equipedItem = new List<Item>();

    private void Awake()
    {
        UpdatePlayerStat();
    }

    private void Start()
    {
        nameTXT.text = playerName;
        jobTXT.text = GetJob();
        UpdateLevelTXT();
        UpdateGoldTXT();
    }

    public void UpdatePlayerStat()
    {
        totalStat = new PlayerStat();

        totalStat.maxHealth = baseStat.maxHealth;
        totalStat.attack = baseStat.attack;
        totalStat.defence = baseStat.defence;
        totalStat.speed = baseStat.speed;
        totalStat.crit_damage = baseStat.crit_damage;
        totalStat.crit_rate = baseStat.crit_rate;
        totalStat.evasion = baseStat.evasion;

        foreach (Item item in equipedItem)
        {
            ApplyItemSpec(item);
        }

        UpdateStatTXT();
    }

    private void ApplyItemSpec(Item item)
    {
        foreach (ItemSpec itemSpec in item.itemData.itemSpecs.OrderBy(o => o.changeType))
        {
            if(itemSpec.changeType == SpecChangeType.Add)
            {
                UpdateStat((basestat, newstat) => (int)(basestat + newstat), itemSpec);
            }
            if (itemSpec.changeType == SpecChangeType.Multiple)
            {
                UpdateStat((basestat, newstat) => (int)(basestat * newstat), itemSpec);
            }
            if (itemSpec.changeType == SpecChangeType.Override)
            {
                UpdateStat((basestat, newstat) => (int)(newstat), itemSpec);
            }
        }
    }

    private void UpdateStat(Func<int, float, int> operation, ItemSpec itemSpec)
    {
        switch (itemSpec.specType)
        {
            case SpecType.MaxHealth:
                totalStat.maxHealth = operation(totalStat.maxHealth, itemSpec.GetValue());
                break;
            case SpecType.Attack:
                totalStat.attack = operation(totalStat.attack, itemSpec.GetValue());
                break;
            case SpecType.Defence:
                totalStat.defence = operation(totalStat.defence, itemSpec.GetValue());
                break;
            case SpecType.Speed:
                totalStat.speed = operation(totalStat.speed, itemSpec.GetValue());
                break;
            case SpecType.Crit_Rate:
                totalStat.crit_rate = operation(totalStat.crit_rate, itemSpec.GetValue());
                break;
            case SpecType.Crit_DMG:
                totalStat.crit_damage = operation(totalStat.crit_damage, itemSpec.GetValue());
                break;
            case SpecType.Evasion:
                totalStat.evasion = operation(totalStat.evasion, itemSpec.GetValue());
                break;
        }

    }

    private void UpdateLevelTXT()
    {
        levelTXT.text = level.ToString("00");
    }

    public void AddGold(int price)
    {
        gold += price;
        UpdateGoldTXT();
    }

    public void UseGold(int price)
    {
        gold -= price;
        UpdateGoldTXT();
    }

    private void UpdateGoldTXT()
    {
        goldTXT.text = gold.ToString("N0");
    }



    private void UpdateStatTXT()
    {
        SetStatTXT(maxHealth, baseStat.maxHealth, totalStat.maxHealth);
        SetStatTXT(attack, baseStat.attack, totalStat.attack);
        SetStatTXT(defence, baseStat.defence, totalStat.defence);
        SetStatTXT(speed, baseStat.speed, totalStat.speed);
        SetStatTXT(crit_rate, baseStat.crit_rate, totalStat.crit_rate, "%");
        SetStatTXT(crit_damage, baseStat.crit_damage, totalStat.crit_damage, "%");
        SetStatTXT(evasion, baseStat.evasion, totalStat.evasion, "%");
    }

    private void SetStatTXT(TextMeshProUGUI text, int baseStat, int totalStat, string percent = "")
    {
        if(totalStat > baseStat)
        {
            text.color = Color.green;
        }
        else if(totalStat < baseStat)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.white;
        }

        text.text = totalStat.ToString() + percent;
    }

    private string GetJob()
    {
        switch (job)
        {
            case Job.Warrior:
                return "전사";
            case Job.Wizard:
                return "마법사";
            case Job.Rogue:
                return "도적";
            default:
                return "백수";
        }
    }
}
