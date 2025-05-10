using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/BattleInitiated")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "BattleInitiated", message: "[Target] has entered [Trigger]", category: "Events", id: "71e41a09a9ae79bce2f0711bc3e99c43")]
public partial class BattleInitiated : EventChannelBase
{
    public delegate void BattleInitiatedEventHandler(Collider2D Target, Collider2D Trigger);
    public event BattleInitiatedEventHandler Event; 

    public void SendEventMessage(Collider2D Target, Collider2D Trigger)
    {
        Event?.Invoke(Target, Trigger);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<Collider2D> TargetBlackboardVariable = messageData[0] as BlackboardVariable<Collider2D>;
        var Target = TargetBlackboardVariable != null ? TargetBlackboardVariable.Value : default(Collider2D);

        BlackboardVariable<Collider2D> TriggerBlackboardVariable = messageData[1] as BlackboardVariable<Collider2D>;
        var Trigger = TriggerBlackboardVariable != null ? TriggerBlackboardVariable.Value : default(Collider2D);

        Event?.Invoke(Target, Trigger);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        BattleInitiatedEventHandler del = (Target, Trigger) =>
        {
            BlackboardVariable<Collider2D> var0 = vars[0] as BlackboardVariable<Collider2D>;
            if(var0 != null)
                var0.Value = Target;

            BlackboardVariable<Collider2D> var1 = vars[1] as BlackboardVariable<Collider2D>;
            if(var1 != null)
                var1.Value = Trigger;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as BattleInitiatedEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as BattleInitiatedEventHandler;
    }
}

