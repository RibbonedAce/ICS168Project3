using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;
public class MoveAndAttack : NetworkBehaviour {

    // Use this for initialization
    //private GameObject destination;
    private Vector3 destinationPos;
    private Rigidbody _rigidbody;
    private NavMeshAgent _navMesh;
    public bool isSetDest;
    public int team;
	void Awake () {
        _rigidbody = GetComponent<Rigidbody>();
        //destination = GameObject.FindGameObjectWithTag("destination");
        //team decider
        if (transform.position.x <= 0)
        {
            destinationPos = new Vector3(9.5f, 1, 0);
            team = 0;
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            destinationPos = new Vector3(-9.5f, 1, 0);
            team = 1;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
            

        _navMesh = GetComponent<NavMeshAgent>();
        isSetDest = false;
        if (_navMesh == null)
            Debug.Log("Error, NavMeshAgent component doesn't exist on " + gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {
        if (_navMesh != null && !isSetDest)
            SetDestination();
        else
            _navMesh.isStopped = true;
	}

    private void SetDestination()
    {
        isSetDest = true;
        _navMesh.isStopped = false;
        //if(destination != null)
        //    _navMesh.SetDestination(destination.transform.position);
        _navMesh.SetDestination(destinationPos);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "destination")
        {
            Destroy(gameObject);
        }
    }
}
