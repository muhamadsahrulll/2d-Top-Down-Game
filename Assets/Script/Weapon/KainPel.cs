using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KainPel : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;

    [SerializeField] private float swordAttackCD = .5f;

    private Transform weaponCollider;

    private Animator anim;


    private GameObject slashAnim;

    private void Awake()
    {

        anim = GetComponent<Animator>();

    }

    private void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;
    }



    private void Update()
    {
        MouseFollowWithOffset();
    }
    public void Attack()
    {
        ActiveWeapon.Instance.ToggleIsAttacking(true);
        anim.SetTrigger("PelAttack");
        weaponCollider.gameObject.SetActive(true);
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
        StartCoroutine(AttackCDRoutine());
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
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

            if (PlayerController.Instance.FacingLeft)
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

            if (PlayerController.Instance.FacingLeft)
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

        if (PlayerController.Instance.sprite.flipX)
        {
            // Player menghadap kiri (flipX true)
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            // Player menghadap kanan (flipX false)
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
