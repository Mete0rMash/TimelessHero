using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    public int xpValue = 1;

    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private GameObject playerTransform;
    private Vector3 startingPosition;

    [SerializeField] private Collision hitbox;

    private void Start()
    {
        playerTransform = GameObject.Find("PlayerHolder").gameObject;
        startingPosition = transform.position;
    }


}
