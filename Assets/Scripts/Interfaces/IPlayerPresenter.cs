using UnityEngine;
using UnityEngine.Events;

namespace Interfaces
{
    public interface IPlayerPresenter
    {
        UnityEvent OnGameOverTriggered { get; }

        void ResetPlayer();
    }
}