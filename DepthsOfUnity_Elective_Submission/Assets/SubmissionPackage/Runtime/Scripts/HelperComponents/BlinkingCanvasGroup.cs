using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class BlinkingCanvasGroup : MonoBehaviour
{
    public float BlinkSpeed => blinkSpeed;
    [SerializeField] private float blinkSpeed = 1f;

    private CanvasGroup canvasGroup;
    private float timer;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>(); 
    }

    private void Update()
    {
        timer += Time.deltaTime;
        canvasGroup.alpha = (Mathf.Sin(timer * blinkSpeed) + 1) / 2 ;
    }
}
