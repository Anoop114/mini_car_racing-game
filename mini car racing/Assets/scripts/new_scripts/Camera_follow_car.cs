using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow_car : MonoBehaviour
{
    public Transform CarBody;
    public Vector3 offset;
    public float FollowSpeed = 10f;
    public float LookupSpeed = 50f;

    public void LookAtTarget()
    {
        Vector3 LookDir = CarBody.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(LookDir,Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation,rot,LookupSpeed*Time.deltaTime); 
    }
    public void MoveToTarget()
    {
        Vector3 TargetPos = CarBody.position + CarBody.forward*offset.z + 
                            CarBody.right*offset.x + CarBody.up*offset.y;
        transform.position = Vector3.Lerp(transform.position,TargetPos,FollowSpeed*Time.deltaTime); 
    }
    private void FixedUpdate()
    {
        LookAtTarget();
        MoveToTarget();
    }
}
