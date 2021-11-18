using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapontype { Pistol, ShotGun, AssaultRifle, Sniper }
public class WeaponManager : Singleton<WeaponManager>
{
    [SerializeField]
    public WeaponBase[] playerWeapons;
    private int currentWeaponIndex;
    public Weapontype currentWeapon;
    [HideInInspector]
    public WeaponBase selectedWeapon;
    public IWeapon interactble;
    // Start is called before the first frame update
    void Start()
    {
        currentWeaponIndex = (int)currentWeapon;
        selectedWeapon = playerWeapons[currentWeaponIndex];
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{

        //    Fire();
        //}
    }

    public void Fire()
    {
        RaycastHit hit;
        Vector2 rayCastOrigin = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(rayCastOrigin);
        if(Physics.Raycast(ray.origin,ray.direction,out hit)){
           Debug.Log( hit.collider.gameObject.name);
        }
        selectedWeapon.PlayFireSound();
        selectedWeapon.ShowFireEffect();

    }
    void UpdateBulletsUI()
    {

    }


}
