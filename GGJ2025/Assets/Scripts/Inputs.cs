using UnityEngine;

public static class Inputs
{
    public static KeyCode[] MoveLeft { get; private set; }
    public static KeyCode[] MoveRight{ get; private set; }
    public static KeyCode[] Up { get; private set; }
    public static KeyCode[] Down { get; private set; }
    public static KeyCode[] Restart{ get; private set; }
    public static KeyCode[] Menu{ get; private set; }

    public static void Set(InputData inputData)
    {
        MoveLeft = inputData.MoveLeft;
        MoveRight = inputData.MoveRight;
        Up = inputData.Up;
        Down = inputData.Down;
        Restart = inputData.Restart;
        Menu = inputData.Menu;
    }

    public static bool IsKeyUp(KeyCode[] keys)
    {
        foreach (var key in keys)
        {
            if (Input.GetKeyUp(key))
            {
                return true;
            }
        }
        
        return false;
    }

    public static bool IsKeyDown(KeyCode[] keys)
    {
        foreach (var key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }
        
        return false;
    }
}