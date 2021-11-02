using UnityEngine;

public class Seat : MonoBehaviour
{
    public bool isAvailable;
    Color originColor;

    private void Start()
    {
        originColor = GetComponent<SpriteRenderer>().color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CustomerBehaviour>().targetSeat == this)
        {
            collision.GetComponent<CustomerBehaviour>().isSeated = true;
            GetComponent<SpriteRenderer>().color = Color.green;
        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<CustomerBehaviour>().targetSeat == this)
        {
            collision.GetComponent<CustomerBehaviour>().isSeated = false;
            GetComponent<SpriteRenderer>().color = originColor;
        }       
    }
}
