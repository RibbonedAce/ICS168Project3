using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    [SerializeField]
    private float size = 1f;

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

        Vector3 buildLocation = new Vector3((float)xCount * size, (float)yCount * size + 1, (float)zCount * size);
        buildLocation += transform.position;
        return buildLocation;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for(float x = transform.position.x; x < transform.position.x + 3; x += size)
        {
            for (float z = -3; z < 4; z += size)
            {
                Vector3 point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}
