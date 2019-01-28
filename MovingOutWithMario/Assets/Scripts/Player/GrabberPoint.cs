using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberPoint : MonoBehaviour {

    [HideInInspector] public PickUpableObject objectToPickUp;

    [HideInInspector] public Player player;
    [HideInInspector] public bool canPickUp;
    // Use this for initialization
    void Start () {
        canPickUp = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PickUpObject()
    {
        if (objectToPickUp != null)
            Destroy(objectToPickUp.gameObject);
        objectToPickUp = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PickUpableObject"))
        {
            if (objectToPickUp == null)
                objectToPickUp = collision.gameObject.GetComponent<PickUpableObject>();

            if (!objectToPickUp.BeingPickedUp)
            {
                canPickUp = true;
                objectToPickUp.BeingPickedUp = true;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PickUpableObject"))
        {

            if (objectToPickUp != null)
            {
                objectToPickUp.BeingPickedUp = false;
                objectToPickUp = null;
            }
            canPickUp = false;

        }
    }

   
}
