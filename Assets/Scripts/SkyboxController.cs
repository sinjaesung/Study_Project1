using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{

    [SerializeField] private Material dayMaterial;
    [SerializeField] private Material nightMaterial;
    [SerializeField] private Material[] materiLlist;
    [SerializeField] private float elapsedTime = 0;

    [SerializeField] float rotateValue = 0;
    [SerializeField] float rotateSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = dayMaterial;
    }

   /* public void NightEvent()
    {
        RenderSettings.skybox = nightMaterial;
    }

    public void DayEvent()
    {
        RenderSettings.skybox = dayMaterial;
    }*/

    public void RandomEvent()
    {
        int randomIndex = Random.Range(0, materiLlist.Length);
        Debug.Log("적용할 랜덤 메터리얼:" +( randomIndex + "/" + (materiLlist.Length - 1)));
        if (materiLlist[randomIndex])
        {
            RenderSettings.skybox = materiLlist[randomIndex];
        }
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;

        rotateValue = elapsedTime * rotateSpeed;

        RenderSettings.skybox.SetFloat("_Rotation", rotateValue);
        if(rotateValue >= 360f)
        {
            elapsedTime = 0;
        }
    }
}
