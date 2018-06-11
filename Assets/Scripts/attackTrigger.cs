using UnityEngine;
using UnityEngine.Networking;

public class attackTrigger : NetworkBehaviour
{
    [SerializeField]
    protected GameObject minion;
    [SerializeField]
    protected float attackInterval;
    [SerializeField]
    protected float attackLast;
    protected int team;
    protected GameObject enemy;
    protected MoveAndAttack _maa;

    // Use this for initialization
    protected virtual void Awake()
    {
        _maa = minion.GetComponent<MoveAndAttack>();
        enemy = null;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!_maa.isServer)
            return;

        team = _maa.team;
        //Debug.Log(enemy);
        if (enemy == null)
            _maa.isSetDest = false;
        else
            _maa.isSetDest = true;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!_maa.isServer)
            return;

        if (other.tag == "minion" && enemy == null && other.GetComponent<MoveAndAttack>().team != team)
            enemy = other.gameObject;
    }


    protected virtual void OnTriggerExit(Collider other)
    {
        if (!_maa.isServer)
            return;

        if (other.gameObject == enemy)
            enemy = null;
    }
}