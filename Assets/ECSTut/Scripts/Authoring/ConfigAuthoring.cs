using Unity.Entities;
using UnityEngine;

namespace ECSTut{
    public class ConfigAuthoring : MonoBehaviour{
        public GameObject TankPrefab;
        public int TankCount;
        public float SafeZoneRadius;
    }

    public class ConfigBaker : Baker<ConfigAuthoring>{

        public override void Bake(ConfigAuthoring authoring){
            AddComponent(new Config{
                TankPrefab = GetEntity(authoring.TankPrefab),
                TankCount = authoring.TankCount,
                SafeZoneRadius = authoring.SafeZoneRadius
            });
        }
        
    }
}