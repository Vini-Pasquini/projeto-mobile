using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayerDeathCheck", story: "[Player] health check", category: "Action", id: "3d1aadac07085a52cee341af38e7761c")]
public partial class PlayerDeathCheckAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<GameObject> GameOverPanel;
    [SerializeReference] public BlackboardVariable<bool> Still;
    private PlayerController playerController;
    private int playerHealth;

    protected override Status OnStart()
    {
        playerController = Player.Value.GetComponent<PlayerController>();
        playerHealth = playerController.GetHealth();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (playerHealth <= 0)
        {
            Still.Value = true;
            GameOverPanel.Value.SetActive(true);
        }
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

