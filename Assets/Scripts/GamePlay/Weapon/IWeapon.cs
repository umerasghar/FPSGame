using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void PlayFireSound();
  void ShowFireEffect();
    void UpdateBullets();
    void ReloadAnimation();
    void ShortAimSight(bool can);
}

