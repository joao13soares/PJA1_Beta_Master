using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour, IRaycastResponse, IPickUpable, IRechargeable
{
    
    // Variables
    [SerializeField] protected List<Action> itemActions;
    [SerializeField] protected GameObject itemGameObjectForInspect;
    [SerializeField] protected Sprite icon;
    [SerializeField] protected string type = "Weapon";
    [SerializeField] protected bool isPermanent = true;

    // Inventory to be stored
    [SerializeField] protected Inventory inventory;

    // -------------------------------------IPickUpable---------------------------------------------//

    //ACESSORS
    public List<Action> ItemActions => itemActions;
    public GameObject ItemGameObjectForInspect => itemGameObjectForInspect;
    public Sprite Icon => icon;
    public string Type => type;

    public Inventory InventoryToStore => inventory;

    public bool IsPermanent => isPermanent;
    // -------------------------------------------------------------------------------------------//


    [SerializeField] private string magazineType;
    public int RemainingMagazines
    {
        get
        {
            if (inventory.IsStored(magazineType))
            {
                return inventory.GetQuantity(magazineType);
            }

            return 0;
        }
        
    }

  
   
    
    // Magazine/bullets variables
    [SerializeField] protected int bulletsinCurrentMagazine;
    [SerializeField] protected int defaultMagazineSize = 10;
    [SerializeField] protected int bulletDamage = 1;

    // Variables for shooting
    [SerializeField] protected float defaultShotCooldown = 0.25f;  // Number in seconds which controls how often the player can fire
    [SerializeField] public float nextShotCooldown; // Float to store the time left until the player will be allowed to fire again, after firing
    [SerializeField] protected int pelletsPerBulletShot = 5;  // Number of pellets per bullet shoot

    // Bullet spread variables
    [SerializeField] protected float pelletSpreadRadiusMultiplier = 1.0f;   // Maximum spread radius per pellet (multiplier)

    // Weapon recoil effect variables
    [SerializeField] protected float recoil = 20.0f;    // Recoil angle after shooting
    [SerializeField] protected float weaponKickRecoil = 0.15f;  // Kickback intensity after shooting

    protected Vector3 defaultLocalPosition;
    protected Quaternion defaultLocalRotation;


    // Gun specific values
    [SerializeField] protected float gunRange = 50f; // Distance in Unity units over which the player can fire
    [SerializeField] protected float hitForce = 100f;   // Amount of force which will be added to objects with a rigidbody shot by the player

    // Sound effects for weapon
    [SerializeField] protected AudioClip[] gunSounds;
    [SerializeField] protected AudioSource gunAudio;   // Holds a reference to the audio source which will play our shooting and reloading sound effects


    // Camera variables
    [SerializeField] protected Camera playerCamera;          // Holds a reference to the first person camera
    
    public Vector3 rayOrigin;

    [SerializeField] protected Transform transformForParent;

    // Weapon Events
    public delegate void OnShooting(int bullets);
    public event OnShooting Shot;

    // Visual shot hit effect manager
    protected PelletHoleManager pelletHoleManager;

    // Keycode associated to weapon for fast equiping
    [SerializeField] public KeyCode weaponKeyCode;

    // Weapon manager
    [SerializeField] private WeaponManager weaponManager;


    //ACESSORS
    public int BulletsInCurrentMagazine => bulletsinCurrentMagazine;
    public int DefaultMagazineSize => defaultMagazineSize;
    
   

    // ANIMATIONS
    [SerializeField] protected Animation animationComponent;
    protected WeaponAnimationManager weaponAnimationManager;

    [SerializeField] protected AnimationClip holsterWeaponAnimation;
    [SerializeField] protected AnimationClip drawWeaponAnimation;
    [SerializeField] protected AnimationClip reloadWeaponAnimation;

    public bool IsAnimationPlaying => weaponAnimationManager.IsAnimationPlaying;

    
    
    // Conditions(TIRAR COMENTARIO QUANDO ANIMATIONS PRONTAS)
    public virtual bool CanShoot => nextShotCooldown <= 0 && bulletsinCurrentMagazine > 0 ;// && !IsAnimationPlaying;
    public virtual bool CanReload => RemainingMagazines  > 0 && bulletsinCurrentMagazine != defaultMagazineSize; // && !IsAnimationPlaying;

    
    
    private void Awake()
    {
        weaponAnimationManager = new WeaponAnimationManager(animationComponent, holsterWeaponAnimation,drawWeaponAnimation,reloadWeaponAnimation);
        
        pelletHoleManager = new PelletHoleManager();
        bulletsinCurrentMagazine = defaultMagazineSize;

        defaultLocalPosition = this.transform.position;
        defaultLocalRotation = this.transform.rotation;
    }


    private void Update()
    {
        // Gradually restore position and rotation after shooting kickback and recoil, respectively
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, defaultLocalPosition, Time.deltaTime * 4f);
        this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, defaultLocalRotation, Time.deltaTime * 4f);

        // If there is a delay for the next shot
        if (nextShotCooldown > 0)
        {
            // Keep decreasing the delay
            nextShotCooldown -= Time.deltaTime;
        }
    }
    public virtual void Attacking()
    {
        // Update the time when our player can fire next
        nextShotCooldown = defaultShotCooldown;

        //PlayShootingSound();

        // Create a vector at the center of our camera's viewport
        rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        // Reduce the bullets available
        bulletsinCurrentMagazine--;

        // Declare a raycast hit to store information about what our raycast has hit
        RaycastHit hit;

        Vector3 pelletDirection = WeaponSpread();

        // Check if our raycast has hit anything
        if (Physics.Raycast(rayOrigin, pelletDirection, out hit, gunRange))
        {

            // Get a reference to an Enemy script attached to the collider we hit
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            float impactPercentageWithDistance = WeaponForceCalculation(hit);

            DoDamage(enemy, impactPercentageWithDistance);

            ApplyWeaponForce(hit, impactPercentageWithDistance);

            HoleCreation(hit);



        }

        WeaponRecoil();
        Debug.Log($"bullets: {bulletsinCurrentMagazine}");
        ShotEvent();
    }



    //CReates the holes
    protected void HoleCreation(RaycastHit hit) => pelletHoleManager.NewPelletHole(hit.point, hit.collider.gameObject);

    //Invokes the shot Event
    protected void ShotEvent()
    {
        Shot?.Invoke(bulletsinCurrentMagazine);
    }

    //Responsable for the weaponRecoil
    protected void WeaponRecoil()
    {
        this.transform.localRotation = defaultLocalRotation;
        this.transform.localPosition = defaultLocalPosition;
        this.transform.localPosition -= this.transform.forward * weaponKickRecoil;
        this.transform.Rotate(-recoil, 0f, 0f);
    }

    //OnPickUp overrrides the default values
    public void OnPickUpDefaultInit(Quaternion localRotation, Vector3 localPosition)
    {
        defaultLocalRotation = localRotation;
        defaultLocalPosition = localPosition;
    }


    //Changes the magazine for the current weapon with a magazine thats has random bullets in her

    private void ChangeMagazine()
    {
        float fillPercentage = RandomNonLinearProbabilityPercentage();
        bulletsinCurrentMagazine = (int)(defaultMagazineSize * fillPercentage);
        
        
        // Play the reloading sound effect
        gunAudio.PlayOneShot(gunSounds[1]);
        
        //DESCOMENTAR QUANDO ANIMATIONS PRONTAS
        // weaponAnimationManager.PlayReloadAnimation();
        
        inventory.RemoveSlot(type);
        
    }

    //Calculates the % of next magazines bullets 
    private float RandomNonLinearProbabilityPercentage()
    {
        float randNonLinearProbabilityPercentage = 0.00f;

        for (int i = 1; i <= 4; i++)
        {
            randNonLinearProbabilityPercentage = UnityEngine.Random.Range(randNonLinearProbabilityPercentage, 0.25f * i);
        }

        return randNonLinearProbabilityPercentage;
    }


  

    // Play the shooting sound effect
    //protected void PlayShootingSound() => gunAudio.PlayOneShot(gunSounds[0]);


    //Responsable for doing the math for the weapon Spread
    protected virtual Vector3 WeaponSpread()
    {
        float recoilSpreadFactor;
        // Else, if there's only 1 pellet per bullet shot, spread it according to the current gun recoil
        float currentRecoil = 360.0f - this.transform.localEulerAngles.x;
        if (currentRecoil >= 360f) currentRecoil = 0.0f;
        recoilSpreadFactor = currentRecoil;

        float pelletSpreadRadius = Random.Range(0.0f, recoilSpreadFactor / 400.0f) * pelletSpreadRadiusMultiplier;  // Pellet spread radius increases with the current weapon X rotation, due to recoil
        Vector3 pelletSpreadAngle = this.transform.TransformDirection(Random.insideUnitCircle.normalized);  // TransformDirection from local space to world space | .normalized turns it into .onUnitCircle
        Vector3 pelletDirection = this.transform.forward - (this.transform.forward.y - playerCamera.transform.forward.y) * 0.5f * Vector3.up + pelletSpreadAngle * pelletSpreadRadius;

        return pelletDirection;
    }


    protected float WeaponForceCalculation(RaycastHit hit) => 1 - hit.distance / gunRange;



    protected void DoDamage(Enemy enemy, float impactPercentageWithDistance)
    {
        // If there was an Enemy script attached
        if (enemy != null && !enemy.isDying())
        {
            // Call the damage function of the Enemy script, passing in our pelletDamage
            enemy.Damage(bulletDamage * impactPercentageWithDistance);
        }
    }


    protected void ApplyWeaponForce(RaycastHit hit, float impactPercentageWithDistance)
    {
        // Check if the object we hit has a rigidbody attached
        if (hit.rigidbody != null)
        {
            // Add force to the rigidbody we hit, in the direction from which it was hit
            hit.rigidbody.AddForce(-hit.normal * hitForce * impactPercentageWithDistance);
        }
    }

    
    
    
    public void OnRaycastSelect()
    {
        StoreItem();
    }



    //Stores the weapon on the inventory
    public void StoreItem()
    {
        inventory.AddItemSlot(this.gameObject);
        weaponManager.AddWeapon(this.gameObject, transformForParent.transform);

    }
   

    public  void RemoveItem()
    {
        inventory.RemoveSlot(type);
    }


    public void Recharge()
    {
        ChangeMagazine();
    }
}

