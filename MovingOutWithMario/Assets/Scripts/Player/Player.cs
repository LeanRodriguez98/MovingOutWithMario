using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

 

    [System.Serializable]
    public struct InputData
    {
        public string horizontalAxis;
        public string verticalAxis;
        public string actionButon;
    }

    private InputData inputDataPlayer1;
    private InputData inputDataPlayer2;


    public enum PlayerNumber
    {
        PLAYER_1,
        PLAYER_2
    }

    [System.Serializable]
    public struct PlayerData
    {
        public PlayerNumber playerNumber;
        public float movementSpeed;
        public float force;
        [HideInInspector] public string horizontalAxis;
        [HideInInspector] public string verticalAxis;
        [HideInInspector] public string actionButon;
    }
    public PlayerData playerData;


    public enum PlayerStates
    {
        NOT_HOLDING_ITEM,
        HOLDING_ITEM,
        PICKING_UP
    }
    private PlayerStates state;

    private Animator animator;

    public RuntimeAnimatorController controllerWithOutBox;
    public RuntimeAnimatorController controllerWithBox;

    public GrabberPoint grabber;
    public float grabberOffsetX = 0.15f;
    public float grabberOffsetY = 0.1f;

    public Truck truckToCharge;
    
    private Rigidbody2D rb;

    private float pickUpTimer;
    private float pickUpedObjectWeight;
    private PickUpableObject pickUpedObject;

    private int score;
    private void Awake()
    {

        inputDataPlayer1.horizontalAxis = "HorizontalP1";
        inputDataPlayer1.verticalAxis = "VerticalP1";
        inputDataPlayer1.actionButon = "ActionP1";

        inputDataPlayer2.horizontalAxis = "HorizontalP2";
        inputDataPlayer2.verticalAxis = "VerticalP2";
        inputDataPlayer2.actionButon = "ActionP2";

        if (playerData.playerNumber == PlayerNumber.PLAYER_1)
        {
            playerData.horizontalAxis = inputDataPlayer1.horizontalAxis;
            playerData.verticalAxis = inputDataPlayer1.verticalAxis;
            playerData.actionButon = inputDataPlayer1.actionButon;
        }

        if (playerData.playerNumber == PlayerNumber.PLAYER_2)
        {
            playerData.horizontalAxis = inputDataPlayer2.horizontalAxis;
            playerData.verticalAxis = inputDataPlayer2.verticalAxis;
            playerData.actionButon = inputDataPlayer2.actionButon;
        }

    }
    // Use this for initialization
    void Start () {
        state = PlayerStates.NOT_HOLDING_ITEM;

        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = controllerWithOutBox;

        rb = GetComponent<Rigidbody2D>();
        grabber.player = this;

        pickUpTimer = 0;
        score = 0;
    }

    // Update is called once per frame
    void Update () {
        switch (state)
        {
            case PlayerStates.NOT_HOLDING_ITEM:
                Movement(0);
                
                if (Input.GetButton(playerData.actionButon) && grabber.canPickUp)
                {
                    state = PlayerStates.PICKING_UP;
                }
                break;
            case PlayerStates.HOLDING_ITEM:
                Movement(pickUpedObjectWeight);
                Drop();
                break;
            case PlayerStates.PICKING_UP:
                if (!grabber.canPickUp)
                {
                    state = PlayerStates.NOT_HOLDING_ITEM;
                }
                PickUp();

                break;
        }
    }

    public void Drop()
    {
        if (grabber.canDrop)
        {
            
            if (Input.GetButtonDown(playerData.actionButon))
            {
                truckToCharge.AddObjeetToTruck(pickUpedObject);

                score += pickUpedObject.score;
                state = PlayerStates.NOT_HOLDING_ITEM;
                animator.runtimeAnimatorController = controllerWithOutBox;

                pickUpedObject = null;
            }
        }
    }

    public void PickUp()
    {
        if (Input.GetButton(playerData.actionButon))
        {
            pickUpTimer += Time.deltaTime;
            if (pickUpTimer >= grabber.objectToPickUp.timeToPickUp)
            {
                pickUpTimer = 0;
                state = PlayerStates.HOLDING_ITEM;
                animator.runtimeAnimatorController = controllerWithBox;
                pickUpedObjectWeight = grabber.objectToPickUp.Weight;
                pickUpedObject = grabber.objectToPickUp;
                grabber.PickUpObject();

            }
        }
        else if (Input.GetButtonUp(playerData.actionButon))
        {
            state = PlayerStates.NOT_HOLDING_ITEM;
            pickUpTimer = 0;
        }
    }

    private void Movement(float weight)
    {    
        float x = Input.GetAxis(playerData.horizontalAxis);
        float y = Input.GetAxis(playerData.verticalAxis);

        Vector2 movement = new Vector2(x, y);
        if (movement.magnitude > 1.0f)
        {
            movement.Normalize();
        }

        rb.velocity = (movement * playerData.movementSpeed * Time.fixedDeltaTime) - ((movement * playerData.movementSpeed * Time.fixedDeltaTime) * (weight/ playerData.force));

        if (Input.GetAxis(playerData.verticalAxis) != 0)
        {
            animator.SetFloat("Vertical", Input.GetAxis(playerData.verticalAxis));
            animator.SetFloat("Horizontal", 0);
            if (Input.GetAxis(playerData.verticalAxis) < 0)
                grabber.gameObject.transform.localPosition = new Vector2(0, grabberOffsetY * -1);
            else
                grabber.gameObject.transform.localPosition = new Vector2(0, grabberOffsetY + .2f);
        }
        if (Input.GetAxis(playerData.horizontalAxis) != 0)
        {
            animator.SetFloat("Horizontal", Input.GetAxis(playerData.horizontalAxis));
            animator.SetFloat("Vertical", 0);
            if (Input.GetAxis(playerData.horizontalAxis) < 0)
                grabber.gameObject.transform.localPosition = new Vector2(grabberOffsetX * -1, .1f);
            else
                grabber.gameObject.transform.localPosition = new Vector2(grabberOffsetX, .1f);
        }

        if (Input.GetAxis(playerData.horizontalAxis) == 0 && Input.GetAxis(playerData.verticalAxis) == 0)
        {
            animator.SetBool("NoMovement", true);
            animator.SetFloat("Vertical", Input.GetAxis(playerData.verticalAxis));
            animator.SetFloat("Horizontal", Input.GetAxis(playerData.horizontalAxis));
        }
        else
        {
            animator.SetBool("NoMovement", false);
        }
    }
}
