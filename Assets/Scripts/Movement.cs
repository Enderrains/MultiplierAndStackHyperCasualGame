using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    SwerseInput swerseInput;
    Rigidbody rigidbody;
    GameManager gameManager;
    Wood wood;

    [SerializeField] float swerseSpeed = 0.5f;
    [SerializeField] float forwardSpeed = 2f;
    float maxspeed = 1f;

    Vector3 movement;

    bool isMovement = true;
    bool isFinish = false;
    float maxSwerseAmount = 1f;


    private void Awake()
    {
        swerseInput = GetComponent<SwerseInput>();
        rigidbody = GetComponent<Rigidbody>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        wood = GameObject.FindWithTag("parrent").GetComponent<Wood>();
    }

    private void FixedUpdate()
    {
        if (isMovement &&gameManager.isPlay)
        {
            swerseMovement();
            forwardMovement();
        }
        if (isFinish)
        {
            rigidbody.AddForce(movement.normalized * 5);
        }
    }


    void swerseMovement()
    {
        float swerseAmount = Time.deltaTime * swerseSpeed * swerseInput.MoveFactorX;
        swerseAmount = Mathf.Clamp(swerseAmount, -maxSwerseAmount, maxSwerseAmount);
        transform.position += new Vector3(swerseAmount, 0, 0).normalized*Time.deltaTime*swerseSpeed;
    }

    void forwardMovement()
    {

        movement = new Vector3(0f,0f, Mathf.Clamp(forwardSpeed *50* Time.deltaTime,-maxspeed,maxspeed));
        transform.position+= movement.normalized*forwardSpeed;
        //rigidbody.velocity = Vector3.forward * Time.deltaTime * forwardSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishPoint"))
        {
            isMovement = false;
            rigidbody.velocity = Vector3.zero;
            transform.position = new Vector3(other.transform.position.x,transform.position.y,other.transform.position.z);
            isFinish = true;
            wood.BuildBridge();              
        }
        if (other.CompareTag("lastWood"))
        {
            rigidbody.velocity = Vector3.zero;
            Debug.Log("durdu");
            isFinish = false;
            gameManager.isLevelComplate = true;
            wood.GoldCalculator();
        }
    }
}
