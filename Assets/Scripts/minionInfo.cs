using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class minionInfo : NetworkBehaviour {
    public int maxHealth;
    public int currentHealth;
	// Use this for initialization
	void Start () {
        maxHealth = 100;
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}
