using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] GameObject[] FloorPrefab;
    GameObject leftWall;
    GameObject spikeWall;
    float leftWallX;
    float rightWallX;
    void Start()
    {
        leftWall = GameObject.Find("LeftWall");
        spikeWall = GameObject.Find("SpikeWall");
        leftWallX = leftWall.transform.position.x;
        rightWallX = -leftWallX;
    }

    public void SpawnFloor()
    {
        int r = Random.Range(0, FloorPrefab.Length);
        GameObject floor = Instantiate(FloorPrefab[r], transform);
        floor.transform.position = new Vector3(Random.Range(leftWallX, rightWallX), -spikeWall.transform.position.y, 0);

    }


}
