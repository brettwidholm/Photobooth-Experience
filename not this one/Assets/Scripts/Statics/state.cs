using UnityEngine;

public static class State
{
    private static int state = 0;

    public static void IncrementState()
    {
        state++; //add in clause to cap state count later once we figure out total number of states for sure
    }

    public static void DecrementState()
    {
        if (state > 0){ //will only decrement if state counter is greater than 0, otherwise we are at state = 0
            state--;
        }
    
    }

    public static void StateReset()
    {
        state = 0; //resets state to 0
    }

    public static int GetState()
    {
        return state;
    }
}
