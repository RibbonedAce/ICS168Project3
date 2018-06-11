using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class minionInfo : NetworkBehaviour {
    [SyncVar,SerializeField]
    public int maxHealth;
    public int currentHealth;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isServer)
            return;

        if (currentHealth <= 0)
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}
