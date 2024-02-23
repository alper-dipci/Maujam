using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera vcam;
    public void takeDamage(int amount) { 
        health -= amount;
        if (health <= 0)
            die();
    }
    private void die()
    {
        Debug.Log("PlayerDEAD");
    }
    public void shakeCam()
    {

    }

}

