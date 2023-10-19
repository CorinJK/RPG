using UnityEngine;

namespace Scripts.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            if (currentAction == action) return;

            if (currentAction != null)
            {
                currentAction.Cancel();
                Debug.Log("Cancelling " + currentAction);
            }

            currentAction = action;
        }
    }
}