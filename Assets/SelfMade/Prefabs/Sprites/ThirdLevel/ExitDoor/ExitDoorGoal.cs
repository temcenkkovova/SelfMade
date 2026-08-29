using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public sealed class ExitDoorGoal : MonoBehaviour
{
    [SerializeField] private LevelGoal levelGoal;
    [SerializeField] private SpriteRenderer doorRenderer;
    [SerializeField, Min(0f)] private float pulseSpeed = 2f;
    [SerializeField, Range(0f, 0.25f)] private float pulseAmount = 0.08f;

    private Color baseColor;
    private bool reached;

    private void Awake()
    {
        if (levelGoal == null)
            levelGoal = FindFirstObjectByType<LevelGoal>();

        if (doorRenderer == null)
            doorRenderer = GetComponentInChildren<SpriteRenderer>();

        if (doorRenderer != null)
            baseColor = doorRenderer.color;
    }

    private void Update()
    {
        if (doorRenderer == null) return;

        float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        doorRenderer.color = new Color(
            Mathf.Clamp01(baseColor.r * pulse),
            Mathf.Clamp01(baseColor.g * pulse),
            Mathf.Clamp01(baseColor.b * pulse),
            baseColor.a);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (reached || !other.CompareTag("Player")) return;

        reached = true;
        if (levelGoal == null)
            levelGoal = FindFirstObjectByType<LevelGoal>();

        levelGoal?.ReachedGoal();
    }
}
