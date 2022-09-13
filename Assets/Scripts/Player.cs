using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
    //[SerializeField] private GameObject camera;
    [SerializeField] private GameObject playerObj;
    private CharacterController cc;

    private PlayerCombat combo;

    //Movement
    [SerializeField] private float moveSpeed;
    private float horizontal;
    private float vertical;
    bool moveInput;
    Vector3 v;
    Vector3 h;
    public Vector3 movement;
    private float gravity = 9.8f;

    //Animator
    private Animator anim;

    //Rotation
    [SerializeField] private LayerMask mask;
    private Vector3 mousePos;

    private void Awake()
    {
        anim = playerObj.GetComponent<Animator>();
        combo = playerObj.GetComponent<PlayerCombat>();
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInput();
        Movement();
    }

    private void FixedUpdate()
    {
        Rotation();
    }

    void GetInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        v = vertical * UnityEngine.Camera.main.transform.up;
        h = horizontal * UnityEngine.Camera.main.transform.right;

        v.y = 0;
        h.y = 0;

        movement = (h + v) * 2;
    }

    void Movement()
    {
        Vector3 localMove = playerObj.transform.InverseTransformDirection(movement);

        anim.SetFloat("forward", localMove.z);
        anim.SetFloat("sideways", localMove.x);

        if (localMove.x > 0.2 || localMove.x < -0.2) moveSpeed = 2f;
        else if (localMove.z > 0.2) moveSpeed = 3f;
        else if (localMove.z < -0.2) moveSpeed = 1.5f;

        if (!cc.isGrounded) movement.y -= gravity * Time.deltaTime;

        if (!combo.isAttacking) cc.Move(movement * moveSpeed * Time.deltaTime);
    }

    void Rotation()
    {
        Ray r = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(r, out hit, mask))
        {
            mousePos = new Vector3(hit.point.x, 0f, hit.point.z);
        }

        playerObj.transform.LookAt(mousePos);

        playerObj.transform.eulerAngles = new Vector3(0f, playerObj.transform.eulerAngles.y, playerObj.transform.eulerAngles.z);
    }
}
