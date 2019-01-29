using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour {

    private List<PickUpableObject> objectsList;
	// Use this for initialization
	void Start () {
        objectsList = new List<PickUpableObject>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddObjeetToTruck(PickUpableObject obj)
    {
        objectsList.Add(obj);
    }
}
