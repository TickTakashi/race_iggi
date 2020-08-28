using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInBox : MonoBehaviour
{
    public GameObject playerPrefab;

    Vector3 colliderSize;
    Vector3 colliderCenter;

    private void Awake()
    {
        Transform boxCollider = GetComponent<Transform>();
        colliderCenter = boxCollider.position;

        // Multiply by scale because it does affect the size of the collider
        colliderSize.x = boxCollider.localScale.x * GetComponent<BoxCollider>().size.x;
        colliderSize.y = boxCollider.localScale.y * GetComponent<BoxCollider>().size.y;
        colliderSize.z = boxCollider.localScale.y * GetComponent<BoxCollider>().size.y;
    }


    private void Start()
    {
        Instantiate(playerPrefab, GetRandomPosition(), Quaternion.identity);
    }


    private Vector3 GetRandomPosition()
    {
        // You can also take off half the bounds of the thing you want in the box, so it doesn't extend outside.
        // Right now, the center of the prefab could be right on the extents of the box
        Vector3 randomPosition = new Vector3(Random.Range(-colliderSize.x / 2, colliderSize.x / 2), 2, Random.Range(-colliderSize.y / 2, colliderSize.y / 2));

        return colliderCenter + randomPosition;
    }
}

