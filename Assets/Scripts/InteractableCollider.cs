using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public abstract class InteractableCollider : MonoBehaviour {

        [Space( 10 ), SerializeField]
        private LivingPerson living_person;
        public LivingPerson Living_person => living_person;
    }
}