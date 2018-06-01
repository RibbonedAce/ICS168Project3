using UnityEngine;
using UnityEngine.Networking;

public class attackTrigger : NetworkBehaviour
{
    public GameObject minion;
    private int team;
    private GameObject enemy;
    // Use this for initialization
    void Start()
    {
        enemy = null;
        team = minion.GetComponent<MoveAndAttack>().team;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(enemy);
        if (enemy == null)
            minion.GetComponent<MoveAndAttack>().isSetDest = false;
        else
        {
            minion.GetComponent<MoveAndAttack>().isSetDest = true;
            enemy.GetComponent<minionInfo>().currentHealth -= 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "minion" && enemy == null && other.GetComponent<MoveAndAttack>().team != team)
            enemy = other.gameObject;
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == enemy)
            enemy = null;
    }
}