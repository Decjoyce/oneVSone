using UnityEngine;

public abstract class PowerupEffect : ScriptableObject
{
    public GameObject visualEffect_P1, visualEffect_P2;
    public abstract void Apply(GameObject target);
    public abstract void Remove(GameObject target);
}
