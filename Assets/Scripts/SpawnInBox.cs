using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInBox : MonoBehaviour
{
    public GameObject playerPrefab;

    Collider boxCollider;
    Vector3 colliderSize;
    Vector3 colliderCenter;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        colliderCenter = boxCollider.transform.position;

        colliderSize.x = GetComponent<BoxCollider>().size.x;
        colliderSize.y = GetComponent<BoxCollider>().size.y;
        colliderSize.z = GetComponent<BoxCollider>().size.z;

        Debug.Log("Collider centered at " + colliderCenter.x + "," + colliderCenter.y + "," + colliderCenter.z);

        // Multiply by scale because it does affect the size of the collider
        //colliderSize.x = boxCollider.transform.localScale.x * GetComponent<BoxCollider>().size.x;
        //colliderSize.y = boxCollider.transform.localScale.y * GetComponent<BoxCollider>().size.y;
        //colliderSize.z = boxCollider.transform.localScale.z * GetComponent<BoxCollider>().size.z;

        Debug.Log("Collider size: " + colliderSize.x + "," + colliderSize.y + "," + colliderSize.z);
    }


    private void Start()
    {
        Instantiate(playerPrefab, GetRandomPosition(), Quaternion.identity);
    }


    private Vector3 GetRandomPosition()
    {
        // You can also take off half the bounds of the thing you want in the box, so it doesn't extend outside.
        // Right now, the center of the prefab could be right on the extents of the box
        Vector3 randomPosition = new Vector3(Random.Range(-colliderSize.x / 2, colliderSize.x / 2), colliderCenter.y, Random.Range(-colliderSize.z / 2, colliderSize.z / 2));
        Debug.Log("Spawning player at " + randomPosition.x + "," + randomPosition.y + "," + randomPosition.z);
        return colliderCenter + randomPosition;
    }
}

