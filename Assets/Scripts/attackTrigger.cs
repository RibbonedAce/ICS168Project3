using UnityEngine;
using UnityEngine.Networking;

public class attackTrigger : NetworkBehaviour
{
    public GameObject minion;
    public float fireInterval;
    private float firedLast;
    private int team;
    private GameObject enemy;

    // Use this for initialization
    void Start()
    {
        if (!minion.GetComponent<MoveAndAttack>().isServer)
            return;

        enemy = null;
        team = minion.GetComponent<MoveAndAttack>().team;
    }

    // Update is called once per frame
    void Update()
    {
        if (!minion.GetComponent<MoveAndAttack>().isServer)
            return;

        Debug.Log(enemy);
        if (enemy == null)
            minion.GetComponent<MoveAndAttack>().isSetDest = false;
        else
        {
            minion.GetComponent<MoveAndAttack>().isSetDest = true;
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
        if (!minion.GetComponent<MoveAndAttack>().isServer)
            return;

        if (other.tag == "minion" && enemy == null && other.GetComponent<MoveAndAttack>().team != team)
            enemy = other.gameObject;
    }



    private void OnTriggerExit(Collider other)
    {
        if (!minion.GetComponent<MoveAndAttack>().isServer)
            return;

        if (other.gameObject == enemy)
            enemy = null;
    }
}