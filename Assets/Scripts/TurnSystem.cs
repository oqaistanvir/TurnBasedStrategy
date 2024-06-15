using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }
    public event EventHandler OnTurnChanged;
    private int turnNumber = 1;
    private bool isPlayerTurn = true;
    private bool isGameOver;
    private void Awake()
    {
        Instance = this;
        isGameOver = false;
    }
    public void NextTurn()
    {
        turnNumber++;
        isPlayerTurn = !isPlayerTurn;

        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }
    public int GetTurnNumber()
    {
        return turnNumber;
    }
    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
    public bool IsGameOver()
    {
        return isGameOver;
    }
    public void SetIsGameOver(bool isGameOver)
    {
        this.isGameOver = isGameOver;
    }
}
