using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {


    public float size = 1.5f;

    public float xMax = 5;

    public float zMax = 5;

    // Use this for initialization
    private void Awake()
    {
    }

    // Update is called once per frame
    void Update () {
		
	}

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;
        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 buildLocation = new Vector3((float)xCount * size, (float)yCount * size, (float)zCount * size);
        buildLocation += transform.position;
        return buildLocation;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for(float x = transform.position.x - xMax; x <= transform.position.x + xMax; x += size)
        {
            for (float z = transform.position.z - zMax; z <= transform.position.z + zMax; z += size)
            {
                Vector3 point = GetNearestPointOnGrid(new Vector3(x,transform.position.y, z));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}
