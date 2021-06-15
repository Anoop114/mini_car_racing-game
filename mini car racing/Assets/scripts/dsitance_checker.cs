using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class dsitance_checker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private Transform player;
    [SerializeField]private Transform Checkpoint;
    [SerializeField] private Text distanceText;

    [SerializeField] private Slider Distance;
    private float distance;

    // Start is called before the first frame update
    void Update()
    {
        distance = distance_check();
        Debug.Log(distance);
        Distance.value=distance;
        distanceText.text= distance.ToString("F1")+" meters.";
        if (distance<=0){
            distanceText.text="Finish";
        }
    }
    private float distance_check(){
        return Vector3.Distance(Checkpoint.transform.position,player.transform.position);
    }
}
