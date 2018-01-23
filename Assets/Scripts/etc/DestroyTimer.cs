using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{

    [SerializeField]
    float destroyTimer;
	void Start ()
    {
        Destroy(gameObject,destroyTimer);	
	}
	
}
