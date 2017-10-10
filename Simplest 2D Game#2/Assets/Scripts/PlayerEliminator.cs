using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEliminator : MonoBehaviour {

    public Transform cameraPos;
	
	// this script is used for the water that follows the camera in which if player falls, it dies.
	void Update () {

        this.transform.position = new Vector3(cameraPos.position.x + 5f, -12f);
	}
}
