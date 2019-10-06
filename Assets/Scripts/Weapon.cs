using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player PC;
    public RaycastHit hit;

    public Bullet projectile;
    public float spreadX;
    public float spreadY;
    public bool isFiring;
    public bool isCocking;
    public bool isReloading;
    public int numProjectiles;
    public int clipSize;
    public float fireSpeed;
    public Transform muzzle;
    public GameObject muzzleFlash;
    public float cockTime;
    public float cocker;

    void Start()
    {
        
    }

    public void FireCommand()
    {
        if (!isCocking && !isReloading)
        {
            Fire();
        }
    }

    public void Fire()
    {
        for (int i = 0; i < numProjectiles; i++)
        {
            Bullet b = Instantiate(projectile, muzzle);
            b.transform.SetParent(null);
            GameObject m = Instantiate(muzzleFlash, muzzle);
            m.transform.SetParent(null);
            b.transform.Rotate(Random.Range(-spreadX, spreadX), Random.Range(-spreadY, spreadY), 0);
        }
        isCocking = true;
        cocker = 0;
    }

    void Update()
    {
        if (isCocking)
        {
            cocker += fireSpeed;
            if (cocker >= cockTime)
            {
                isCocking = false;
            }
        }
        //Debug.Log(Input.mousePosition);
        Vector3 pos = Input.mousePosition;
        pos.z = 0.1f;
        pos = Camera.main.ScreenToWorldPoint(pos);
        int layerMask = 1 << 9;
        int inverse = ~(layerMask);
        
        Physics.Raycast(pos, pos-PC.transform.position, out hit, Mathf.Infinity, inverse);
        if (hit.point!=Vector3.zero)
        {
            transform.LookAt(hit.point);
        }
        else
        {
            transform.LookAt(pos);
        }
        // Start is called before the first frame update
    }

    

}
