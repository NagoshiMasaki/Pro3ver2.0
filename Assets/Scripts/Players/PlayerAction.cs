using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    [SerializeField]
    PlayerStatus playerStatusScript;
    int playerNumber = 1;
    [SerializeField]
    BoardStatus boardStatusScript;

    void Update()
    {
        Mouse();
    }

    void Mouse()
    {
        int turn = playerStatusScript.GetPlayerTurn();
        if (turn == playerNumber)
        {
            if (Input.GetMouseButtonDown(0))
                RayAction();
        }
    }

    void RayAction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask masslayer = playerStatusScript.GetMassLayer();

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, masslayer))
        {
            AtachMass(hit.collider.gameObject);
            return;
        }

        LayerMask decklayer = playerStatusScript.GetDeckLayer();
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, decklayer))
        {
            AtachDeck(hit.collider.gameObject);
            return;
        }

        LayerMask illustrationlayer = playerStatusScript.GetIllustrationLayer();
        RaycastHit2D hit2D = Physics2D.Raycast(ray.origin,ray.direction ,Mathf.Infinity, illustrationlayer);
        if(hit2D.collider)
        {
            AtachIllustration(hit.collider.gameObject);
        }
    }

    void AtachMass(GameObject attachobj)
    {
        int length = 0, side = 0, number = 0;
        attachobj.GetComponent<MassStatus>().GetNumbers(ref length, ref side, ref number);
    }

    void AtachDeck(GameObject attachdeck)
    {
        SituationManager.Phase phase = playerStatusScript.GetPhase();
        if (phase != SituationManager.Phase.Draw)
        {
            int decknum = attachdeck.GetComponent<DeckClass>().GetPlayerNumber();
            int turnnum = playerStatusScript.GetPlayerTurn();
            if (turnnum == decknum)
            {
                Debug.Log("Draw");
                GameObject drawobj = playerStatusScript.GetDrawObj(decknum);
                playerStatusScript.SetPhase(SituationManager.Phase.Main1);
            }
        }
    }

    void AtachIllustration(GameObject attachobj)
    {
        if (playerStatusScript.GetPhase() == SituationManager.Phase.Main1)
        {
            int playernum = 0;
            int costnum = 0;
            int ratenum = 0;
            int dictionartnum = 0;
            attachobj.GetComponent<IllustrationStatus>().GetRate_Dictionary_Cost_Player_Number(ref ratenum,ref dictionartnum,ref costnum,ref playernum);
            if (playernum == playerStatusScript.GetPlayerTurn())
            {
            }
        }
    }
}
