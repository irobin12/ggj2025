using UnityEngine;

public static class Inputs
{
    public static KeyCode[] moveLeft;
    public static KeyCode[] moveRight;
    public static KeyCode[] restart;

    public static void Set(InputData inputData)
    {
        moveLeft = inputData.MoveLeft;
        moveRight = inputData.MoveRight;
        restart = inputData.Restart;
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