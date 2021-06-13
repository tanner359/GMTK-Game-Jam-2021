using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CustomerBehaviour : MonoBehaviour
{
    public Customer customerID;

    public enum ActionState {entering, ordering, waiting, leaving};

    public ActionState actionState = ActionState.entering;
    public Vector3 targetPostion;
    public Seat targetSeat;
    public bool isSeated;
    public ParticleSystemRenderer happinessParticles;
    public float happiness = 100;
    public TMP_Text dialogBox;
    public GameObject dialogBackround;
    public GameObject actionButton;
    public Animator animator;

    [Header("Materials")]
    public Material angry;
    public Material happy;
    public Material neutral;

    private void Start()
    {
        targetSeat = BarManager.instance.FindSeat();
        targetSeat.isAvailable = false;
        targetPostion = targetSeat.gameObject.transform.position;
        GetComponent<SpriteRenderer>().sortingOrder = targetSeat.GetComponent<SpriteRenderer>().sortingOrder + 1;
    }

    private void Update()
    {
        switch (actionState)
        {
            case ActionState.entering:
                animator.SetBool("Sitting", false);
                break;

            case ActionState.leaving:
                targetPostion = BarManager.instance.exit.transform.position;
                animator.SetBool("Sitting", false);
                targetSeat.isAvailable = true;
                actionButton.SetActive(false);
                GetComponent<SpriteRenderer>().flipX = true;
                break;

            case ActionState.waiting:
                animator.SetBool("Sitting", true);
                actionButton.SetActive(true);
                actionButton.GetComponentInChildren<TMP_Text>().text = "Serve";
                break;

            case ActionState.ordering:
                animator.SetBool("Sitting", true);
                actionButton.SetActive(true);
                actionButton.GetComponentInChildren<TMP_Text>().text = "Take Order";
                break;
        }

        if (actionState == ActionState.entering && isSeated)
        {
            StartCoroutine(HappinessDecay());
            actionState = ActionState.ordering;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPostion, 0.01f);    
    }

    #region Collision Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Seat>() == targetSeat)
        {
            StopCoroutine(Dialog(customerID.enterDialog));
            StartCoroutine(Dialog(customerID.enterDialog));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Seat>() == targetSeat)
        {
            if (happiness < 50)
            {
                StopCoroutine(Dialog(customerID.angryExit));
                StartCoroutine(Dialog(customerID.angryExit));
                return;
            }
            StopCoroutine(Dialog(customerID.happyExit));
            StartCoroutine(Dialog(customerID.happyExit));
        }
    }

    #endregion

    #region Coroutines
    public IEnumerator Dialog(string dialog)
    {
        dialogBackround.SetActive(true);
        dialogBox.text = dialog;
        yield return new WaitForSeconds(6f);
        dialogBackround.SetActive(false);
        dialogBox.text = "";
    }
    public IEnumerator HappinessDecay()
    {
        if(happiness == 0) {
            happinessParticles.material = angry;         
            StartCoroutine(Dialog(customerID.angryExit));          
            actionState = ActionState.leaving;
            TicketRack.instance.RemoveTicket(customerID.drink);
            StopCoroutine(HappinessDecay());
        }
        else if(happiness < 66 && happiness > 33)
        {
            happinessParticles.material = neutral;
        }
        else if (happiness < 33)
        {
            happinessParticles.material = angry;
        }

        if (happiness == 100)
        {
            happinessParticles.material = happy;
            yield return new WaitForSeconds(5f);
        }
        yield return new WaitForSeconds(1f);
        happiness--;
        StartCoroutine(HappinessDecay());
    }
    #endregion

    #region Functions

    public void CustomerAction()
    {
        switch (actionState)
        {
            default:
                break;

            case ActionState.ordering:
                Bartender.instance.AddOrder(customerID.drink);
                actionState = ActionState.waiting;
                break;

            case ActionState.waiting:
                if (Bartender.instance.currentDrink != null)
                {
                    int score = CalculateScore(Bartender.instance.currentDrink, (int)happiness);
                    Bartender.instance.AddScore(score);
                    Bartender.instance.currentDrink = null;
                    actionState = ActionState.leaving;
                    TicketRack.instance.RemoveTicket(customerID.drink);
                    break;
                }
                break;
        }       
    }

    public int CalculateScore(Recipe drink, int happiness)
    {
        int score = 0;
        int bonus = 0;

        for (int i = 0; i < drink.ingredients.Count; i++)
        {
            Ingredient currentIngredient = drink.ingredients[i];
            for (int k = 0; k < customerID.drink.recipe.ingredients.Count; k++)
            {
                if (currentIngredient == customerID.drink.recipe.ingredients[k])
                {
                    score += 10;
                    k = 9999;
                }
            }            
        }

        bonus = (int)((happiness * 0.01f) * score);

        return score + bonus;  
    }

    #endregion

}
