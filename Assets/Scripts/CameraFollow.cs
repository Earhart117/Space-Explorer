using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _objectToFollow = null;

    Vector3 _objectOffset;

    private void Awake()
    {
        // create an offset between this position and the objects position
        _objectOffset = this.transform.position - _objectToFollow.position;
    }
    //camera should move last1
    private void LateUpdate()
    {
        //apply the offset every frame, to reposition this object
        this.transform.position = _objectToFollow.position + _objectOffset;
    }
 
}
