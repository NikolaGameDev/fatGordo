using UnityEngine;
using System.Collections;

public class popGlow : MonoBehaviour
{
    [Header("Glow Settings")]
    [SerializeField] private Color flashColor = Color.yellow;
    [SerializeField] private float flashDuration;

    [Header("Pop Settings")]
    [SerializeField] private float popScaleMultiplier;

    private SpriteRenderer[] spriteRenderers;
    private Vector3 originalScale;
    private Color originalColor;

    private void Awake()
    {
        // Automatically get all child sprite renderers (rig parts)
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        if (spriteRenderers.Length > 0)
            originalColor = spriteRenderers[0].color;

        originalScale = transform.localScale;
    }

    public void TriggerCollectableEffect()
    {
        StopAllCoroutines();
        StartCoroutine(GlowAndPop());
    }

    private IEnumerator GlowAndPop()
    {
        // Flash all parts
        foreach (var sr in spriteRenderers)
            sr.color = flashColor;

        // Pop scale
        transform.localScale = originalScale * popScaleMultiplier;

        yield return new WaitForSeconds(flashDuration);

        // Reset all parts
        foreach (var sr in spriteRenderers)
            sr.color = originalColor;

        transform.localScale = originalScale;
    }
}
