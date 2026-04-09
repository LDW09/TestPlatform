using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInputHolder : MonoBehaviour
{
    [field: SerializeField] public HoldButton Left { get; private set; }
    [field: SerializeField] public HoldButton Right { get; private set; }
    [field: SerializeField] public Button Jump { get; private set; }
}
