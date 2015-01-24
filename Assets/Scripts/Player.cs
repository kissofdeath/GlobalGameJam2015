using UnityEngine;
using System.Collections;

public class Player  {
    private float hp, maxhp;

    static Player instance = null;

    public static Player Instance()
    {
        if (instance == null)
            instance = new Player();

        return instance;
    }

	
	private Player () 
    {
        hp = 50;
        maxhp = 100;
	}
	


    public void Heal(float delta)
    {
        hp += delta;
        Mathf.Clamp(hp, 0f, 100f);

        Debug.Log("healed player");
    }
}
