using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    Player player;

    void Start() {
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
    }

    void Update() {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        player.setJump(false);
        Debug.Log("Player landed.");
    }

}
