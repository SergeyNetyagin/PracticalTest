using System;
using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public class EventControl : MonoBehaviour {

        public Action OnAnimationComplete;


		/// <summary>
		/// Start is called before the first frame update.
		/// </summary>
		private void Start() {
        
        }


        /// <summary>
        /// Calls when the animation has been complete.
        /// </summary>
        public void AnimationEvent_AnimationComplete() { 
            
            if( !enabled ) { 
            
                return;
            }

            OnAnimationComplete?.Invoke();
        }
    }
}