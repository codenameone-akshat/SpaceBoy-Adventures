using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    public Transform player;

    private void Update()
    {
        transform.position = new Vector3(player.position.x - 40, 0f, 0f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject.Destroy(collision.gameObject); // this destroys a rigidbody only
        //refer internet to learn how to destroy fixed joints
    }
}   
