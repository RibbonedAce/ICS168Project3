using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class TankTypeUnit : attackTrigger {


    [SerializeField]
    private float attackDistance;
    [SerializeField]
    private int damage;
    private Transform parent = null;
    // Use this for initialization
    protected override void Awake() {
        base.Awake();
        parent = transform.parent;
        if (parent == null)
            Debug.Log("Cant create instance of parent");
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
        if (enemy != null && _maa.isSetDest)
        {
            parent.GetComponent<MoveAndAttack>().SetDestination(enemy.transform.position);
            Attack();
        }
        else if (enemy == null)
        {
            parent.GetComponent<MoveAndAttack>().SetDestination(parent.GetComponent<MoveAndAttack>().destinationPos);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

    private void Attack()
    {
        float dist = Vector3.Distance(GetComponentInParent<Transform>().position, enemy.transform.position);
        if (dist <= attackDistance && enemy != null)
        {
            Debug.Log("attackLast: " + attackLast + "Time: " + Time.time);
            if (attackLast <= Time.time)
            {
                enemy.GetComponent<minionInfo>().currentHealth -= damage;
                attackLast = Time.time + attackInterval;
            }
        }
    }
}
