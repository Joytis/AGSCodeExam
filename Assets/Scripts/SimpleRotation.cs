using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
	bool _isRotating = false;
	bool _rotationDirection = true; // true => counterclockwise | false => clockwise

	public float rotationRate = 90f;
	public Vector3 rotationAxis = new Vector3(0, 0, 1);

	public void ToggleRotation() => _isRotating = !_isRotating;
	public void ToggleDirection() => _rotationDirection = !_rotationDirection;

    // Just rotate the ball if we can in the direction specified. 
    void Update() {
        if(_isRotating) {
        	var direction = _rotationDirection ? -1 : 1;
        	var magnitude = Time.deltaTime * rotationRate * direction;

        	transform.Rotate(rotationAxis * magnitude);
        }
    }
}
