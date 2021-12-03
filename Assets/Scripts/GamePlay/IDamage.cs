using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    void TakeDamage(float hitPoint);
    void ApplyDamage(float hitPoint,IDamage objectToDamage);

    void CheckCharacterDeath();
}
