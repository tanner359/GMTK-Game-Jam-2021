using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    public Vector3 targetPostion;
    public Seat targetSeat;
    public bool isSeated;
    public SpriteRenderer happinessGauge;
    public float happiness = 100;

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
        else
        {
            if (!isWaiting)
            {
                StartCoroutine(HappinessDecay());
                isWaiting = true;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPostion, 0.01f);    
    }

    bool isWaiting;
    public IEnumerator HappinessDecay()
    {
        if(happiness == 0) { StopAllCoroutines(); targetPostion = BarManager.instance.exit.transform.position; }
        if(happiness == 100)
        {
            yield return new WaitForSeconds(5f);
        }
        yield return new WaitForSeconds(1f);
        happiness--;
        happinessGauge.color = Color.HSVToRGB(happiness/360.0f, 1.0f, 1.0f);
        StartCoroutine(HappinessDecay());
    }
}
