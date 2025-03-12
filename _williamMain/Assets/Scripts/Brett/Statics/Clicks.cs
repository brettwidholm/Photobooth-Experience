using UnityEngine;

//useless class, could be used later on maybe????
public static class Clicks
{
    private static bool click = false;

    public static bool HasClicked(){
        click = false;
        if (Input.GetMouseButtonDown(0) && !(click)){
            click = true;
            return true;
        }
        else{
            return false;
        }
    }

}
