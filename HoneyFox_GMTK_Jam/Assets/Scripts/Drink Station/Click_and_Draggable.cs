using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Click_and_Draggable : MonoBehaviour
{
    // Core Components
    Transform obj_Transform;
    Rigidbody2D rb;
    SpringJoint2D spring_Joint;
    public Ingredient ingredient;

    bool active;

    // Crafting Parameters
    bool added;
    public bool consumed_On_Use;
    public bool is_Loose;
    public bool clink;

    // Resetting Position Parameters
    Vector2 home_Position;
    Vector2 last_Home;

    float t;
    bool reset = true;

    // FX Components
    AudioSource clink_Source;

    // Start is called before the first frame update
    void Awake()
    {
        // Get the transform and rigidbody of the object
        obj_Transform = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        // Create a Spring Joint component
        spring_Joint = gameObject.AddComponent(typeof(SpringJoint2D)) as SpringJoint2D;

        // Set Spring Joint parameters
        spring_Joint.enableCollision = true;
        spring_Joint.connectedAnchor = gameObject.transform.position;
        spring_Joint.autoConfigureDistance = false;
        spring_Joint.distance = 0.005f;
        spring_Joint.frequency = 2.5f;
        spring_Joint.enabled = false;

        // Set Rigidbody parameters
        rb.drag = 1f;
        rb.angularDrag = 0.05f;
        rb.gravityScale = 0f;

        // Set the home position for the object to return to once dropped
        if (ingredient != null)
            ingredient.on_Platform = false;
        
        home_Position = transform.position;
        last_Home = home_Position;

        // Get audio source
        clink_Source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Keep rotation at zero while not being dragged
        if (reset)
        {
            transform.position = home_Position;
            transform.rotation = Quaternion.identity;
        }

        if (ingredient != null)
        {
            if (ingredient.on_Platform && home_Position == last_Home)
            {
                home_Position = (Vector2)GameObject.FindGameObjectWithTag("Base Pos").transform.position + new Vector2(0f, GetComponent<BoxCollider2D>().size.y / 2f);
                StartCoroutine(Reset_Item());
            }
        }
    }

    public void OnMouseDown()
    {
        // Turn off reset
        reset = false;
        active = true;
        GetComponent<BoxCollider2D>().isTrigger = true;

        // Get z rotation of object to correct the position of the anchor
        float rotation = obj_Transform.eulerAngles.z;

        // Convert rotation to radians and calculate correction
        rotation = rotation * ((2f * Mathf.PI)/360f);
        Vector2 correction = new Vector2(Mathf.Cos(2f * rotation + Mathf.PI / 4f) * Mathf.Sqrt (2f),
                                         Mathf.Cos(2f * rotation - Mathf.PI / 4f) * Mathf.Sqrt (2f));
        
        // Enable the spring joint
        spring_Joint.enabled = true;

        // Get the current cursor position and calculate the corrected position
        Vector2 cursor_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spring_Joint.anchor = Vector2.Scale((cursor_Pos - rb.position), correction);

        // Adjust rigidbody for cleaner dragging
        rb.drag = 10f;
        rb.angularDrag = 5f;
    }

    public void OnMouseDrag()
    {
        // If an object is being dragged...
        if (spring_Joint.enabled)
        {
            // Get the cursor position
            Vector2 cursor_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Set the connected anchor to the cursor position
            spring_Joint.connectedAnchor = cursor_Pos;
            spring_Joint.distance = 0.005f;
        }
    }

    public void OnMouseUp()
    {
        // Disable the spring when releasing the object
        spring_Joint.enabled = false;
        GetComponent<BoxCollider2D>().isTrigger = false;

        // Set this object to be inactive
        active = false;

        // Reset rigidbody parameters
        rb.drag = 1f;
        rb.angularDrag = 0.05f;

        // Reset the item's position
        if (!is_Loose)
            StartCoroutine(Reset_Item());
        else
            rb.gravityScale = 3f;
    }

    public void Full_Reset()
    {
        home_Position = last_Home;
        GetComponent<Animator>().SetTrigger("Empty");
        StartCoroutine(Reset_Item());
    }

    IEnumerator Reset_Item()
    {
        yield return new WaitForEndOfFrame();

        // Smoothly interpolate the item back to its home position
        t = 0.0f;

        while (t <= 1.0f && !active)
        {
            t += Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, home_Position, Mathf.SmoothStep(0.0f, 1.0f, t));
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Mathf.SmoothStep(0.0f, 1.0f, t));
            
            if (Vector2.Distance(transform.position, home_Position) < 0.05f || active)
            {
                reset = true;
                yield break;
            }
            else
                yield return new WaitForEndOfFrame();
        }

        reset = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        clink_Source.Play();
    }
}
