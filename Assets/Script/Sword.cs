using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private Transform weaponColider;
    [SerializeField] private float swordAttackCD = .5f;


    private Animator anim;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private GameObject slashAnim;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        anim = GetComponent<Animator>();
        
    }

    

    private void Update()
    {
        MouseFollowWithOffset();
    }

    public void Attack()
    {
        // isAttacking = true;
        anim.SetTrigger("Attack");
        weaponColider.gameObject.SetActive(true);
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
        StartCoroutine(AttackCDRoutine());
    }

    public void DoneAttackingAnimEvent()
    {
        weaponColider.gameObject.SetActive(false);
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(swordAttackCD);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }



    public void SwingUpFlipAnim()
    {
        if (slashAnim != null)
        {
            slashAnim.transform.rotation = Quaternion.Euler(-180, 0, 0);

            if (playerController.FacingLeft)
            {
                slashAnim.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                slashAnim.GetComponent<SpriteRenderer>().flipX = false; // Atau biarkan seperti ini sesuai kebutuhan
            }
        }
    }

    public void SwingDownFlipAnim()
    {
        if (slashAnim != null)
        {
            slashAnim.transform.rotation = Quaternion.Euler(0, 0, 0);

            if (playerController.FacingLeft)
            {
                slashAnim.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                slashAnim.GetComponent<SpriteRenderer>().flipX = false; // Atau biarkan seperti ini sesuai kebutuhan
            }
        }
    }

    private void MouseFollowWithOffset()
    {
        float angle = 0f; // Default angle

        if (playerController.sprite.flipX)
        {
            // Player menghadap kiri (flipX true)
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponColider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            // Player menghadap kanan (flipX false)
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponColider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
