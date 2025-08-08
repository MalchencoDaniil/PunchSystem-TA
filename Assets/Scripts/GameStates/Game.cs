using UnityEngine;

public abstract class Game : MonoBehaviour
{
    public virtual void Won() { }
    public virtual void Loss() { }
}