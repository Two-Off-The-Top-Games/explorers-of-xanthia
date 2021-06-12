using System;

public class ItemAction
{
    public string Name;
    public Action Action;
    public ItemAction(string name, Action action)
    {
        Name = name;
        Action = action;
    }
}
