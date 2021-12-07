﻿using System.Collections;
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
    IDamage characterInteractable;
    float readyToFire = 0;
    public static bool isLastKill;
    Transform lastBullet;
    Transform bulletCamera;
    Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        currentWeaponIndex = (int)currentWeapon;
        selectedWeapon = playerWeapons[currentWeaponIndex];
       // playerInteractable = GameObject.FindGameObjectWithTag("Player").GetComponent<IDamage>();
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
       // Fire();
        //if (Input.GetMouseButtonDown(0))
        //{

        //    Fire();
        //}
    }
    private void FixedUpdate()
    {
        if (isLastKill)
        {
           
            lastBullet.Translate(Vector3.forward * Time.deltaTime * 2f);
            bulletCamera.localPosition = lastBullet.localPosition + cameraOffset;
        }
    }
    public void Fire()
    {
   
        if (!selectedWeapon.canFire&&Time.time>=readyToFire)
        {
           
            RaycastHit hit;
        //    int layerMask = 1 << LayerMask.NameToLayer("IgnoreRaycast");
          //  Debug.Log(layerMask);
            Vector2 rayCastOrigin = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray = Camera.main.ScreenPointToRay(rayCastOrigin);
            if (Physics.Raycast(ray.origin, ray.direction,out hit))
            {
                if (hit.collider.gameObject.tag == "Enemy")
                {

                    characterInteractable = hit.collider.gameObject.transform.root.gameObject.GetComponent<IDamage>();
                    characterInteractable.TakeDamage(selectedWeapon.damage);
                   // playerInteractable.ApplyDamage(selectedWeapon.damage, enemyInteractable);
                    characterInteractable.CheckCharacterDeath();
                    CreateBlood(hit.point);
                }
                else
                {
                    HitImapactParticle(hit.point, hit.normal);
                    GameObject bulletHole = Instantiate(selectedWeapon.bulletImpact, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                    bulletHole.transform.SetParent(hit.transform);
                    Destroy(bulletHole, 7f);
                }
            }
            selectedWeapon.PlayFireSound();
            selectedWeapon.ShowFireEffect();
            selectedWeapon.UpdateBullets();
            UpdateBulletsUI();
            readyToFire = Time.time + selectedWeapon.fireRate;
            Debug.Log(Time.time);
        }
        Debug.Log("canFire" + selectedWeapon.canFire);

    }
    public void Reload()
    {
        if (selectedWeapon.loadedBullets < selectedWeapon.startBullets)
        {
            selectedWeapon.isReloading = true;
            selectedWeapon.canFire = true;
            StartCoroutine(WaitForReload());
        }
    }
    IEnumerator WaitForReload()
    {
        selectedWeapon.ReloadAnimation();
            yield return new WaitForSeconds(4f);
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
    void CreateBlood(Vector3 pos)
    {
        GameObject blood = selectedWeapon.bloodEffect;
        GameObject bloodEffect = Instantiate(blood, pos, new Quaternion(0, 0, 0, 0));
        Destroy(bloodEffect, 3f);
    }

    void HitImapactParticle(Vector3 pos, Vector3 normal)
    {
        GameObject hitParticleEffect = Instantiate(selectedWeapon.hitParticle, pos, Quaternion.FromToRotation(Vector3.up, normal));
        Destroy(hitParticleEffect, 1f);
    }
    public void BulletTimeEffect(bool show)
    {
        if (show)
        {
            var bullet = Instantiate(selectedWeapon.bulletPrefab);
            bullet.transform.parent = selectedWeapon.bulletInstantiatePoint.parent;
            bullet.transform.localPosition = selectedWeapon.bulletInstantiatePoint.localPosition;
            lastBullet = bullet.transform;
            selectedWeapon.bulletTimeCamera.transform.parent = selectedWeapon.bulletTimeCameraPos.parent;
            bulletCamera = selectedWeapon.bulletTimeCamera.transform;
            bulletCamera.localPosition = selectedWeapon.bulletTimeCameraPos.localPosition;
            cameraOffset = bulletCamera.localPosition-lastBullet.localPosition;
            selectedWeapon.bulletTimeCamera.depth = 1;
            isLastKill = true;
        }
    }
}
