using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMaker : MonoBehaviour
{
    public Box tile;
    public Box boxStart;
    public List<Box> tiles;
    public float step;
    public int currProgress;
    public int tileIdCount;
    public int tileLimit;
    public List<Box> DeadEnds;
    public Blob blobSpore;
    public List<Blob> blobs;
    public Texture2D cursorThingy;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorThingy,Vector2.zero,CursorMode.Auto);
        boxStart.id = tileIdCount;
        boxStart.findNeighbors();
        tileIdCount++;
        MakeRooms(boxStart);
        for(int i=0;i< tileLimit; i++)
        {
            MakeRooms(tiles[i]);
        }
        StartCoroutine("FindDeadEnds");
        
    }

    IEnumerator FindDeadEnds()
    {
        yield return new WaitForSeconds(1);
        foreach (Box b in tiles)
        {
            b.findNeighbors();
            //Debug.Log(b.neighList.Count);
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                if (b.Neighbors.ContainsKey(i))
                {
                    count++;
                }
            }
            Debug.Log(count);
            if (count == 1)
            {
                Blob s = Instantiate(blobSpore, b.transform);
                s.transform.position = b.transform.position + Vector3.forward;
                s.transform.SetParent(null);
                s.father = b.gameObject;
                s.allfather = this;
                s.Spread();
                s = Instantiate(blobSpore, b.transform);
                s.transform.position = b.transform.position + Vector3.forward;
                s.transform.SetParent(null);
                s.father = b.gameObject;
                s.allfather = this;
                s.Spread();

            }
        }
    }

    public void MakeRooms(Box zero)
    {
        int neighborsMade = 0;
        for (int i = 0; i < 4; i++)
        {
            if (!zero.Neighbors.ContainsKey(i))
            {
                if (Random.Range(0, 10) >= 4)
                {
                    MakeBox(i, zero);
                    neighborsMade++;
                }
            }
        }
        if(tiles.Count < 10 && !zero.Neighbors.ContainsKey(0) && zero == boxStart)
        {
            MakeBox(0, zero);
            neighborsMade++;
        }
    }

    public void MakeBox(int i,Box zero)
    {
        Vector3 offset = zero.transform.position;
        Box tempBox = Instantiate(tile);
        tempBox.id = tileIdCount;
        
        if (i == 0)
        {
            offset.z += step;
            zero.walls[i].SetActive(false);
            zero.poles[i].SetActive(false);
        }
        if (i == 1)
        {
            offset.x += step;
            zero.walls[i].SetActive(false);
            zero.poles[i].SetActive(false);
        }
        if (i == 2)
        {
            offset.z -= step;
            zero.walls[i].SetActive(false);
            zero.poles[i].SetActive(false);
        }
        if (i == 3)
        {
            offset.x -= step;
            zero.walls[i].SetActive(false);
            zero.poles[i].SetActive(false);
        }
        tempBox.transform.position = offset;
        tempBox.name = "Tile" + tileIdCount;
        if (checkDuplicate(tempBox))
        {
            Destroy(tempBox.gameObject);
            Debug.Log("Duplicate killed! " + tempBox.name);
            return;
        }
        else
        {


            tempBox.findNeighbors();

            tempBox.maker = zero;
            tileIdCount++;
            tiles.Add(tempBox);
            zero.Neighbors.Add(i, tempBox);
        }
    }

    public bool checkDuplicate(Box testBox)
    {
        int layerMask = 1 << 9;
        Collider[] hitColliders = Physics.OverlapSphere(testBox.transform.position, 0.5f, layerMask);
        if (hitColliders.Length > 0)
        {
            if (hitColliders[0].GetComponent<Box>())
            {

                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
