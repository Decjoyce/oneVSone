using UnityEngine;
using UnityEngine.UI;

public abstract class PowerupEffect : ScriptableObject
{
    public GameObject visualEffect_P1, visualEffect_P2;
    public Texture powerupArt;
    public abstract void Apply(GameObject target);
    public abstract void Remove(GameObject target);
}
