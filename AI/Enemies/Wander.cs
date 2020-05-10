using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( Rigidbody2D ) ) ]
[RequireComponent( typeof( CircleCollider2D ) ) ]
[RequireComponent( typeof( Animator ) ) ]

public class Wander : MonoBehaviour {
    public float pursuitSpeed;                                  // Enemy movement speed when chasing the player.
    public float wanderSpeed;                                   // Enemy movement speed when wandering out in the field.
    public bool followPlayer;                                   // Flag to control whether the enemy if following the player.
    public float directionChangeInterval;                       // Determines how often the enemy changes direction when wandering around.
    float currentSpeed;                                         // Current enemy speed.
    Coroutine moveCoroutine;                                    // Movement coroutine.
    Rigidbody2D rigibody2d;                                     // Rigibody 2d component reference.
    Animator animator;                                          // Animator component reference.
    Transform targetTransform = null;                           // Target transform component reference.
    Vector3 endPosition;                                        // Destination where the enemy is wandering.
    float currentAngle = 0;                                     // When choosing a new direction to wander, a new angle is added to the existing angle. The angle is used to generate a vector, which becomes the destination.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Wander coroutine. This is the core
    /// method for the wander algorithym.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator WanderRoutine() {

        while ( true ) {

            ChooseNewEndPoint();

            if ( moveCoroutine != null ) {
                StopCoroutine( moveCoroutine );
            }

           //  moveCoroutine = StartCoroutine( Move( rigibody2d, this.currentSpeed ) );

            yield return new WaitForSeconds( this.directionChangeInterval );
        }
    }

    /// <summary>
    /// Choose a new point in the world
    /// for the enemy to move in.
    /// </summary>
    private void ChooseNewEndPoint() {

        this.currentAngle += Random.Range( 0, 360 );
        this.currentAngle = Mathf.Repeat( this.currentAngle, 360 );

        endPosition += Vector3FromAngle( this.currentAngle );
    }

    /// <summary>
    /// This method takes an angle parameter in degrees,
    /// converts it to radians, and returns a directional 
    /// Vector3 used by the ChooseNewEndPoint() method.
    /// </summary>
    /// <param name="inputAngleDegrees">float - angles to convert to radians</param>
    /// <returns>Vector3</returns>
    private Vector3 Vector3FromAngle( float inputAngleDegrees ) {

        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;

        return new Vector3( Mathf.Cos( inputAngleRadians ), Mathf.Sin( inputAngleRadians ), 0 );
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get animator component reference.
        animator = GetComponent<Animator>();

        // set current speed initial value;
        this.currentSpeed = this.wanderSpeed;

        // get rigibody 2d component reference.
        rigibody2d = GetComponent<Rigidbody2D>();

        // init wander coroutine.
        // StartCoroutine( WanderCoroutine() );
    }
}
