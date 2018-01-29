﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour {

    [SerializeField]
    DeckClass player1Deck;
    [SerializeField]
    DeckClass player2Deck;

    [SerializeField]
    UImanager uimanagerScript;

    public void GameFinish(int num)
    {
        uimanagerScript.GameFinish(num);
    }

    public GameObject GetDrawObj(int number)
    {
        switch (number)
        {
            case 1:
                return player1Deck.GetCharacter();
            case 2:
                return player2Deck.GetCharacter();
        }
        return null; 
    }
}
