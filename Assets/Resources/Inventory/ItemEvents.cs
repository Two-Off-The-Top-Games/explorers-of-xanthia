using System;

namespace Events.Inventory
{
    public class RegisterOutsideClickEvent : GlobalEvent<RegisterOutsideClickEvent>
    {
        public RegisterOutsideClickEvent(Action onOutsideClickAction) 
        {
            OnOutsideClickAction = onOutsideClickAction;
        }

        public Action OnOutsideClickAction;
    }
}