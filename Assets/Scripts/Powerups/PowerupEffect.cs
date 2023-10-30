using UnityEngine;
using UnityEngine.UI;

public abstract class PowerupEffect : ScriptableObject
{
    public string displayName;
    public Sprite visualEffect;
    public Texture powerupArt;
    public abstract void Apply(GameObject target);
    public abstract void Remove(GameObject target);
}
