using Oculus.Platform;
using UnityEngine;
public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set; }

    protected float startTime;
    public float time => Time.time - startTime;


    protected NPCManager core;
    protected Rigidbody body => core.body;
    protected Animator animator => core.animator;

    public StateMachine machine;

    public StateMachine parent;
    public State state => machine.state;


    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }

    public virtual void DoAll()
    {
        Do();

        state?.DoAll();
    }

    public virtual void FixedDoAll()
    {
        FixedDo();

        state?.FixedDoAll();

    }

    protected virtual void Set(State newState, bool forceReset = false)
    {
        machine.Set(newState, forceReset);
    }
    public virtual void SetCore(NPCManager _core)
    {
        machine = new StateMachine();
        core = _core;
    }

    public virtual void Initialise(StateMachine _parent)
    {
        parent = _parent;
        isComplete = false;
        startTime = Time.time;
    }
}
