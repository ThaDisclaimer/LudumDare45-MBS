using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public List<GameObject> walls;
    public List<GameObject> poles;
    public Dictionary<int,Box> Neighbors = new Dictionary<int, Box>();
    public List<Box> neighList;
    public int id;
    public Box maker;
    public bool deadEnd;

    // Start is called before the first frame update
    void Start()
    {
        findNeighbors();
    }

    public void findNeighbors()
    {
        int layerMask = 1 << 9;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, new Vector3(0, 0, 1.5f), out hit, 1.5f, layerMask) && (!Neighbors.ContainsKey(0) && !Neighbors.ContainsValue(hit.collider.GetComponent<Box>())))
        {
            Destroy(walls[0]);
            Neighbors.Add(0, hit.collider.GetComponent<Box>());
            Destroy(poles[0]);
            //Debug.Log(id + "New Neighbor!" + hit.collider.GetComponent<Box>().id);
        }
        if (Physics.Raycast(transform.position, new Vector3(1.5f, 0, 0), out hit, 1.5f, layerMask) && (!Neighbors.ContainsKey(1) && !Neighbors.ContainsValue(hit.collider.GetComponent<Box>())))
        {
            Destroy(walls[1]);
            Neighbors.Add(1, hit.collider.GetComponent<Box>());
            Destroy(poles[1]);
            //Debug.Log(id + "New Neighbor!" + hit.collider.GetComponent<Box>().id);
        }
        if (Physics.Raycast(transform.position, new Vector3(0, 0, -1.5f), out hit, 1.5f, layerMask) && (!Neighbors.ContainsKey(2) && !Neighbors.ContainsValue(hit.collider.GetComponent<Box>())))
        {
            Destroy(walls[2]);
            Neighbors.Add(2, hit.collider.GetComponent<Box>());
            Destroy(poles[2]);
            //Debug.Log(id+ "New Neighbor!" + hit.collider.GetComponent<Box>().id);
        }
        if (Physics.Raycast(transform.position, new Vector3(-1.5f, 0, 0), out hit, 1.5f, layerMask) && (!Neighbors.ContainsKey(3) && !Neighbors.ContainsValue(hit.collider.GetComponent<Box>())))
        {
            Destroy(walls[3]);
            Neighbors.Add(3, hit.collider.GetComponent<Box>());
            Destroy(poles[3]);
            //Debug.Log(id + "New Neighbor!" + hit.collider.GetComponent<Box>().id);
        }
        neighList.Clear();
        for (int i = 0; i < 4; i++)
        {
            if (Neighbors.ContainsKey(i))
            {
                neighList.Add(Neighbors[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        neighList.Clear();
        for (int i = 0; i < 4; i++)
        {
            if (Neighbors.ContainsKey(i))
            {
                neighList.Add(Neighbors[i]);
            }
        }
    }
}
