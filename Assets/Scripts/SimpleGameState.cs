using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: I was having a bit of fun here and decided to implement the game state as a scriptable object. Worked out well enough, 
//       and allowed me to just reload the scene without worrying about too much. 
[CreateAssetMenu(fileName = "gameState_newGameState", menuName = "GameState")]
public class SimpleGameState : ScriptableObject
{
    uint _leftButtonClicks = 0;
    uint _rightThingToggles = 0;

    public uint LeftButtonClicks => _leftButtonClicks;
    public uint RightThingToggles => _rightThingToggles;
    public uint TotalCount => LeftButtonClicks + RightThingToggles;

    void CheckGameEnded() {
        if(TotalCount >= 10) {
            GameEnded?.Invoke();
        }
    }

    public void IncrementLeftButtonClicks() {
        _leftButtonClicks += 1;
        CheckGameEnded();
    } 

    public void IncrementRightThingToggles() {
        _rightThingToggles += 1;
        CheckGameEnded();
    } 

    public event System.Action GameEnded;

    public void Reset() {
        _leftButtonClicks = 0;
        _rightThingToggles = 0;
    }
}
