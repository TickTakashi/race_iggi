using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    //// 0 = spawn, 1 = top, 2 = bottom, 3 = left, 4 = right
    //private int exitDirection;

    //private Vector3Int startLocation = new Vector3Int(0,0,0);
    //private Vector3Int endLocation;

    public int maxTiles;

    private Vector3Int prevLocation = new Vector3Int(0,0,0);
    private Vector3Int nextLocation;
    private int randDirection;

    private bool movedLeft;
    private bool movedRight;
    private bool movedForward;

    public GameObject[] topExitTiles;
    public GameObject[] bottomExitTiles;
    public GameObject[] leftExitTiles;
    public GameObject[] rightExitTiles;
    

    void Start()
    {
        //endLocation = new Vector3(Random.Range(5, 10), Random.Range(5, 10), 0);
        //options = GameObject.FindGameObjectWithTag("tiles").GetComponent<TileOptions>();
        Invoke("Spawn", 1.0f);
    }

    // Update is called once per frame
    void Spawn()
    {

        for (int i = 0; i < maxTiles; i++)  
        {
            if (movedForward == true)
            {
                int[] validOptions = new int[3] { 1, 2, 3 };
                randDirection = validOptions[Random.Range(0, validOptions.Length)];
            }

            else if (movedLeft == true)
            {
                int[] validOptions = new int[2] { 1, 2 };
                randDirection = validOptions[Random.Range(0, validOptions.Length)];
            }

            else if (movedRight == true)
            {
                int[] validOptions = new int[2] { 1, 3 };
                randDirection = validOptions[Random.Range(0, validOptions.Length)];
            }

            else
            {
                int[] validOptions = new int[3] { 1, 2, 3 };
                randDirection = validOptions[Random.Range(0, validOptions.Length)];
            }
      

            // Generate tile forward
            if (randDirection == 1)
            {
                int randTile = Random.Range(0, topExitTiles.Length);

                prevLocation = nextLocation;
                nextLocation = new Vector3Int(nextLocation.x + 1, nextLocation.y, nextLocation.z);

                Instantiate(rightExitTiles[randTile], nextLocation, rightExitTiles[randTile].transform.rotation);

                movedForward = true; movedLeft = false; movedRight = false;
            }

            // Generate tile to the left
            if (randDirection == 2)
            {
                int randTile = Random.Range(0, leftExitTiles.Length);

                prevLocation = nextLocation;
                nextLocation = new Vector3Int(nextLocation.x, nextLocation.y, nextLocation.z - 1);

                Instantiate(rightExitTiles[randTile], nextLocation, rightExitTiles[randTile].transform.rotation);

                movedForward = false; movedLeft = true; movedRight = false;
            }

            // Generate tile to the right
            if (randDirection == 3)
            {
                int randTile = Random.Range(0, rightExitTiles.Length);

                prevLocation = nextLocation;
                nextLocation = new Vector3Int(nextLocation.x, nextLocation.y, nextLocation.z + 1);

                Instantiate(leftExitTiles[randTile], nextLocation, leftExitTiles[randTile].transform.rotation);

                movedForward = false; movedLeft = false; movedRight = true;
            }
            Debug.Log("Spawning tile number " + (i+1) + " at " + nextLocation.x + "," + nextLocation.z);
        }

        //endLocation = new Vector3Int(Random.Range(5, 10), Random.Range(-3, 3), 0);

        //Debug.Log("Trying to get to " + endLocation.x + "," + endLocation.y);

        //int i = 0;
        //int j = 0;

        //while (i != endLocation.x && j != endLocation.y)
        //{
        //    // 1 = forward, 2 = left, 3 = right
        //    int randDirection = Random.Range(1, 4);
        //    Debug.Log("hi mom, i = " + i + "and j = " + j);

        //    if (randDirection == 1)
        //    {
        //        int randTile = Random.Range(0, topExitTiles.Length);
        //        Instantiate(topExitTiles[randTile], new Vector3Int(i + 1, j, 0), topExitTiles[randTile].transform.rotation);
        //        Debug.Log("Spawning tile at " + (i + 1) + "," + j);
        //    }

        //    if (randDirection == 2)
        //    {
        //        int randTile = Random.Range(0, leftExitTiles.Length);
        //        Instantiate(leftExitTiles[randTile], new Vector3Int(i, j-1, 0), rightExitTiles[randTile].transform.rotation);
        //        Debug.Log("Spawning tile at " + i + "," + (j-1));
        //    }

        //    if (randDirection == 3)
        //    {
        //        int randTile = Random.Range(0, rightExitTiles.Length);
        //        Instantiate(rightExitTiles[randTile], new Vector3Int(i, j+1, 0), rightExitTiles[randTile].transform.rotation);
        //        Debug.Log("Spawning tile at " + i + "," + (j + 1));
        //    }

        //    i++; j++;
    

        //if (exitDirection == 1)
        //{
        //    rand = Random.Range(0, options.topExitTiles.Length);
        //    Instantiate(options.topExitTiles[rand], transform.position, options.topExitTiles[rand].transform.rotation);
        //}

        //if (exitDirection == 2)
        //{
        //    rand = Random.Range(0, options.bottomExitTiles.Length);
        //    Instantiate(options.bottomExitTiles[rand], transform.position, options.bottomExitTiles[rand].transform.rotation);
        //}

        //if (exitDirection == 3)
        //{
        //    rand = Random.Range(0, options.leftExitTiles.Length);
        //    Instantiate(options.leftExitTiles[rand], transform.position, options.leftExitTiles[rand].transform.rotation);
        //}

        //if (exitDirection == 4)
        //{
        //    rand = Random.Range(0, options.rightExitTiles.Length);
        //    Instantiate(options.rightExitTiles[rand], transform.position, options.rightExitTiles[rand].transform.rotation);
        //}
    }
}
