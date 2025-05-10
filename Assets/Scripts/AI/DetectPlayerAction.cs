using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DetectPlayer", story: "[Agent] detects [Target]", category: "Action", id: "a0557b42a95b7661de15beddd77a0900")]
public partial class DetectPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<Collider2D> Agent;
    [SerializeReference] public BlackboardVariable<Collider2D> Target;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Physics2D.IsTouching(Agent, Target))
            return Status.Success;
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

