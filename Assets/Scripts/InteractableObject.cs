using System;
using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public abstract class InteractableObject : MonoBehaviour {

        public Action<InteractableObject> OnDamaged;
        public Action<InteractableObject> OnKilled;

        [Space( 10 ), SerializeField, Range( 0f, 1f )]
        protected float health = 1;
        public float Health => health;
    }
}