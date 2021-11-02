using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_on_Click : MonoBehaviour
{
    public GameObject spawned_Item;

    Click_and_Draggable cnd_Ref;

    private void OnMouseDown()
    {
        GameObject spawned = GameObject.Instantiate(spawned_Item, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        spawned.transform.position = new Vector3(spawned.transform.position.x, spawned.transform.position.y, 0f);
        
        cnd_Ref = spawned.GetComponent<Click_and_Draggable>();
        cnd_Ref.OnMouseDown();
    }

    private void OnMouseDrag()
    {
        cnd_Ref.OnMouseDrag();
    }

    private void OnMouseUp()
    {
        cnd_Ref.OnMouseUp();
    }
}
