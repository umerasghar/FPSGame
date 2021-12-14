using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetToFollow;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = this.gameObject.transform.position - targetToFollow.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = targetToFollow.position + offset;
        offset= this.gameObject.transform.position - targetToFollow.position;
    }
}
