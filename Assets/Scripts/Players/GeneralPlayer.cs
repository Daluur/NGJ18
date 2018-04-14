using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPlayer : MonoBehaviour
{
    public PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = gameObject.GetComponent<PlayerHealth>();
    }
}
