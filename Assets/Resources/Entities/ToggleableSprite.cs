using UnityEngine;

public class ToggleableSprite : UIComponentWithQueueableActions
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Enable()
    {
        EnqueueAction(() => _spriteRenderer.enabled = true);
    }

    public void Disable()
    {
        EnqueueAction(() => _spriteRenderer.enabled = false);
    }

    public void Toggle(bool enabled)
    {
        EnqueueAction(() => _spriteRenderer.enabled = enabled);
    }
}
