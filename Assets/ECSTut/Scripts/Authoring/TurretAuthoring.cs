using Unity.Entities;
using UnityEngine;

namespace ECSTut {
    public class TurretAuthoring : MonoBehaviour{
        public GameObject CannonBallPrefab;
        public Transform CannonBallSpawn;
    }

    class TurretBaker : Baker<TurretAuthoring> {
        
        public override void Bake(TurretAuthoring InAuthoring) {
            AddComponent(new Turret{
                CannonBallPrefab =  GetEntity(InAuthoring.CannonBallPrefab),
                CannonBallSpawn = GetEntity(InAuthoring.CannonBallSpawn)
            });
            
            AddComponent<Shooting>();
        }
        
    }
}