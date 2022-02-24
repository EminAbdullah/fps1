using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int StartHealth = 100;
    private int CurrentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = StartHealth;
    }
    public int getHealth()
    {


        return CurrentHealth;
    }
    public void hit (int damage){

        CurrentHealth -= damage;
        if (CurrentHealth<=0)
        {
            CurrentHealth = 0;
           
        }
     }
    // Update is called once per frame
    void Update()
    {
        
    }
}
