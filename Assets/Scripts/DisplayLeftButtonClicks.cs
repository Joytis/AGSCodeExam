using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DisplayLeftButtonClicks : MonoBehaviour
{
    TextMeshProUGUI _textMesh;
    public SimpleGameState state;

    void Awake() => _textMesh = GetComponent<TextMeshProUGUI>();
    
    // I know that this will make the garbage collector cry, but it's super simple for now. 
    void Update() => _textMesh.text = state.LeftButtonClicks.ToString();
}
