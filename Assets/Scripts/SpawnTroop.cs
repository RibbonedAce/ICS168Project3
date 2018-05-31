using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnTroop : MonoBehaviour {

    // Use this for initialization
    private Vector3 spawnLocation;
    private float spawnRate;
    public GameObject troop;

	void Awake () {
        Debug.Log("Building Type: " + gameObject.name);
        spawnLocation = transform.position - new Vector3(0,0,1);
        spawnRate = 5.0f;
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
            Instantiate(troop, spawnLocation, Quaternion.identity);
        }
    }
}
