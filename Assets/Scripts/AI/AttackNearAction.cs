using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AttackNear", story: "[Agent] hits [Target]", category: "Action", id: "5953008ba207720092b2bb401b850bd3")]
public partial class AttackNearAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<PlayerController> Target;
    GameObject player;

    protected override Status OnStart()
    {
        player = GameObject.FindWithTag("Player");
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        player.GetComponent<PlayerController>().TakeHit();
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

