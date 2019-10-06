using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Box position;
    public Weapon weapon;
    

    // Start is called before the first frame update
    void Start()
    {
        MoveToTile();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward*3, Color.blue);
        Dictionary<int,Box> TilesAvailable = CheckNextTile();
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (TilesAvailable.ContainsKey(0))
            {
                Debug.Log("Going Forth");
                position = TilesAvailable[0];
                MoveToTile();
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (TilesAvailable.ContainsKey(1))
            {
                Debug.Log("Going Back");
                position = TilesAvailable[1];
                MoveToTile();
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(0, -90, 0, Space.World);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(0, 90, 0, Space.World);
        }
        if (Input.GetButton("Fire1"))
        {
            weapon.FireCommand();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public Dictionary<int,Box> CheckNextTile()
    {
        int layerMask = 1 << 9;
        Dictionary<int, Box> BackForth = new Dictionary<int, Box>();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1.5f, layerMask))
        {
            
            BackForth.Add(0,hit.collider.GetComponent<Box>());
        }
        if (Physics.Raycast(transform.position, -transform.forward, out hit, 1.5f, layerMask))
        {
            
            BackForth.Add(1,hit.collider.GetComponent<Box>());
        }
        return BackForth;
    }

    public void MoveToTile()
    {
        transform.position = position.transform.position + new Vector3(0, 1, 0);
    }
}
