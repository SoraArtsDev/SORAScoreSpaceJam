using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float fSpeed = 60.0f;

    public GameObject ImapctEffect;
    public void Seek(Transform a_target)
    {
        target = a_target;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float fDistance = fSpeed * Time.deltaTime;

        if (direction.magnitude <= fDistance)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * fDistance, Space.World);
    }

    void HitTarget()
    {
        GameObject effectInstance = (GameObject)Instantiate(ImapctEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2.0f);

        Destroy(target.gameObject);

        Destroy(gameObject);
    }
}
