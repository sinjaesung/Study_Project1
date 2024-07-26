using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    private Vector3 right = new Vector3(1, 0, 0);
    private Vector3 left = new Vector3(-1, 0, 0);
    private Vector3 forward = new Vector3(0, 0, 1);
    private Vector3 back = new Vector3(0, 0, -1);

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += left * (Time.deltaTime * 10f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += right * (Time.deltaTime * 10f);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += forward * (Time.deltaTime * 10f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += back * (Time.deltaTime * 10f);
        }
    }
}
