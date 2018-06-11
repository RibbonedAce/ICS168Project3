using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RangeUnitType : attackTrigger {

	// Use this for initialization
	protected override void Awake () {
        base.Awake();
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if (enemy != null && _maa.isSetDest)
            Shoot();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

    private void Shoot()
    {
        if (attackLast >= attackInterval)
        {
            Quaternion rot = Quaternion.Euler(90f, Vector3.SignedAngle(Vector3.forward, enemy.transform.position - transform.position, Vector3.up), 0f);
            GameObject g = Instantiate(GameController.Instance.bullets[team], transform.position, rot);
            NetworkServer.Spawn(g);
            attackLast = 0f;
        }
        attackLast = Mathf.Min(attackLast + Time.deltaTime, attackInterval);
    }
}
