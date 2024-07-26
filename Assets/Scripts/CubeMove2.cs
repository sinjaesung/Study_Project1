using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove2 : MonoBehaviour
{
    private void Update()
    {
        Vector3 pos = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Debug.Log("movePos:" + pos);
    }
}
