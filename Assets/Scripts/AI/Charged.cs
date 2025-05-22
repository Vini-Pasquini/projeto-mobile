using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/Charged")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "Charged", message: "[Agent] charged", category: "Events", id: "7e6aa3c08e529f7483d5f782ddd86817")]
public partial class Charged : EventChannelBase
{
    public delegate void ChargedEventHandler(GameObject Agent);
    public event ChargedEventHandler Event; 

    public void SendEventMessage(GameObject Agent)
    {
        Event?.Invoke(Agent);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<GameObject> AgentBlackboardVariable = messageData[0] as BlackboardVariable<GameObject>;
        var Agent = AgentBlackboardVariable != null ? AgentBlackboardVariable.Value : default(GameObject);

        Event?.Invoke(Agent);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        ChargedEventHandler del = (Agent) =>
        {
            BlackboardVariable<GameObject> var0 = vars[0] as BlackboardVariable<GameObject>;
            if(var0 != null)
                var0.Value = Agent;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as ChargedEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as ChargedEventHandler;
    }
}

