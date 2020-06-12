using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arc : MonoBehaviour {

    /// <summary>
    /// Defines the arc used
    /// by the item that is thrown
    /// It uses linear interpolation algorythim
    /// to move the object to one position
    /// to another at same speed.
    /// The longer is thrown, the faster it will go.
    /// </summary>
    /// <param name="destination">Vector3 - where the object is being thrown</param>
    /// <param name="duration">float - arc throwing duration</param>
    /// <returns>IEnumerator</returns>
    public IEnumerator TravelArc ( Vector3 destination, float duration ) {
        var startPosition = transform.position;
        var percentComplete = 0.0f;

        while ( percentComplete < 1.0f ) {

            percentComplete += Time.deltaTime / duration;
            transform.position = Vector3.Lerp( startPosition, destination, percentComplete );

            yield return null;
        }

        gameObject.SetActive( false );
        
    }

}
