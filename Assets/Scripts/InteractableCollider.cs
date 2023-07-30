using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public abstract class InteractableCollider : MonoBehaviour {

        [Space( 10 ), SerializeField]
        private InteractableObject damaging_object;
        public InteractableObject Damaging_object => damaging_object;
    }
}