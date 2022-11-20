using Unity.Entities;
using UnityEngine;

namespace ECSTut {
    public class TankAuthoring : MonoBehaviour {
        
    }

    class TankBaker : Baker<TankAuthoring> {
        public override void Bake(TankAuthoring authoring) {
            AddComponent<Tank>();
        }
    }
}