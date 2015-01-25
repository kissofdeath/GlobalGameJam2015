using UnityEngine;
using System.Collections;

public class BulletPool : MonoBehaviour {

    int ArraySize = 500;
    GameObject[] BulletArray;
    GameObject MasterBullet;

	// Use this for initialization
	void Start () {
	    BulletArray = new GameObject[ArraySize];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
