using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TankTypeUnit : attackTrigger {

    [SerializeField]
    private float attackDistance;

	// Use this for initialization
	protected override void Awake () {
        base.Awake();
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if (enemy != null && _maa.isSetDest)
            Attack();
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
        float dist = Vector3.Distance(GetComponentInParent<Transform>().position,enemy.transform.position);
        if (attackLast >= attackInterval && dist <= attackDistance)
        {
            enemy.GetComponent<minionInfo>().currentHealth -= 20;
        }
        attackLast = Mathf.Min(attackLast + Time.deltaTime, attackInterval);
    }
}
