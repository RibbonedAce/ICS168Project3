using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBarController : MonoBehaviour {
    public GameObject minion;
    private Camera cam;
    private minionInfo health;
    private float healthPercentage;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.LookRotation(-cam.transform.position);

        health = minion.GetComponent<minionInfo>();
        healthPercentage = (float)health.currentHealth / (float)health.maxHealth;

        // health bar update
        transform.localScale = new Vector3(healthPercentage, transform.localScale.y, transform.localScale.z);
        if (healthPercentage <= 0.66)
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        if (healthPercentage <= 0.33)
            gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
