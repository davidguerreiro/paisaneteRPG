using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    public float movementSpeed;                         // Player's movement speed.
    private Vector2 movement;                           // Temporal vector used to get player's movement direction.
    private Rigidbody2D rigidbody2D;                    // Rigibody 2D component reference.
    private Animator animator;                          // Animator class component reference.
    private string animationState;                      // Animation state variable name.

    // values for animation variables machine states.
    enum CharStates {
        walkEast = 1,
        walkSouth = 2,
        walkWest = 3,
        walkNorth = 4,
        idleSouth = 5,
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
    /// <returns>void</returns>
    void FixedUpdate() {
        MovePlayerByInput();
    }

    /// <summary>
    /// Update animation machine state
    /// based on player movement.
    /// This method must be called from 
    /// Update.
    /// </summary>
    /// <returns>void</returns>
    private void UpdateAnimationState() {

        // check for east movement animation.
        if ( movement.x > 0 ) {
            animator.SetInteger( this.animationState, (int) CharStates.walkEast );
        }

        // check for west movement animation.
        else if ( movement.x < 0 ) {
            animator.SetInteger( this.animationState, (int) CharStates.walkWest ); 
        }

        // check for north movement animation.
        else if ( movement.y > 0 ) {
            animator.SetInteger( this.animationState, (int) CharStates.walkNorth );
        }

        // check for south movement animation.
        else if ( movement.y < 0 ) {
            animator.SetInteger( this.animationState, (int) CharStates.walkSouth );
        }

        // if none of the above, then display player idle animation for no-movement state.
        else {
            animator.SetInteger( this.animationState, (int) CharStates.idleSouth );
        }
    }

    /// <summary>
    /// Move player in 4 directions based
    /// in user input. This method must be
    /// called from FixedUpdate.
    /// </summary>
    /// <returns>void</returns>
    private void MovePlayerByInput() {

        // read input from keyboard.
        movement.x = Input.GetAxisRaw( "Horizontal" );
        movement.y = Input.GetAxisRaw( "Vertical" );

        // normalize vector.
        movement.Normalize();

        // update velocity value in the rigibody component.
        rigidbody2D.velocity = movement * this.movementSpeed;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <returns>void</returns>
    private void Init() {
        
        // get rigibody 2D component.
        rigidbody2D = GetComponent<Rigidbody2D>();

        // get animator component reference.
        animator = GetComponent<Animator>();

        // set default value for animation state controller variable name;
        this.animationState = "AnimationState";
    }
}
