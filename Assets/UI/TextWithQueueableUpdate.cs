using UnityEngine.UI;

public class TextWithQueueableUpdate : UIComponentWithQueueableActions
{
    private Text _text;

    private void Awake()
    {
        _text = gameObject.GetComponentInChildren<Text>();
    }

    protected void EnqueueUpdateTextAction(UpdateTextAction updateTextAction)
    {
        EnqueueAction(() => _text.text = updateTextAction.UpdatedText);
    }
}
