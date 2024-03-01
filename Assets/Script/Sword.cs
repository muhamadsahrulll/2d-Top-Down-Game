using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private Transform weaponColider;

    private PlayerControl playerControl;
    private Animator anim;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private GameObject slashAnim;

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
        weaponColider.gameObject.SetActive(true);

        // Pastikan slashAnimPrefab dan slashAnimSpawnPoint sudah diinisialisasi di Inspector Unity
        if (slashAnimPrefab != null && slashAnimSpawnPoint != null)
        {
            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.SetParent(this.transform.parent);
        }
    }

    public void DoneAttackingAnimEvent()
    {
        weaponColider.gameObject.SetActive(false);
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
