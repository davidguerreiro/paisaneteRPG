using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
    public static PlayerMovementController instance;    // Public static class instance.
    public float movementSpeed;                         // Player's movement speed.
    public bool canMove;                                // Flag to control whether the player can move.
    public bool isMoving;                               // Flag to control whether the player is moving or stopped.
    private Vector2 movement;                           // Temporal vector used to get player's movement direction.
    private Rigidbody2D rigidbody2D;                    // Rigibody 2D component reference.
    private Animator animator;                          // Animator class component reference.
    
    private string animationState;                      // Animation state variable name.
    private string playerFacingDirection;               // Records which direction the player is facing.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    }

    // values for animation variables machine states.
    enum CharStates {
        walkEast = 1,
        walkSouth = 2,
        walkWest = 3,
        walkNorth = 4,
        idleSouth = 5,
    }

    /// <summary>
    /// Get player's facing
    /// direction.
    /// </summary>
    /// <returns>string</returns>
    public string GetFacingDirection() {
        return this.playerFacingDirection;
    }

    /// <summary>
    /// Freeze player's movement.
    /// </summary>
    public void FreezePlayer() {
        this.canMove = false;
    }

    /// <summary>
    /// Unfreeze player's movement.
    /// </summary>
    public void UnFreezePlayer() {
        this.canMove = true;
    }

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        UpdateAnimationState();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {

        if ( this.canMove ) {
            MovePlayerByInput();
        }
    }

    /// <summary>
    /// Update animation machine state
    /// based on player movement.
    /// This method must be called from 
    /// Update.
    /// </summary>
    private void UpdateAnimationState() {

        // check for east movement animation.
        if ( movement.x > 0 ) {
            animator.SetInteger( this.animationState, (int) CharStates.walkEast );
            this.playerFacingDirection = "right";
        }

        // check for west movement animation.
        else if ( movement.x < 0 ) {
            animator.SetInteger( this.animationState, (int) CharStates.walkWest ); 
            this.playerFacingDirection = "left";
        }

        // check for north movement animation.
        else if ( movement.y > 0 ) {
            animator.SetInteger( this.animationState, (int) CharStates.walkNorth );
            this.playerFacingDirection = "top";
        }

        // check for south movement animation.
        else if ( movement.y < 0 ) {
            animator.SetInteger( this.animationState, (int) CharStates.walkSouth );
            this.playerFacingDirection = "down";
        }

        // if none of the above, then display player idle animation for no-movement state.
        else {
            // TODO: Add idle animation for all directions here based in player facing direction.
            animator.SetInteger( this.animationState, (int) CharStates.idleSouth );
        }
    }

    /// <summary>
    /// Move player in 4 directions based
    /// in user input. This method must be
    /// called from FixedUpdate.
    /// </summary>
    private void MovePlayerByInput() {

        // read input from keyboard.
        movement.x = Input.GetAxisRaw( "Horizontal" );
        movement.y = Input.GetAxisRaw( "Vertical" );

        // normalize vector.
        movement.Normalize();

        // update velocity value in the rigibody component.
        rigidbody2D.velocity = movement * this.movementSpeed;

        // update player movement flag.
        this.isMoving = ( rigidbody2D.velocity.magnitude > 0f ) ? true : false;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {
        
        // get rigibody 2D component.
        rigidbody2D = GetComponent<Rigidbody2D>();

        // get animator component reference.
        animator = GetComponent<Animator>();

        // set default value for animation state controller variable name;
        this.animationState = "AnimationState";

        // set default value for movement control attributes.
        this.canMove = true;
        this.isMoving = false;
        this.playerFacingDirection = "down";
    }
}
