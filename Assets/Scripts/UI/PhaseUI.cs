
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseUI : MonoBehaviour {

    [SerializeField]
    Text phaseText;
    public void UpdatePhase(SituationManager.Phase phase,int playernum)
    {
        phaseText.text = phase.ToString();
    }
}
