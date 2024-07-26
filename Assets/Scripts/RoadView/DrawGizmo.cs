using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmo : MonoBehaviour
{
    [SerializeField] private float radius = 0.3f;

    [SerializeField] private bool isTestMode = false;
    private void OnDrawGizmos()
    {
        if(isTestMode == true)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, radius);
        }   
    }
}
