using UnityEngine;
using UnityEngine.Networking;

public class attackTrigger : NetworkBehaviour
{
    public GameObject minion;
    public float fireInterval;
    private float firedLast;
    private int team;
    private GameObject enemy;
    private MoveAndAttack _maa;

    // Use this for initialization
    void Awake()
    {
        _maa = minion.GetComponent<MoveAndAttack>();
        enemy = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_maa.isServer)
            return;

        team = _maa.team;
        //Debug.Log(enemy);
        if (enemy == null)
            _maa.isSetDest = false;
        else
        {
            _maa.isSetDest = true;
            if (firedLast >= fireInterval)
            {
                Quaternion rot = Quaternion.Euler(90f, Vector3.SignedAngle(Vector3.forward, enemy.transform.position - transform.position, Vector3.up), 0f);
                GameObject g = Instantiate(GameController.Instance.bullets[team], transform.position, rot);
                NetworkServer.Spawn(g);
                firedLast = 0f;
            }
        }
        firedLast = Mathf.Min(firedLast + Time.deltaTime, fireInterval);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_maa.isServer)
            return;

        if ((other.tag == "minion" || other.tag == "building") && enemy == null && other.GetComponent<MoveAndAttack>().team != team)
            enemy = other.gameObject;
    }



    private void OnTriggerExit(Collider other)
    {
        if (!_maa.isServer)
            return;

        if (other.gameObject == enemy)
            enemy = null;
    }
}