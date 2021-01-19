
using UnityEngine;

public class FlashLightInput : MonoBehaviour
{
    //--------------------------------------------------

    // Input variables
    private const float HoldKeyDelay = 1f;
    private float currentTimer;
    private bool canBeUsed;


    [SerializeField] private Transform flashlightHolderTransform;

    [SerializeField] private Light flashlightLight;


    [SerializeField] private Inventory inventoryToCheckBatteries;
    [SerializeField] private string batteriesType;

    [SerializeField] int chargeDuration;
    [SerializeField] float currentChargeDurationRemaining;
    public int CurrentChargePercentageRemaining => (int)((currentChargeDurationRemaining / chargeDuration) * 100);
    private int RemainingCharges
    {
        get
        {
            if (inventoryToCheckBatteries.IsStored(batteriesType))
                return inventoryToCheckBatteries.GetQuantity(batteriesType);

            return 0;
        }
    }





    //--------------------------------------------------

    void Awake()
    {
        currentChargeDurationRemaining = chargeDuration;
        canBeUsed = true;
    }

    void Update()
    {
        // Timer counting battery time left
        if (flashlightLight.enabled)
            BatteryTimer();

        // Input
        FlashLightAction();

        float forwardOffset = 25f;
        // transform.LookAt(flashlightHolderTransform.forward + new Vector3(0f,0f,forwardOffset), );


        RaycastHit hit;
        if (Physics.Raycast(flashlightHolderTransform.transform.position, flashlightHolderTransform.forward, out hit, Mathf.Infinity))
        {
            Transform temp = flashlightHolderTransform;
            temp.LookAt(hit.point);

            Quaternion.Slerp(flashlightHolderTransform.rotation, temp.rotation, Time.deltaTime * 6f);

        }
        else
            transform.localRotation =
                Quaternion.LookRotation(flashlightHolderTransform.forward + new Vector3(0f, 0f, forwardOffset));
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
            if (currentTimer >= HoldKeyDelay && canBeUsed && RemainingCharges > 0)
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
        flashlightLight.enabled = false;
        currentTimer = 0f;
        canBeUsed = false;
        currentChargeDurationRemaining = chargeDuration;
    }
}