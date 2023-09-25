using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballistic : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private float force = 45.0f;
    private int dmgAmount = 0;
    public GameObject enemyFinder;
    public GameObject[] sceneEnemies;


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector3(0, force, 0));
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

    public void FireCannonAtPoint(Vector3 position, float angle, float gravity, float velocityMult, int dmg, ref GameObject[] sceneEnemies)
    {
        rb = GetComponent<Rigidbody>();
        force = gravity;
        var velocity = ShootBallistic(position, angle);
        rb.transform.position = transform.position;
        rb.velocity = velocity* velocityMult;
        rb.useGravity = false;
        dmgAmount = dmg;

        this.sceneEnemies = sceneEnemies;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Floor")
        {
            return;
        }

        GameObject go = Instantiate(enemyFinder);
        go.transform.position = transform.position;
        go.GetComponent<Sora.BallisticEnemyFinder>().damage = dmgAmount;
        go.GetComponent<Sora.BallisticEnemyFinder>().sceneEnemies = this.sceneEnemies;
        Destroy(gameObject);
    }
}
