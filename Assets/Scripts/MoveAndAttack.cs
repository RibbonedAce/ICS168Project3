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
    private minionInfo _minionInfo;

    public bool isSetDest;
    [SyncVar(hook = "SetTeam")]
    public int team;
    public GameObject afterEffect;

	public override void OnStartServer () {
        base.OnStartServer();

        _rigidbody = GetComponent<Rigidbody>();
        _minionInfo = GetComponent<minionInfo>();
        //destination = GameObject.FindGameObjectWithTag("destination");
        //team decider
        if (transform.position.x <= 0)
        {
            SetTeam(0);
            destinationPos = GetDestination();
        }
        else
        {
            SetTeam(1);
            destinationPos = GetDestination();
        }
        //Debug.Log(destinationPos);
        _navMesh = GetComponent<NavMeshAgent>();
        isSetDest = false;
        if (_navMesh == null)
            Debug.Log("Error, NavMeshAgent component doesn't exist on " + gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {
        if (!isServer)
            return;

        if (_navMesh != null && !isSetDest)
            SetDestination();
        else
            _navMesh.isStopped = true;
	}

    private void OnDestroy()
    {
        GameObject g = Instantiate(afterEffect, transform.position, Quaternion.identity);
    }

    private void SetTeam(int value)
    {
        team = value;
        gameObject.GetComponent<Renderer>().material = GameController.Instance.troopColors[team];
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
        if (!isServer)
            return;

        if (collision.transform.tag == "bullet")
        {
            Debug.Log("Bullet");
        }

        if (collision.transform.tag == "destination")
        {
            Destroy(gameObject);
        }
    }
    private Vector3 GetDestination()
    {
        GameObject EnemyPosition;
        if(team == 0)
        {
            EnemyPosition = GameObject.Find("Player2");
        }
        else
        {
            EnemyPosition = GameObject.Find("Player1");
        }
        if (EnemyPosition == null)
        {
            Destroy(gameObject, 3.0f);
            return new Vector3(0, 1, 0);
        }
        return EnemyPosition.transform.position;
    }
}
