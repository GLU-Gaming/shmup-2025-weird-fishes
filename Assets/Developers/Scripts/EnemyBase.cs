using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public abstract void Shoot();
    public abstract void Destroyed();
    public abstract void Spawn();

    public abstract void Damaged();
}
