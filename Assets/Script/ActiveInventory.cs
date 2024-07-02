using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int activeSlotIndexNum = 0;
    private PlayerControl playerControl;

    public List<WeaponInfo> allWeaponInfos; // Tambahkan daftar semua WeaponInfo
    public GameObject belumBeli;

    private void Awake()
    {
        playerControl = new PlayerControl();
    }

    private void Start()
    {
        // Muat status pembelian senjata
        foreach (Transform inventorySlot in transform)
        {
            InventorySlot slot = inventorySlot.GetComponentInChildren<InventorySlot>();
            if (slot != null && slot.GetWeaponInfo() != null)
            {
                WeaponInfo weaponInfo = slot.GetWeaponInfo();
                ScoreManager.Instance.LoadWeaponPurchase(weaponInfo, 1);
                ScoreManager.Instance.LoadWeaponPurchase(weaponInfo, 2);
                ScoreManager.Instance.LoadWeaponPurchase(weaponInfo, 3);
            }
        }

        foreach (WeaponInfo weaponInfo in allWeaponInfos)
        {
            ScoreManager.Instance.LoadWeaponPurchase(weaponInfo, 1);
            ScoreManager.Instance.LoadWeaponPurchase(weaponInfo, 2);
            ScoreManager.Instance.LoadWeaponPurchase(weaponInfo, 3);
        }

        playerControl.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    public void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighlight(numValue - 1);
    }

    private void ToggleActiveHighlight(int indexNum)
    {
        activeSlotIndexNum = indexNum;

        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);
        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        InventorySlot slot = transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>();
        if (slot != null)
        {
            WeaponInfo weaponInfo = slot.GetWeaponInfo();
            if (weaponInfo != null && weaponInfo.isPurchased)
            {
                GameObject weaponToSpawn = weaponInfo.weaponPrefab;
                GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);
                newWeapon.transform.parent = ActiveWeapon.Instance.transform;
                ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
            }
            else
            {
                Debug.Log("Senjata belum dibeli.");
                StartCoroutine(NotCompleted(2f));
                ActiveWeapon.Instance.WeaponNull();
            }
        }

    }

    IEnumerator NotCompleted(float seconds)
    {
        belumBeli.SetActive(true);
        yield return new WaitForSeconds(seconds);
        belumBeli.SetActive(false);
    }

}
