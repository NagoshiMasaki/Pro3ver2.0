using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour {

    [SerializeField]
    PhaseUI phaseUIScript;

    public void  UpdatePhase(SituationManager.Phase phase,int playernum)
    {
        phaseUIScript.UpdatePhase(phase,playernum);
    }
}
