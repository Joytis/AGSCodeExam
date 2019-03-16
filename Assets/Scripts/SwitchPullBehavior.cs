using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwitchPullBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public float timeToReset = 0.5f; // Time it takes for the switch to fall back down. 
	public int mouseYThresholdForToggle = 30; // Pixel threshold to move. Should probably be relative to screen size. 
	public RectTransform transformToSFlip = null; // Just flip the scale of the image. 
	public UnityEvent onSwitchPulled;

	bool _mouseReleased = false;
	Vector3 _startPosition;
	Vector3 _currentPosition;

	bool HasPassedThreshold() => (_currentPosition - _startPosition).y > 30;

	// This is a little kludgey, but it should be okay for this. 
	// A simple state machine that handles all the fun switch behavior. 
	IEnumerator runningCoroutine = null;

	void StopRunningCoroutine() {
		StopCoroutine(runningCoroutine);
		runningCoroutine = null;
	}
	IEnumerator SwitchPullCoroutine() {
		_mouseReleased = false;
		_startPosition = Input.mousePosition;
		_currentPosition = _startPosition;

		// Loop until either our mouse goes up of we've moved enough to toggle the switch.  
		while(!HasPassedThreshold()) {

			// If we've released the mouse, just go ahead and leave the coroutine. 
			if(_mouseReleased) {
				StopRunningCoroutine();
			}

			yield return null;
			_currentPosition = Input.mousePosition;
		}

		// Flip the image and invoke our event. 
		onSwitchPulled.Invoke();
		transformToSFlip.localScale = new Vector3(1, -1, 1);	

		// Wait for reset
		yield return new WaitForSeconds(timeToReset);
		transformToSFlip.localScale = new Vector3(1, 1, 1);

		// We're done. 
		StopRunningCoroutine();
	}

	public void OnPointerUp(PointerEventData _) => _mouseReleased = true;

	public void OnPointerDown(PointerEventData _) {
		// If we're not already cooling down from our previous pull, start a new coroutine. 
		if(runningCoroutine == null) {
			runningCoroutine = SwitchPullCoroutine();
			StartCoroutine(runningCoroutine);
		}
	}


}
