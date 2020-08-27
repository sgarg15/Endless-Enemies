using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public float health;
    public float maxHealth;
    public MeshRenderer model;
    private CameraController camControl;

    private void Awake()
    {
        camControl = Object.FindObjectOfType<CameraController>();
    }

    public void Initiliaze(int _id, string _username)
    {
        id = _id;
        username = _username;
        health = maxHealth;

    }

    public void SetHealth(float _health)
    {
        health = _health;

        if (health <= 0f)
        {
            Die();
        }
    }
    public void Die()
    {
        model.enabled = false;
        camControl.SwitchCamera();
    }

    public void Respawn()
    {
        model.enabled = true;
        camControl.SwitchCamera();
        SetHealth(maxHealth);
    }
}
