using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;
public class MoveAndAttack : NetworkBehaviour {

    // Use this for initialization
    private GameObject destination;
    private Rigidbody _rigidbody;
    private NavMeshAgent _navMesh;
    private bool isSetDest;
	void Awake () {
        _rigidbody = GetComponent<Rigidbody>();
        destination = GameObject.FindGameObjectWithTag("destination");
        _navMesh = GetComponent<NavMeshAgent>();
        isSetDest = false;
        if (_navMesh == null)
            Debug.Log("Error, NavMeshAgent component doesn't exist on " + gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {
        if (_navMesh != null && !isSetDest)
            SetDestination();
	}

    private void SetDestination()
    {
        isSetDest = true;
        if(destination != null)
            _navMesh.SetDestination(destination.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "destination")
        {
            Destroy(gameObject);
        }
    }
}
