using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoundCameraPos : CinemachineExtension {
    public float pixelsPerUnit;                             // Pixels used per unit - each block is 32 * 32.

    /// <summary>
    /// This method will be called by Cinemachine
    /// after the confiner is done procesing.
    /// </summary>
    protected override void PostPipelineStageCallback( CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime ) {

        if ( stage == CinemachineCore.Stage.Body ) {

            Vector3 pos = state.FinalPosition;
            Vector3 pos2 = new Vector3( Round( pos.x), Round( pos.y), pos.z );
            state.PositionCorrection += pos2 - pos;
        }
    }

    /// <summary>
    /// Basic round function
    /// We use this method to ensure
    /// the camera always stays on pixel
    /// position.
    /// </summary>
    /// <param name="x">float - value to round</param>
    /// <returns>float</returns>
    private float Round( float x ) {
        return Mathf.Round( x * this.pixelsPerUnit ) / this.pixelsPerUnit;
    }
}
