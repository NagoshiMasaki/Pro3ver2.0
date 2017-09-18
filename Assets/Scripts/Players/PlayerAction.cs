using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {

    [SerializeField]
    PlayerStatus playerStatusScript;
    int playerNumber = 1;
    [SerializeField]
    BoardStatus boardStatusScript;
	// Update is called once per frame
	void Update () {
        Mouse();	
	}

    void Mouse()
    {
        int turn = playerStatusScript.GetPlayerTurn();
        if (turn == playerNumber)
        {
            if(Input.GetMouseButtonDown(0))
            RayAction();
        }
    }

    void RayAction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask masslayer = playerStatusScript.GetMassLayer();
        if(Physics.Raycast(ray, out hit,Mathf.Infinity,masslayer))
        {
            AtachMass(hit.collider.gameObject);
        }
    }

    void AtachMass(GameObject attachobj)
    {
        int length = 0 , side = 0, number = 0;
        attachobj.GetComponent<MassStatus>().GetNumbers(ref length,ref side,ref number);
    }

}
