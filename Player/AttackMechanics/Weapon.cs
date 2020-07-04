using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(Animator) ) ]
public class Weapon : MonoBehaviour {
    public int initialAmmo;                             // Initial ammo quantity
    public GameObject ammoPrefab;                       // Ammo prefab - used to instantiate in the object pool for ammo.
    public PlayerAmmo playerAmmo;                       // Player ammo scriptable object reference.
    public int poolSize;                                // Ammo object pool size.
    public float weaponVelocity;                        // Ammo fired speed.
    static List<GameObject> ammoPool;                   // Ammo pool list of gameoObjects.
    public bool isFiring;                               // Wheter the player is firing proyectiles.
    [HideInInspector]
    public Animator animator;                           // Player controller animator, used to set parameters for the blend tree animation.
    public Camera localCamera;                          // Main camera reference.
    private float positiveSlope;                        // Positive slope for quadrant calculations.
    private float negativeSlope;                        // Negative slope for quadrant calculations.
    private enum Quadrant {                             // Quadrant enum to describe and calculate in which direction the player is firing.
        East,
        South,
        West,
        North
    };

    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        SetAmmoObjectPool();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Init();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        
        if ( Input.GetMouseButtonDown(0) && playerAmmo.value > 0f ) {
            this.isFiring = true;
            FireAmmo();
        }

        // update firing animation state.
        UpdateState();
    }

    /// <summary>
    /// Spawn ammo method.
    /// Spawn a new ammo proyectile by
    /// location.
    /// </summary>
    /// <parma name="location">Vector3 - where to spawn the ammo</param>
    /// <returns>GameObject</returns>
    private GameObject SpawnAmmo( Vector3 location ) {
        
        // loop through the ammo pool.
        foreach ( GameObject ammo in ammoPool ) {

            // if an unused disabled ammo gameobject is found in the pool, the use it.
            if ( ammo.activeSelf == false ) {

                ammo.SetActive( true );
                ammo.transform.position = location;
                
                // ammo consumed.
                playerAmmo.value--;

                return ammo;
            }
        }

        return null;
    }

    /// <summary>
    /// Fire ammo when the 
    /// player press attack button.
    /// </summary>
    private void FireAmmo() {

        // get mouse position.
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );

        GameObject ammo = SpawnAmmo( transform.position );

        if ( ammo != null ) {

            Arc arcScript = ammo.GetComponent<Arc>();
            float travelDuration = 1.0f / weaponVelocity;

            StartCoroutine( arcScript.TravelArc( mousePosition, travelDuration ) );
        }
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy() {
        ammoPool = null;
    }

    /// <summary>
    /// Set up ammo object pool.
    /// This is done to reuse ammo gameObjects
    /// and improve performance avoinding
    /// extra instatiation / destruction of ammo
    /// gameObjects.
    /// </summary>
    private void SetAmmoObjectPool() {

        if ( ammoPool == null ) {
            ammoPool = new List<GameObject>();
        }

        for ( int i = 0; i < this.poolSize; i++ ) {
            GameObject ammoObject = Instantiate( ammoPrefab );
            ammoObject.SetActive( false );
            ammoPool.Add( ammoObject );
        }
    }

    /// <summary>
    /// Get slope.
    /// </summary>
    /// <param name="pointOne">Vector2 - First point of the slope line.</param>
    /// <param name="pointTwo">Vector2 - Second point of the slope line.</param>
    /// <returns>float</returns>
    private float GetSlope( Vector2 pointOne, Vector2 pointTwo ) {
        return ( pointTwo.y - pointOne.y ) / ( pointTwo.x - pointOne.x );
    }

    /// <summary>
    /// Generate slopes in the screen using the camera.
    /// Used to divide the screen area in 4 quadrants.
    /// The positive slope goes frm the lower corner of the 
    /// screen to the upper right. The negative slope goes 
    /// from the upper left corner to the lower right corner 
    /// of the screen.
    /// More info the in page 354 of the book.
    /// </summary>
    private void GenerateSlopes() {

        // calculate all the points to generate the slopes ( the four cornes of the screen ).
        Vector2 lowerLeft = localCamera.ScreenToWorldPoint( new Vector2( 0, 0 ) );
        Vector2 upperRight = localCamera.ScreenToWorldPoint( new Vector2( Screen.width, Screen.height ) );
        Vector2 upperLeft = localCamera.ScreenToWorldPoint( new Vector2( 0, Screen.height ) );
        Vector2 lowerRight = localCamera.ScreenToWorldPoint( new Vector2( Screen.width, 0 ) );

        // calculate the slopes and save them in the local variables.
        positiveSlope = GetSlope( lowerLeft, upperRight );
        negativeSlope = GetSlope( upperLeft, lowerRight );
    }

    /// <summary>
    /// Calculates if the point where the user clicks is
    /// actually higher than the positive slope line.
    /// </summary>
    /// <param name="inputPosition">Vector2 mouse click input position</param>
    /// <returns>bool</returns>
    private bool HigherThanPositiveSlopeLine( Vector2 inputPosition ) {

        Vector2 playerPosition = gameObject.transform.position;

        // transform user click to world coordenates position vector2.
        Vector2 mousePosition = localCamera.ScreenToWorldPoint( inputPosition );

        float yIntercept = playerPosition.y - ( positiveSlope * playerPosition.x );
        float inputIntercept = mousePosition.y - ( positiveSlope * mousePosition.x );

        return inputIntercept > yIntercept;
    }

    /// <summary>
    /// Calculates if the point where the user clicks is
    /// actually higher than the negative slope line.
    /// </summary>
    /// <parma name="inputPosition">Vector2 mouse click input position</param>
    /// <returns>bool</returns>
    private bool HigherThanNegativeSlopeLine( Vector2 inputPosition ) {

        Vector2 playerPosition = gameObject.transform.position;

        // transform user click to world coordenates position vector2.
        Vector2 mousePosition = localCamera.ScreenToWorldPoint( inputPosition );

        float yIntercept = playerPosition.y - ( negativeSlope * playerPosition.x );
        float inputIntercept = mousePosition.y - ( negativeSlope * mousePosition.x );

        return inputIntercept > yIntercept;
    }

    /// <summary>
    /// This method checks in which quadrant the user has click with
    /// the mouse when firing ammo. It is used to update the blend animation
    /// for firing ammo.
    /// </summary>
    /// <returns>Quadrant</returns>
    private Quadrant GetQuadrant() {

        Vector2 mousePosition = Input.mousePosition;
        Vector2 playerPosition = gameObject.transform.position;

        bool higherThanPositiveSlopeLine = HigherThanPositiveSlopeLine( mousePosition );
        bool higherThanNegativeSlopeLine = HigherThanNegativeSlopeLine( mousePosition );

        // logic to get quadrant based on the results of the slope calculations.
        if ( ! higherThanPositiveSlopeLine && higherThanNegativeSlopeLine ) {
            return Quadrant.East;
        } 
        else if ( ! higherThanPositiveSlopeLine && ! higherThanNegativeSlopeLine ) {
            return Quadrant.South;
        }
        else if ( higherThanPositiveSlopeLine && ! higherThanNegativeSlopeLine ) {
            return Quadrant.West;
        }
        else {
            return Quadrant.North;
        }
    }

    /// <summary>
    /// Update firing animation state
    /// based in user input action and
    /// quadrant clicked by the user.
    /// </summary>
    private void UpdateState() {

        if ( this.isFiring ) {
            
            Vector2 quadrantVector;
            Quadrant quadEnum = GetQuadrant();

            switch ( quadEnum ) {
                case Quadrant.East:
                    quadrantVector = new Vector2( 1.0f, 0.0f );
                    break;
                case Quadrant.South:
                    quadrantVector = new Vector2( 0.0f, -1.0f );
                    break;
                case Quadrant.West:
                    quadrantVector = new Vector2( -1.0f, 1.0f );
                    break;
                case Quadrant.North:
                    quadrantVector = new Vector2( 0.0f, 1.0f );
                    break;
                default:
                    quadrantVector = Vector2.zero;
                    break;
            }

            // update blend animation values.
            animator.SetBool( "isFiring", true );

            animator.SetFloat( "fireXDir", quadrantVector.x );
            animator.SetFloat( "fireYDir", quadrantVector.y );

            this.isFiring = false;
        } else {
            
            // set animator boolean paramenter as false if the player is not longer firing proyectiles.
            animator.SetBool( "isFiring", false );
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public void Init() {

        // set initial ammo value.
        playerAmmo.value = this.initialAmmo;

        // get animatior component.
        animator = GetComponent<Animator>();

        // set firing flag default value.
        this.isFiring = false;

        // get a reference from the main camera.
        localCamera = Camera.main;

        // generate the slopes to create the quadrants used to display firing weapon animation.
        GenerateSlopes();
    }
}
