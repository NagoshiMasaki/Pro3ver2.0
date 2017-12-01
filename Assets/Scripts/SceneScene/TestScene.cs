using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Test");
        }
	}
}
