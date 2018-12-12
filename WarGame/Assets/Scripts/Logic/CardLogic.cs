using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class CardLogic
{
    public CardAsset ca;
    public GameObject VisualRepresentation;

    //public int ID
    //{
    //    get{ return UniqueCardID; }
    //}

    public CardLogic(CardAsset ca)
    {
        this.ca = ca;
        //UniqueCardID = IDFactory.GetUniqueID();
        //UniqueCardID = IDFactory.GetUniqueID();
        //CardsCreatedThisGame.Add(UniqueCardID, this);
    }

    // STATIC (for managing IDs)
    public static Dictionary<int, CardLogic> CardsCreatedThisGame = new Dictionary<int, CardLogic>();

}
