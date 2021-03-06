using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Customer")]
public class Customer : ScriptableObject
{
    public GameObject gameObject;
    public string enterDialog;
    public string orderDialog;
    public string angryExit;
    public string happyExit;
    public Drink drink;
}
