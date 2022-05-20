using System.Threading.Tasks;
using UnityEngine;

namespace CrowdT
{
    public abstract class CharacterInput : MonoBehaviour, IObserver
    {
        protected bool turnMade;
        
        protected CharacterInputView view;

        public virtual void StartTurn(){}
        
        public abstract Task<bool> IsExecuted();

        public virtual void UpdateObserver()
        {
            turnMade = true;
        }
    }
}