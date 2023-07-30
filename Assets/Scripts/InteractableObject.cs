using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public abstract class LivingPerson : MonoBehaviour {

        [Space( 10 ), SerializeField, Range( 0f, 1f )]
        protected float health = 1;
        public float Health => health;
    }
}