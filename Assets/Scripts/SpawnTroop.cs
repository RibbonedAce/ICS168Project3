using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnTroop : NetworkBehaviour {

    // Use this for initialization
    private Vector3 spawnLocation;
    private float spawnRate;
    public GameObject troop;

    public override void OnStartServer()
    {
        base.OnStartServer();
        //Debug.Log("Building Type: " + gameObject.name);
        spawnLocation = transform.position - new Vector3(0, 0, 1);
        spawnRate = 8.0f;
        StartCoroutine("Spawn");
    }
	
	// Update is called once per frame
	void Update () {

    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            GameObject g = Instantiate(troop, spawnLocation, Quaternion.identity);
            NetworkServer.Spawn(g);
        }
    }
}
