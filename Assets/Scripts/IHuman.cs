using UnityEngine;

namespace NetyaginSergey.TestFor1C {

    public interface IPerson {

        /// <summary>
        /// Attacks an opponent.
        /// </summary>
        void Attacks();


        /// <summary>
        /// Damages a pesron.
        /// </summary>
        void Damaged();


        /// <summary>
        /// Kills a person.
        /// </summary>
        void Killed();
    }
}