using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCar : MonoBehaviour
{
    public GameObject CarBody;
    CarDetails CarDetailScript;
    Transform CarBodyTransform;
    [SerializeField]private Vector3 offset;
    [SerializeField]private float FollowSpeed = 10f;
    [SerializeField]private float LookupSpeed = 50f;
    public void Start() {
        StartCoroutine("CarGameObject");
        CarBodyTransform = CarBody.transform;
        CarDetailScript = CarBody.GetComponent<CarDetails>();
        CarDetailScript.Camera = gameObject;
    }
    
    private void LookAtTarget()
    {
        Vector3 LookDir = CarBodyTransform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(LookDir,Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation,rot,LookupSpeed*Time.deltaTime); 
    }
    private void MoveToTarget()
    {
        Vector3 TargetPos = CarBodyTransform.position + CarBodyTransform.forward*offset.z + 
                            CarBodyTransform.right*offset.x + CarBodyTransform.up*offset.y;
        transform.position = Vector3.Lerp(transform.position,TargetPos,FollowSpeed*Time.deltaTime); 
    }
    IEnumerator CarGameObject()
    {
        yield return new WaitForSeconds(0.1f);
    }
    private void FixedUpdate()
    {
        if(CarBody != null)
        {
            LookAtTarget();
            MoveToTarget();
        }
        else
        {
            return;
        }
    }
}
