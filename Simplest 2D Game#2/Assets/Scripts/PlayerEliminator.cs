using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEliminator : MonoBehaviour {

    public Transform cameraPos;
	
	// Update is called once per frame
	void Update () {

        this.transform.position = new Vector3(cameraPos.position.x, -15f);
	}
}
