using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    //--------------------------------------------------

    // Action variables
    private float holdKeyDelay = 1f, currentTimer = 0f;
    private bool isRecharging;
    public bool canBeUsed { get; set; }


    [SerializeField]
    private Light flashlightLight;


    // TEST
    [SerializeField]
    Transform player, playerCamera;
    GameObject playerObject;

    private Vector3 offset;
    private Vector3 initialOffset;

    public MovementInfo info;

    float angularDrag = 0.95f;


    [SerializeField] private Inventory inventoryToCheckBatteries;
    [SerializeField] private string batteriesType;

    private int remainingCharges
    {
        get
        {
            if (inventoryToCheckBatteries.IsStored(batteriesType))
                return inventoryToCheckBatteries.GetQuantity(batteriesType);
            
           return 0;
        }
    } 


    
    
    int chargeDuration;
    float currentChargeDurationRemaining;
    public int CurrentChargePercentageRemaining => (int) ((currentChargeDurationRemaining / chargeDuration) * 100);

    //--------------------------------------------------

    void Awake()
    {
        if (flashlightLight == null) flashlightLight = this.GetComponentInChildren<Light>();

        playerObject = GameObject.FindGameObjectWithTag("Player");

        player = playerObject.transform;
        playerCamera = playerObject.GetComponentInChildren<Camera>().transform;

        offset = playerCamera.position - this.transform.position;

        initialOffset = player.transform.localPosition - this.transform.position;
        initialOffset.Normalize();

        currentChargeDurationRemaining = 300;
        chargeDuration = 300;
        isRecharging = false;
        canBeUsed = true;

    }

    void Update()
    {

        // Timer counting battery time left
        if (flashlightLight.enabled)
            BatteryTimer();

        // Input
        FlashLightAction();

    }


    private void FlashLightAction()
    {

        // If it is only a click and has battery
        if (Input.GetKeyUp(KeyCode.F) && currentChargeDurationRemaining > 0)
        {
            //Resets timer for click/hold separation
            currentTimer = 0f;

            //Turn On/Off
            if (canBeUsed && currentChargeDurationRemaining > 0)
            {
                flashlightLight.enabled = !flashlightLight.enabled;
            }
            else canBeUsed = true;


        }

        // Checks if it is held down(1sec held)
        else if (Input.GetKey(KeyCode.F))
        {

            if (currentTimer >= holdKeyDelay && canBeUsed && remainingCharges > 0)
            {
                flashlightLight.enabled = false;
                Recharge();
                currentTimer = 0f;
                canBeUsed = false;

            }

            else
                currentTimer += Time.deltaTime;
        }



    }

    private void BatteryTimer()
    {
        // Counts down
        if (currentChargeDurationRemaining > 0)
            currentChargeDurationRemaining -= Time.deltaTime;

        // Clamps when goes below 0 and turns off lantern
        else
        {
            currentChargeDurationRemaining = 0;
            flashlightLight.enabled = false;

        }
        
    }

    public void Recharge()
    {
        if (remainingCharges > 0)
        {
            inventoryToCheckBatteries.RemoveSlot(batteriesType);
            currentChargeDurationRemaining = chargeDuration;
        }


    }


  






}
