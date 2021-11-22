using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void PlayFireSound();
    void PlayReloadSound();
  void ShowFireEffect();
    void UpdateBullets();
    void ShortAimSight(bool can);
}

