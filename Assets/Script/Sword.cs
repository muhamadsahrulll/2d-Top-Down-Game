using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerControl playerControl;
    private Animator anim;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        anim = GetComponent<Animator>();
        playerControl = new PlayerControl();
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void Start()
    {
        playerControl.Combat.Attack.started += _ => Attack();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
    }

    private void MouseFollowWithOffset()
    {
        float angle = 0f; // Default angle

        if (playerController.sprite.flipX)
        {
            // Player menghadap kiri (flipX true)
            activeWeapon.transform.rotation = Quaternion.Euler(0, 180, angle);
        }
        else
        {
            // Player menghadap kanan (flipX false)
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
