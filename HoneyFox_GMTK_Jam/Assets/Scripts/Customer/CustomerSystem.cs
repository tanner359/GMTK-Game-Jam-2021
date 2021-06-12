using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerSystem : MonoBehaviour
{
    public static CustomerSystem instance;

    public Customer[] customers;
    public GameObject defaultCustomer;
    public TMP_Text queueText;
    public int queue = 0;
    public int currentCustomers = 0;

    private void Awake()
    {
        instance = this;

        customers = Resources.LoadAll<Customer>("Customers");
    }
    private void Start()
    {
        SpawnCustomer(customers[Random.Range(0, customers.Length)]);
        currentCustomers++;
        StartCoroutine(StartQueue());
    }
    private void Update()
    {
        if(BarManager.instance.FindSeat() != null && queue > 0 && currentCustomers < BarManager.instance.seats.childCount)
        {
            SpawnCustomer(customers[Random.Range(0, customers.Length)]);
            currentCustomers++;
            queue--;
            UpdateQueueCount();
        }
    }
    public IEnumerator StartQueue()
    {
        yield return new WaitForSeconds(Random.Range(15f, 25f));
        queue++;
        UpdateQueueCount();
        StartCoroutine(StartQueue());
    }

    public void UpdateQueueCount()
    {
        if(queue == 0)
        {
            queueText.text = "";
            return;
        }
        queueText.text = "Waiting: " + queue;
    }

    public void SpawnCustomer(Customer customer)
    {
        GameObject newCustomer = Instantiate(customer.gameObject, BarManager.instance.door.position, Quaternion.identity);

        newCustomer.GetComponent<CustomerBehaviour>().customerID = customer;
    }
}
