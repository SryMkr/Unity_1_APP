using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    GameObject spikeWall;
    // Start is called before the first frame update
    void Start()
    {
        spikeWall=GameObject.Find("SpikeWall");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, moveSpeed * Time.deltaTime, 0);
        if(transform.position.y > spikeWall.transform.position.y)
        {
            Destroy(gameObject);
            transform.parent.GetComponent <FloorManager> ().SpawnFloor();
        }


    }
}
