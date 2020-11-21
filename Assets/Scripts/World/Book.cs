using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    
    public float maxAngle = -90f;
    float xAngle;
    float anglePerSecond;


    void Start()
    {
        // rotation factor per second, based on length of timer and max rotation
        anglePerSecond = GameManager.Instance.startTime / maxAngle;
    }

    void Update()
    {
        
        //update rotation
        float f = GameManager.Instance.timer / GameManager.Instance.startTime;
        Vector3 angle = new Vector3((maxAngle * f) - maxAngle, transform.rotation.y, transform.rotation.z);
        transform.rotation = Quaternion.Euler(angle);
    }
}
