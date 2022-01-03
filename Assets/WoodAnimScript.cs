using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodAnimScript : MonoBehaviour
{

    Vector3 targetPosition;
    Quaternion targetrotation;
    void Start()
    {
        targetPosition = new Vector3(0.8f, transform.localPosition.y, transform.localPosition.z);
        targetrotation = new Quaternion(0.5f, -0.5f, 0.5f, 0.5f);
    }

    
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        moveWood();
        Debug.Log(targetrotation);
    }
    void moveWood()
    {      
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetrotation, Time.deltaTime);
    }
}
