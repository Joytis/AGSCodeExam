using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasGameStateManipulators : MonoBehaviour
{
    public SimpleGameState state;
    public GameObject[] gameObjectsToDisableOnOver;

    void Awake() => state.Reset();

	void OnEnable() => state.GameEnded += OnGameEnded;
	void OnDisable() => state.GameEnded -= OnGameEnded;

    public void IncrementLeftButtonClicks() => state.IncrementLeftButtonClicks();
    public void IncrementRightThingToggles() => state.IncrementRightThingToggles();

    // For simplicity sake, just reset the scene and let the engine take care of the reset. 
    public void ResetGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    // Exit the game depending on running env. 
    public void ExitGame() {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
    }

    // Just Enable our 'game ended' objects once the game is over. 
    public void OnGameEnded() {
    	foreach(var obj in gameObjectsToDisableOnOver) {
    		obj.SetActive(true);
    	}
    }
}
