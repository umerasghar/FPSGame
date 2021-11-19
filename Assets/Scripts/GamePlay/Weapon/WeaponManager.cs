using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Weapontype { Pistol, ShotGun, AssaultRifle, Sniper }
public class WeaponManager : Singleton<WeaponManager>
{
    [Header("UI References")]
    public Text ammoText;
    [SerializeField]
    public WeaponBase[] playerWeapons;
    private int currentWeaponIndex;
    public Weapontype currentWeapon;
    [HideInInspector]
    public WeaponBase selectedWeapon;
    public IWeapon interactble;
    private bool isShortAim = false;
    IDamage playerInteractable, enemyInteractable;
    // Start is called before the first frame update
    void Start()
    {
        currentWeaponIndex = (int)currentWeapon;
        selectedWeapon = playerWeapons[currentWeaponIndex];
        playerInteractable = GameObject.FindGameObjectWithTag("Player").GetComponent<IDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isShortAim)
        {
            selectedWeapon.ShortAimSight(true);
       
        }
        else
        {
            selectedWeapon.ShortAimSight(false);
        }

        //if (Input.GetMouseButtonDown(0))
        //{

        //    Fire();
        //}
    }

    public void Fire()
    {
   
        if (!selectedWeapon.canFire)
        {
           
            RaycastHit hit;
            Vector2 rayCastOrigin = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray = Camera.main.ScreenPointToRay(rayCastOrigin);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                if (hit.collider.gameObject.tag == "Enemy")
                {

                    enemyInteractable = hit.collider.gameObject.GetComponent<IDamage>();
                    playerInteractable.ApplyDamage(selectedWeapon.damage, enemyInteractable);
                }
            }
            selectedWeapon.PlayFireSound();
            selectedWeapon.ShowFireEffect();
            selectedWeapon.UpdateBullets();
            UpdateBulletsUI();
        }
        Debug.Log("canFire" + selectedWeapon.canFire);

    }
    public void Reload()
    {
        selectedWeapon.isReloading = true;
        selectedWeapon.canFire = true;
        StartCoroutine(WaitForReload());
    }
    IEnumerator WaitForReload()
    {
        
            yield return new WaitForSeconds(0.5f);
        selectedWeapon.UpdateBullets();
        UpdateBulletsUI();
        selectedWeapon.canFire = false;
        selectedWeapon.isReloading = false;

    }
    void UpdateBulletsUI()
    {
        ammoText.text = selectedWeapon.loadedBullets + "/" + selectedWeapon.bulletsLeft;
    }
    public void ShortAimTrigger()
    {
        isShortAim = !isShortAim;
    }

}
