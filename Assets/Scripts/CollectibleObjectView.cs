using UnityEngine;

public class CollectibleObjectView : MonoBehaviour
{
    [SerializeField] private ParticleSystem collectEffect;
    private CollectibleObjectModel model;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void Initialize(CollectibleObjectModel model)
    {
        this.model = model;
        spriteRenderer.sprite = model.Sprite;
    }

    private void OnMouseDown()
    {
        FindObjectOfType<GameViewModel>().OnObjectCollected(model.ID);
        PlayCollectEffect();
    }

    private void PlayCollectEffect()
    {
        if (collectEffect != null)
        {
            collectEffect.Play();
        }
        if (animator != null)
        {
            animator.SetTrigger("Collect");
        }
    }
}
