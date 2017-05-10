using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    public Transform player;

    private void Update()
    {
        transform.position = new Vector3(player.position.x - 50, player.position.y, 0f); //object moves with player
        
        foreach (Collider2D c in Physics2D.OverlapBoxAll(transform.position, new Vector2(1, 100), 0f)) //get all the colliders
        {  
            Destroy(c.gameObject);
        }        
    }
}   
