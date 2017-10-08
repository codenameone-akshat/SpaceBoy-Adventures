using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform player;

    private void Awake()
    {
        transform.position = new Vector3(player.position.x + 6, 0f, -15f);    //setting defailt position to lower level i.e. player start
    }   
    // Update is called once per frame
    void Update () {

        Vector3 original = new Vector3(player.position.x + 6, 0f, -15f);    //lower level position
        Vector3 target = new Vector3(player.position.x + 6, 10f, -15f);      //higher level position

        if (player.position.y > 7.5f)
            transform.position = Vector3.MoveTowards(transform.position, target, 0.3f);
        else
            transform.position = Vector3.MoveTowards(transform.position, original, 0.3f);
    }
}
