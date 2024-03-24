using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GameObject[] physicable = GameObject.FindGameObjectsWithTag("inGravity");

        foreach(GameObject physicableObj in physicable)
        {
            Rigidbody2D rb = physicableObj.GetComponent<Rigidbody2D>();

            Vector3 position = rb.transform.position;

            Vector3 distance = transform.position - position;

            float force = (SpaceLogic.GRAVITY_SCALE * rigidbody.mass * rb.mass) / (distance.sqrMagnitude);

            rb.AddForce(distance.normalized *  force * Time.fixedDeltaTime);
        }
    }
}
