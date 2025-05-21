using UnityEngine;

public class AthenaPower : PowerUpProp
{
    private UIController _uiController;

    private void Start()
    {
        this._uiController = GetComponent<UIController>();
        this._uiController.ToggleCardSigns(true);
    }

    public override void Update()
    {
        base.Update();
    }

    private void OnDestroy()
    {
        this._uiController.ToggleCardSigns(false);
    }
}
