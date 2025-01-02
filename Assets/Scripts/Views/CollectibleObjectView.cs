using UnityEngine;
using System.Collections;

public class CollectibleObjectView : MonoBehaviour
{
    [SerializeField] private GameObject collectFXPrefab;
    private CollectibleObjectModel model;
    private SpriteRenderer spriteRenderer;
    private GameViewModel gameViewModel;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameViewModel = FindObjectOfType<GameViewModel>();
    }

    public void Initialize(CollectibleObjectModel model)
    {
        this.model = model;
        spriteRenderer.sprite = model.Sprite;
    }

    private void OnMouseDown()
    {
        // Check if this object is in target objects before playing effect
        bool isTarget = gameViewModel.IsTargetObject(model.ID);
        if (isTarget)
        {
            PlayCollectEffect();
        }
        gameViewModel.OnObjectCollected(model.ID);
    }

    private void PlayCollectEffect()
    {
        // Particle effect
        if (collectFXPrefab != null)
        {
            GameObject fx = Instantiate(collectFXPrefab, transform.position, Quaternion.identity);
            Destroy(fx, 2f);
        }

        // Scale animation
    }


}