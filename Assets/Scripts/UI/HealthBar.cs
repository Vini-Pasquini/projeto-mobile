using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Sprite health0;
    [SerializeField] private Sprite health1;
    [SerializeField] private Sprite health2;
    [SerializeField] private Sprite health3;
    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
        img.sprite = health3;
    }

    public void SetHealth(int health)
    {
        switch (health)
        {
            case 0:
                img.sprite = health0;
                break;
            case 1:
                img.sprite = health1;
                break;
            case 2:
                img.sprite = health2;
                break;
            default:
                img.sprite = health3;
                break;
        }
    }
}
