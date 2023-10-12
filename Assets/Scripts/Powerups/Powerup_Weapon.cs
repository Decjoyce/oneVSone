using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Weapon")]
public class Powerup_Weapon : PowerupEffect
{
    [SerializeField]
    string weaponType;

    [SerializeField]
    float fireDelay, fireForce;

    [SerializeField]
    GameObject bulletPrefab_P1, bulletPrefab_P2;

    public override void Apply(GameObject target)
    {
        Weapon weap = target.GetComponent<Weapon>();
        weap.fireDelay = fireDelay;
        weap.fireForce = fireForce;
        weap.weaponType = weaponType;
        if(target.CompareTag("Player1"))
            weap.currentBulletPrefab = bulletPrefab_P1;
        else if(target.CompareTag("Player2"))
            weap.currentBulletPrefab = bulletPrefab_P2;
    }

    public override void Remove(GameObject target)
    {
        Weapon weap = target.GetComponent<Weapon>();
        weap.ResetWeapon();
    }

}