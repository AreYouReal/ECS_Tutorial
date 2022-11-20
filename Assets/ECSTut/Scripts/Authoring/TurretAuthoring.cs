using Unity.Entities;
using UnityEngine;

namespace ECSTut {
    public class TurretAuthoring : MonoBehaviour { }

    class TurretBaker : Baker<TurretAuthoring> {
        
        public override void Bake(TurretAuthoring InAuthoring) {
            AddComponent<Turret>();
        }
        
    }
}