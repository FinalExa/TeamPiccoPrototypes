using UnityEngine;
public abstract class State
{
    public virtual void Start()
    {
        return;
    }
    public virtual void StateUpdate()
    {
        return;
    }
    public virtual void Exit()
    {
        return;
    }
    public virtual void Collisions(Collision collision)
    {
        return;
    }
}
