using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
public partial struct BrickSpawnSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SpawnBrickConfig>();
    }
    public void OnUpdate(ref SystemState state)
    {
        SpawnBrickConfig spawnBrickConfig = SystemAPI.GetSingleton<SpawnBrickConfig>();
        EntityCommandBuffer ecb = new(state.WorldUpdateAllocator);

        for (int i = 0; i < spawnBrickConfig.SpawnCount; i++)
        {
            Entity brickEntity = ecb.Instantiate(spawnBrickConfig.BrickPrefab);

            LocalTransform spawnTransform = new()
            {
                Position = new float3(UnityEngine.Random.Range(-spawnBrickConfig.SpawnBound, spawnBrickConfig.SpawnBound), UnityEngine.Random.Range(-spawnBrickConfig.SpawnBound, spawnBrickConfig.SpawnBound), 0), 
                Rotation = quaternion.identity,
                Scale = 1f
            };

            ecb.SetComponent(brickEntity, spawnTransform);
        }

        ecb.Playback(state.EntityManager);
        ecb.Dispose();

        state.Enabled = false;
    }
}