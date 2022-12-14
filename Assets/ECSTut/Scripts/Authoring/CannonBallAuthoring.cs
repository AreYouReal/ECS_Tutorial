using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

namespace ECSTut{
    public class CannonBallAuthoring : MonoBehaviour{

    }

    public class CannonBallBaker : Baker<CannonBallAuthoring>{
        public override void Bake(CannonBallAuthoring authoring){
            AddComponent<CannonBall>();
            AddComponent<URPMaterialPropertyBaseColor>();
        }
    }
}