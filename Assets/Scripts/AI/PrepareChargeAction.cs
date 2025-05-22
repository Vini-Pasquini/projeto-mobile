using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PrepareCharge", story: "[Agent] prepares charge to [Target]", category: "Action/Animation", id: "9f26872e853966f3b5cd8c92eb805a66")]
public partial class PrepareChargeAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    private Animator myAnimator;
    private Vector3 agentPos;
    private Vector3 targetPos;

    protected override Status OnStart()
    {
        Debug.Log("start do charge");
        if (Agent.Value == null || Target.Value == null)
        {
            Debug.Log("Charge falhou");
            return Status.Failure;
        }


        return Initialize();
    }

    protected override Status OnUpdate()
    {
        if (targetPos.x < agentPos.x)
            myAnimator.SetBool("ChargeLeft", true);
        myAnimator.SetBool("PrepareCharge", true);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }

    protected override void OnDeserialize()
    {
        Initialize();
    }

    private Status Initialize()
    {
        myAnimator = Agent.Value.GetComponentInChildren<Animator>();
        agentPos = Agent.Value.transform.position;
        targetPos = Target.Value.transform.position;

        return Status.Running;
    }
}

