using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPlayer : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerController playerController;

    private void Awake()
    {
        playerHealth = gameObject.GetComponent<PlayerHealth>();
        playerController = gameObject.GetComponent<PlayerController>();
    }
}
