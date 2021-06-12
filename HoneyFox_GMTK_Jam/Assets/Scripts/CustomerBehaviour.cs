using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    public Vector3 targetPostion;
    public Seat targetSeat;
    public bool isSeated;

    private void Start()
    {
        targetSeat = BarManager.instance.FindSeat();
        Debug.Log(targetSeat);
        targetPostion = targetSeat.gameObject.transform.position;    
    }

    private void Update()
    {
        if (!isSeated)
        {
            if (BarManager.instance.FindSeat() != null && targetSeat.GetComponent<Seat>().isAvailable == false)
            {
                targetSeat = BarManager.instance.FindSeat();
                targetPostion = targetSeat.transform.position;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPostion, 0.01f);    
    }
}
