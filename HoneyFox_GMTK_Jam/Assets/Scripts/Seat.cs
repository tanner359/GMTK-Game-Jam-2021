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
        collision.GetComponent<CustomerBehaviour>().isSeated = true;
        isAvailable = false;
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<CustomerBehaviour>().isSeated = false;
        isAvailable = true;
        GetComponent<SpriteRenderer>().color = originColor;
    }
}
