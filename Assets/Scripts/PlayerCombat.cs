using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kryz.CharacterStats;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject characterMenu;
    [SerializeField] private GameObject statTooltip;
    [SerializeField] private GameObject itemTooltip;
    private CharacterController cc;
    private Player player;

    CharacterStat characterStat;

    private Animator anim;

    //Attack
    private int clicks = 0;
    bool canClick = true;
    public bool isAttacking;
    private bool translate;
    private bool isDodging;
    private bool canDodge = true;
    private bool canMove = true;

    private void Awake()
    {
        cc = holder.GetComponent<CharacterController>();
        player = holder.GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        GetInput();
        if (translate) cc.Move(player.movement * 0.3f * (Time.deltaTime * 1.5f));
        if (isDodging) Avoid();
    }

    void GetInput()
    {
        if (canMove)
        {
            if (Input.GetMouseButtonDown(0)) Attack();
            if (Input.GetMouseButtonDown(1)) Cast();
            if (Input.GetKeyDown(KeyCode.E)) Fireball();
            if (Input.GetKeyDown(KeyCode.Space) && canDodge) Dodge();
            if (Input.GetKeyDown(KeyCode.LeftShift)) PowerUp();
        }
            if (Input.GetKeyDown(KeyCode.Tab)) OpenInventory();
    }

    void Attack()
    {
        if (canClick) clicks++;

        if (clicks == 1)
        {
            anim.SetInteger("attack", 1);
            isAttacking = true;
            translate = true;
            canDodge = false;
        }
    }

    void Combo()
    {
        canClick = false;

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("CrossPunch") && clicks == 1)
        {
            Stop();
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("CrossPunch") && clicks >= 2)
        {
            anim.SetInteger("attack", 2);
            canClick = true;
            canDodge = false;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("HookPunch"))
        {
            Stop();
        }
    }

    void Cast()
    {
        anim.SetInteger("attack", 3);
        isAttacking = true;
        canDodge = false;
    }

    void Fireball()
    {
        anim.SetInteger("attack", 4);
        isAttacking = true;
        canDodge = false;
    }

    void SpawnProjectile()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Casting"))
        {
            GameObject projectile = PoolManager.instance.GetPooledObject("missiles");

            if (projectile != null)
            {
                projectile.transform.position = spawnPoint.transform.position;
                projectile.transform.rotation = transform.rotation;
                projectile.SetActive(true);
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Fireball"))
        {
            GameObject projectile = PoolManager.instance.GetPooledObject("fireballs");

            if (projectile != null)
            {
                projectile.transform.position = spawnPoint.transform.position;
                projectile.transform.rotation = transform.rotation;
                projectile.SetActive(true);
            }
        }
    }

    void Dodge()
    {
        anim.SetInteger("attack", 5);
        isAttacking = true;
        isDodging = true;
    }

    void Avoid()
    {
        cc.Move(player.movement * 2f * (Time.deltaTime * 1.5f));
        holder.GetComponent<CharacterController>().radius = 0f;
    }

    void PowerUp()
    {
        anim.SetInteger("attack", 6);
        isAttacking = true;
        canDodge = false;
    }

    void Stop()
    {
        holder.GetComponent<CharacterController>().radius = 0.5f;
        anim.SetInteger("attack", 0);
        canClick = true;
        clicks = 0;
        translate = false;
        isAttacking = false;
        isDodging = false;
        canDodge = true;
    }

    void OpenInventory()
    {
        if (!characterMenu.activeInHierarchy)
        {
            canMove = false;
            isAttacking = true;
            characterMenu.SetActive(true);
        }
        else
        {
            characterMenu.SetActive(false);
            itemTooltip.SetActive(false);
            statTooltip.SetActive(false);
            
            canMove = true;
            isAttacking = false;
        }

    }
}
