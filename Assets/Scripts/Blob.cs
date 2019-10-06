using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    public float sexSpeed;
    public float growth;
    public float growthSpeed;
    public float growthLimit;
    public bool isLatched = false;
    public bool isMature = false;
    public bool isReproduced = false;
    public float fuse;
    public Blob spore;
    public int blobLimit;
    public Vector3 spreadDirection;
    public float tick;
    public float tickLimit;
    public GameObject father;
    public MazeMaker allfather;
    public GameObject explo;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SelfDestruct");
    }

    public void Spread()
    {
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        isLatched = false;
        isMature = false;
        isReproduced = false;
        growth = 0;
        int layerMask = 1 << 9;
        int inverse = ~(layerMask);
        RaycastHit hit;
        if (spreadDirection == Vector3.zero)
        {
            spreadDirection = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        }
        //Debug.Log("pos"+transform.position);
        //Debug.Log("dir"+spreadDirection);
        Physics.Raycast(transform.position, spreadDirection, out hit, 3, inverse);
        Debug.DrawRay(transform.position, spreadDirection, Color.green, 3);
        if (hit.point != Vector3.zero)
        {
            transform.position = hit.point;
            allfather.blobs.Add(this);
            isLatched = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLatched && !isMature)
        {
            growth += growthSpeed;
            Vector3 tempScale = new Vector3(growth, growth, growth);
            transform.localScale = tempScale;
            if (growth > growthLimit)
            {
                isMature = true;
            }
        }
        if (isMature && !isReproduced && allfather.blobs.Count< blobLimit)
        {
            isReproduced = true;
            
            for (int i = 0; i < 5; i++)
            {
                Blob b = Instantiate(spore, transform);
                Mesh m = b.GetComponent<MeshFilter>().mesh;

                int vertice = Random.Range(0, 100);
                if (m.vertices.Length < vertice)
                {
                    vertice = m.vertices.Length;
                }
                b.transform.SetParent(null);
                b.transform.position = transform.position+ m.vertices[vertice]/2;
                b.spreadDirection = m.normals[vertice];

                b.father = gameObject;

                tick = 0;
                b.allfather = allfather;
                b.Spread();
                
                
            }
        }
        if (isReproduced)
        {
            tick += sexSpeed;
            if (tick > tickLimit)
            {
                isReproduced = false;
            }
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(fuse);
        Removal();
    }

    public void Removal()
    {
        GameObject e = Instantiate(explo, transform);
        e.transform.SetParent(null);
        allfather.blobs.Remove(this);
        Destroy(this.gameObject);
    }
}
