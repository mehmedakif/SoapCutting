using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int point;
    public int coin;
    public string[] itemNames;
    public int[] itemCounts;

    public Player(int _point, int _coin)
    {
        this.point = _point;
        this.coin = _coin;
        this.itemCounts = null;
        this.itemNames = null;
    }
    public Player GetPlayer()
    {
        return this;
    }

    public void SetPlayerItems(string[] _itemNames, int[] _itemCounts)
    {
        itemNames = _itemNames;
        itemCounts = _itemCounts;
    }

    public void SetPlayerPoints(int _point, int _coin)
    {
        point = _point;
        coin = _coin;
    }


}
