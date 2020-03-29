using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisionRange : MonoBehaviour {
    public static PlayerVisionRange instance;                       // Public static class instance.
    public float closestDistance;                                   // Closest raycast maximun ray distance.
    private GameObject closestGameObject;                           // Closest gameObject the player is looking at.
    private bool allowRays;                                         // Flag to control whether player raycasting is allowed. Used to improve performance.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        
        if ( allowRays ) {

            // trigger low distance ray to detect gameobjects close to the player and which are in the same direction the player is looking to.
            TriggerClosestVision();

            if ( closestGameObject != null ) {
                Debug.Log( closestGameObject.name );
            }
        }
    }

    /// <summary>
    /// Freeze player
    /// raycasting.
    /// </summary>
    public void FreezeRaycasting() {
        this.allowRays = false;
    }

    /// <summary>
    /// Unfreeze player
    /// raycasting.
    /// </summary>
    public void UnFreezeRaycasting() {
        this.allowRays = true;
    }

    /// <summary>
    /// Raycast closest player
    /// vision range and update
    /// closest gameObject seen
    /// by the player.
    /// </summary>
    private void TriggerClosestVision() {
        Vector2 rayDirection = GetVectorDirection();
        RaycastHit2D hit = Physics2D.Raycast( transform.position, rayDirection, this.closestDistance );

        // update closes gameObject
        closestGameObject = ( hit.collider != null ) ? hit.collider.gameObject : null;
    }

    /// <summary>
    /// Get vector direction to
    /// raycast vision range ray.
    /// Vector direction depeneds of
    /// player facing direction.
    /// </summary>
    /// <returns>Vector2</returns>
    private Vector2 GetVectorDirection() {

        Vector2 rayDirection;
        string playerDirection = PlayerMovementController.instance.GetFacingDirection();

        switch ( playerDirection ) {
            case "top":
                rayDirection = Vector2.up;
                break;
            case "down":
                rayDirection = Vector2.down;
                break;
             case "left":
                rayDirection = Vector2.left;
                break;
            case "right":
                rayDirection = Vector2.right;
                break;
            default:
                rayDirection = Vector2.zero;            // This point should never be reached.
                break;
        }

        return rayDirection;
    }

    /// <summary>
    /// Get closest gameObject the
    /// player is looking at.
    /// </summary>
    /// <returns>GameObject</summary>
    public GameObject GetClosestGameObject() {
        return closestGameObject;
    }


    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // set default value for the closest gameObject the player is looking at.
        closestGameObject = null;

        // set default value for attributes.
        this.allowRays = true;
    }
}
