using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Control : MonoBehaviour
{
    public GameObject barCam;
    public GameObject stationCam;

    public void MovetoStation(GameObject targetUI)
    {
        targetUI.SetActive(false);
        stationCam.SetActive(true);
        barCam.SetActive(false);
    }
    public void MovetoBar(GameObject targetUI)
    {
        StartCoroutine(EnableDelay(targetUI));
    }

    public IEnumerator EnableDelay(GameObject targetUI)
    {
        stationCam.SetActive(false);
        barCam.SetActive(true);
        yield return new WaitForSeconds(2);
        targetUI.SetActive(true);
    }
}
