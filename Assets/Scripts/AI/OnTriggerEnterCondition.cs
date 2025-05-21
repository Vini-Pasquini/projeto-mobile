using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "OnTriggerEnter", story: "[TargetCollider] entered [collider]", category: "Conditions", id: "74dcd611a71abe5c9ed80e75c8dadea0")]
public partial class OnTriggerEnterCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Collider2D> TargetCollider;
    [SerializeReference] public BlackboardVariable<Collider2D> Collider;

    public override bool IsTrue()
    {
        if (Physics2D.IsTouching(Collider, TargetCollider))
            return true;
        return false;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
