using Unity.Entities;
using UnityEngine;
public class TheVoidAuthoring : MonoBehaviour {
    private class Baker : Baker<TheVoidAuthoring> {
        public override void Bake(TheVoidAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new TheVoid());
        }
    }
}
public struct TheVoid : IComponentData {
    
}