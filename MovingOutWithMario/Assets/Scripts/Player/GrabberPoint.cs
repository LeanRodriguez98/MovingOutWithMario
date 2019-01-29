using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberPoint : MonoBehaviour {

    [HideInInspector] public PickUpableObject objectToPickUp;

    [HideInInspector] public Player player;
    [HideInInspector] public bool canPickUp;
    [HideInInspector] public bool canDrop;
    // Use this for initialization
    void Start () {
        canPickUp = false;
        canDrop = false;
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
                player.playerUI.EnableInfoPanel(true, objectToPickUp);
            }
        }

        if (collision.gameObject.CompareTag("Truck"))
        {
            if (collision.gameObject == player.truckToCharge.gameObject)
            {
                canDrop = true;
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
                player.playerUI.EnableInfoPanel(false, null);

            }
            canPickUp = false;

        }

        if (collision.gameObject.CompareTag("Truck"))
        {
            if (collision.gameObject == player.truckToCharge.gameObject)
            {
                canDrop = false;
            }
        }
    }

   
}
