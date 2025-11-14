using UnityEngine;
using UnityEngine.UI;

public class ExpSystem : MonoBehaviour
{
    [SerializeField] private int _value = 1;
    void Start()
    {
        EventBus.Instance.Subscribe<EndSignal>(OnEndSignal);
    }

    public void OnEndSignal(EndSignal signal){
        Destroy(gameObject);
    }
    void OnDestroy()
    {
        EventBus.Instance.Unsubscribe<EndSignal>(OnEndSignal);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var playerExp = other.GetComponentInParent<PlayerExperience>();
        if (playerExp == null)
            playerExp = other.GetComponentInParent<PlayerExperience>();
        if (playerExp != null)
        {
            playerExp.GetExp(_value);
            Destroy(gameObject);
        }
    }
}