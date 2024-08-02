using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCube : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;

    private void Update()
    {
        float posX = Input.GetAxis("Horizontal");
        float posY = Input.GetAxis("Vertical");

        Vector3 pos = new Vector3(posX, 0, posY);
        transform.Translate(pos * Time.deltaTime * moveSpeed);
    }
}
