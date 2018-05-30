using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    #region Variables
    private float Health;
    #endregion

    #region Properties

    #endregion

    #region Events
    void Awake()
    {
        transform.position = new Vector3(-8.5f,1f,0);
        Health = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(t.rawPosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Build(ray, hit);
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Build(ray, hit);
            }
        }
	}
    #endregion

    #region Methods
    private void Build(Ray ray, RaycastHit hit)
    {
        GameObject g = Instantiate
            (
                //GameController.Instance.buildings[Random.Range(0, GameController.Instance.buildings.Length)],
                GameController.Instance.buildings[0],
                hit.point.MoveToAxisPosition(Axis.Y, 1, ray.direction), 
                Quaternion.identity
            );
        NetworkServer.Spawn(g);
    }
    #endregion

    #region Coroutines

    #endregion
}
