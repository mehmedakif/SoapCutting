using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public GameObject selectedSoap;
    public GameObject selectedWall;
    public GameObject selectedGround;
    public PlayerData playerData;
    public Text coinValue;
    public Text soapsValue;

    void Start()
    {
        string path = Application.persistentDataPath + "/player.mars";
        if (!File.Exists(path))
        {
            Player player = new Player(0, 0);
            SaveSystem.SavePlayer(player);
        }
        playerData = SaveSystem.LoadPlayer();
        coinValue.text = playerData.point.ToString();
        soapsValue.text = playerData.point.ToString();

    }

    void Awake()
    {
        //Let the gameobject persist over the scenes
        DontDestroyOnLoad(selectedSoap);
        DontDestroyOnLoad(selectedWall);
        DontDestroyOnLoad(selectedGround);
    }
    

}
