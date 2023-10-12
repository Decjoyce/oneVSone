using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Health")]
public class Powerup_Health : PowerupEffect
{
    [SerializeField]
    string heatlhType;

    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerHealth>().powerupType = heatlhType;
    }

    public override void Remove(GameObject target)
    {
        target.GetComponent<PlayerHealth>().powerupType = null;
    }
}
