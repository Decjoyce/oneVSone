using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Movement")]
public class Powerup_Movement : PowerupEffect
{
    [SerializeField]
    float speed;

    public override void Apply(GameObject target)
    {
        target.GetComponent<playermovement>().moveSpeed = speed;
    }

    public override void Remove(GameObject target)
    {
        playermovement mov = target.GetComponent<playermovement>();
        mov.moveSpeed = mov.ogSpeed;
    }
}
