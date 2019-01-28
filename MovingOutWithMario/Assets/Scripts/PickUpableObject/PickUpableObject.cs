using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpableObject : MonoBehaviour {

    public string nameToDisplay = "";
    public float Weight = 0;
    public int score = 0;
    public float timeToPickUp;
    [HideInInspector] public bool BeingPickedUp;
    // Use this for initialization
    void Start () {
        BeingPickedUp = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
