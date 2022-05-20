using UnityEngine;

namespace CrowdT.Level
{
    public class TentView : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        
        private static readonly int IsClose = Animator.StringToHash("IsClose");

        public void OpenDoor()
        {
            anim.SetBool(IsClose, false);
        }

        public async void CloseDoor()
        {
            await new WaitForSeconds(1f);
            
            anim.SetBool(IsClose, true);
        }
    }
}