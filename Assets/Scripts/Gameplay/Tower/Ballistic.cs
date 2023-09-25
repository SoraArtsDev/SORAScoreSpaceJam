using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballistic : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private float angle = 5.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>
();    }

    // Update is called once per frame
    void Update()
    {
        
    }


    Vector3 ShootBallistic(Vector3 destination, float angle)
    {
        Vector3 dir = destination - transform.position; // get Target Direction
        float height = dir.y; // get height difference
        dir.y = 0; // retain only the horizontal difference
        float dist = dir.magnitude; // get horizontal direction
        float a = angle * Mathf.Deg2Rad; // Convert angle to radians
        dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
        dist += height / Mathf.Tan(a); // Correction for small height differences

        // Calculate the velocity magnitude
        float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * dir.normalized; // Return a normalized vector.

    }

    public void FireCannonAtPoint(Vector3 position)
    {
        var velocity = ShootBallistic(position, angle);
        rb.transform.position = transform.position;
        rb.velocity = velocity;
    }
}
