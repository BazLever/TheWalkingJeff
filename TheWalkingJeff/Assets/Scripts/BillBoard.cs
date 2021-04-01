using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDir = (playerPos.position - transform.position);
        transform.eulerAngles = new Vector3(0f, Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg, 0f);
    }
}
