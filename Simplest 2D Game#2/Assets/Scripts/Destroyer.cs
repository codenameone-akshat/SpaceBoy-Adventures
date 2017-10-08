//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Destroyer : MonoBehaviour {

//    public Transform player;

//    private void Update()
//    {
//        transform.position = new Vector3(player.position.x - 25, 0f , 0f); //object moves with player
        
//        foreach (Collider2D c in Physics2D.OverlapBoxAll(transform.position, new Vector3(1, 100, 50), 0f)) //get all the colliders
//        {  
//            Destroy(c.gameObject);
//        }        
//    }
//}   
