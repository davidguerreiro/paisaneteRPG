using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( Rigidbody2D ) ) ]
[RequireComponent( typeof( CircleCollider2D ) ) ]
[RequireComponent( typeof( Animator ) ) ]

public class Wander : MonoBehaviour {
    public float pursuitSpeed;                                  // Enemy movement speed when chasing the player.
    public float wanderSpeed;                                   // Enemy movement speed when wandering out in the field.
    public float directionChangeInterval;                       // Determines how often the enemy changes direction when wandering around.
    public bool followPlayer;                                   // Flag to control whether the enemy if following the player.
    float currentSpeed;                                         // Current enemy speed.
    Coroutine moveCoroutine;                                    // Movement coroutine.
    Rigidbody2D rigibody2d;                                     // Rigibody 2d component reference.
    Animator animator;                                          // Animator component reference.
    Transform targetTransform = null;                           // Target transform component reference.
    Vector3 endPosition;                                        // Destination where the enemy is wandering.
    float currentAngle = 0;                                     // When choosing a new direction to wander, a new angle is added to the existing angle. The angle is used to generate a vector, which becomes the destination.
    private CircleCollider2D circleCollider2D;                  // Circle collider 2D component refernece. Used to drawn gizmos for debugging.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        // debug movement destination line.
        Debug.DrawLine( rigibody2d.position, endPosition, Color.red );
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

            moveCoroutine = StartCoroutine( Move( rigibody2d, this.currentSpeed ) );

            yield return new WaitForSeconds( this.directionChangeInterval );
        }
    }

    /// <summary>
    /// Move enemy to choosen point
    /// during the wandering state.
    /// </summary>
    /// <param name="rigiBodyToMove">Rigibody2D - rigibody component which is going to be moved</param>
    /// <param name="speed">float - movement speed.</param>
    /// <returns>IEnumerator</returns>
    public IEnumerator Move( Rigidbody2D rigiBodyToMove, float speed ) {

        // get remaining distance between current position and move position in float number.
        float remainingDistance = ( transform.position - endPosition ).sqrMagnitude;

        while ( remainingDistance > float.Epsilon ) {

            // if the enemy is pursuing the player, the end position is altered
            // to be the player's position.
            if ( targetTransform != null ) {
                endPosition = targetTransform.position;
            }

            // move enemy.
            if ( rigiBodyToMove != null ) {

                animator.SetBool( "isWalking", true );
                
                // move towards calculates the next position where the enemy has to move. MovePosition actually moves the enemy to new calculated position.
                Vector3 newPosition = Vector3.MoveTowards( rigiBodyToMove.position, endPosition, speed * Time.deltaTime );
                rigibody2d.MovePosition( newPosition );

                
                remainingDistance = ( transform.position - endPosition ).sqrMagnitude;
            }

            yield return new WaitForFixedUpdate();
        }

        // stop moving animation when the loop is closed.
        animator.SetBool( "isWalking", false );

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
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="collision">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D( Collider2D collision ) {
        
        // if the player enters the wander view range and pursuit mode is enable for this entity, them enable pursueMode.
        if ( collision.gameObject.CompareTag("Player") && this.followPlayer ) {
            PursuePlayer( collision.gameObject );
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="collision">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D( Collider2D collision ) {
        
        // if the player leaves the wander view range, the wander entity stops chasing the player and returns back to wandering state.
        if ( collision.gameObject.CompareTag( "Player" ) ) {
            StopPursuingPlayer();
        }
    }

    /// <summary>
    /// Enable pursue mode, so this 
    /// wander entity pursues the player.
    /// </summary>
    /// <param name="pursueTarget">GameObject - gameObject referent to target to be pursued</param>
    private void PursuePlayer( GameObject pursueTarget ) {

        // set new target and new movement speed.
        this.currentSpeed = this.pursuitSpeed;
        targetTransform = pursueTarget.transform;

        // stop wandering coroutine if neccesary.
        if ( moveCoroutine != null ) {
            StopCoroutine( moveCoroutine );
        }

        moveCoroutine = StartCoroutine( Move( rigibody2d, currentSpeed ) );
    }

    /// <summary>
    /// Disable pursue mode and set wanderer
    /// entity to wander again.
    /// </summary>
    private void StopPursuingPlayer() {

        animator.SetBool( "isWalking", false );

        // set wander speed.
        this.currentSpeed = this.wanderSpeed;

        // stop pursuing if neccesary
        if ( moveCoroutine != null ) {
            StopCoroutine( moveCoroutine );
        }

        // disable target transform.
        targetTransform = null;
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos() {
        
        // draw enemiy range of view.
        if ( circleCollider2D != null ) {
            Gizmos.DrawWireSphere( transform.position, circleCollider2D.radius );
        }
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

        // get circle 2d component reference.
        circleCollider2D = GetComponent<CircleCollider2D>();

        // init wander coroutine.
        StartCoroutine( WanderRoutine() );
    }
}
