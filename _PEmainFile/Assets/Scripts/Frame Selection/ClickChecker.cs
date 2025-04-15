using UnityEngine;

public static class ClickChecker
{
    public static int selected = 0;

    public static void Start(){
        selected = 0;
    }


    public static void setSelected(int a){
        selected = a;

    }

    public static int getSelected(){
        return selected;

    }


        
}


