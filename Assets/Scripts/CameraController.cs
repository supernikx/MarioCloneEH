using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    /*public Transform playerpos;
    
    // Update is called once per frame
    void Update () {
        gameObject.transform.position = playerpos.transform.position + offset;
	}*/

    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = 0.5f;
    private Vector3 velocity;

    private void LateUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 centerPoin = GetCenterPoint();
        Vector3 newPosition = centerPoin + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null)
            {
                targets.Remove(targets[i]);
            }
            else
            {
                bounds.Encapsulate(targets[i].position);
            }
        }

        return bounds.center;
    }
}
