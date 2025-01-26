using UnityEngine;

public class CreditsScreen : MonoBehaviour
{
    public void HandleBackClicked()
    {
        GameStatesManager.SetGameState(GameStatesManager.States.StartMenu);
    }
    
}