using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private List<int> itemPrices;

    [SerializeField]private Player player;
    [SerializeField]private FloatingTextManager floatingTextManager;

    private int money;

    public int GetMoney()
    {
        return money;
    }

    public int SetMoney(int value)
    {
        return money += value;
    }

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    private void Awake()
    {
        instance = this;
    }
}
