using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommingMissiles : MonoBehaviour
{
    public Transform target;
    public bool isActive;
    public float angleChangeSpeed;
    public float movementSpeed;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!isActive)
            return;

        Vector3 dir = target.position - transform.position;
        dir.Normalize();
        Vector3 rotationAmount = Vector3.Cross(dir, transform.forward) * Vector3.Angle(transform.forward , dir);
        rb.angularVelocity = rotationAmount * -angleChangeSpeed;
        rb.velocity = transform.forward * movementSpeed;

    }
}
