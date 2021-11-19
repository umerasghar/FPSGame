using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void PlayFireSound();
  void ShowFireEffect();
    void UpdateBullets();
    void ShortAimSight(bool can);
}

