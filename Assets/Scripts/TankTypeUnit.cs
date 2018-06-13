using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class TankTypeUnit : attackTrigger {


    [SerializeField]
    private float attackDistance;
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
            Debug.Log("Enemy found");
            Attack();
        }
        else if (enemy == null)
        {
            parent.GetComponent<MoveAndAttack>().SetDestination(parent.GetComponent<MoveAndAttack>().destinationPos);
            Debug.Log("Enemy not found");
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
        if (attackLast >= attackInterval && dist <= attackDistance && enemy != null)
        {
            enemy.GetComponent<minionInfo>().currentHealth -= 10;
            attackLast = 0;
        }
        attackLast = Mathf.Min(attackLast + Time.deltaTime, attackInterval);
    }
}
