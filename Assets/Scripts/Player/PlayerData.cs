using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int point;
    public int coin;
    public string[] itemNames;
    public int[] itemCounts;

    public PlayerData(Player player)
    {
        point = player.point;
        coin = player.coin;
        itemNames = player.itemNames;
        itemCounts = player.itemCounts;

    }
}
