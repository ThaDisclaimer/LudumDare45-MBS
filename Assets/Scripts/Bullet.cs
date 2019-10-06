using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Rigidbody rb;
    public float speed;
    public GameObject explo;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * speed,ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        int layerMask = 1 << 12;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.5f, layerMask);
        if (hitColliders.Length>0)
        {
            foreach (Collider c in hitColliders)
            {
                c.GetComponent<Blob>().Removal();
            }
        }
        GameObject e = Instantiate(explo,transform);
        e.transform.SetParent(null);
        Destroy(this.gameObject);
        Debug.Log(collision.collider.name);
        
    }
}
