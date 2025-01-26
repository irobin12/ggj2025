using System;
using UnityEngine;

public class WrapScreen : MonoBehaviour
{
    public Action StartClicked;
    
    public void HandleStartClicked()
    {
        StartClicked?.Invoke();
    }
}