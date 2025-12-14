using Unity.Entities;
public class BallComponentAuthoring : UnityEngine.MonoBehaviour
{
    private class Baker : Baker<BallComponentAuthoring>
    {
        public override void Bake(BallComponentAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Ball());
        }
    }
}
public struct Ball : IComponentData
{
    
}