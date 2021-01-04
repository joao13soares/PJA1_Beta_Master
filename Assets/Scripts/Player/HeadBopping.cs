using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBopping : MonoBehaviour
{
    // Essential Components----------------------------------------

    [SerializeField] private GameObject playerCamera;

    private PlayerMovement playerMovementScript;
    
    [SerializeField] private float smoothTurn;
    private float headDirection, headTrack;

    // Start is called before the first frame update
    void Start()
    {
        // Gets acess to camera GameObject
        playerCamera = GameObject.Find("Main Camera");

        playerMovementScript = GetComponent<PlayerMovement>();
        headDirection = 1;

        
        smoothTurn = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        StateManager();

        headTurn();
    }

    // Manages head balance (head like sensation)
    private void headTurn()
    {
        // Head limits
        float leftLimitAngle = -10f;
        float rightLimitAngle = 10f;


        // Checks current direction of head turning
        if (headTrack < leftLimitAngle)
        {
            headDirection = 1;
            
        }
        else if (headTrack > rightLimitAngle)
        {
            headDirection = -1;
            
        }

        // Changes headtrack according to turnSpeed and current direction
        headTrack += headDirection * smoothTurn * Time.deltaTime;

        playerCamera.transform.eulerAngles = new Vector3(playerCamera.transform.eulerAngles.x, playerCamera.transform.eulerAngles.y, headTrack);
    }

 
    private void StateManager()
    {
        
        switch (playerMovementScript.GetPlayerState)
        {

            case PlayerMovement.PlayerState.IDLE:
                smoothTurn = 5f;
                break;

            case PlayerMovement.PlayerState.WALKING:
                smoothTurn = 10f;
                break;

            



        }

    }
}
