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

        for (int i = 0; i < 12; i++)
        {
            Entity brickEntity = ecb.Instantiate(spawnBrickConfig.BrickPrefab);

            LocalTransform spawnTransform = new()
            {
                Position = new float3(UnityEngine.Random.Range(-spawnBrickConfig.SpawnBound, spawnBrickConfig.SpawnBound), 1, UnityEngine.Random.Range(-spawnBrickConfig.SpawnBound, spawnBrickConfig.SpawnBound)), 
                Rotation = quaternion.identity,
                Scale = 1f
            };

            ecb.SetComponent(brickEntity, spawnTransform);
            // ecb.SetComponent(brickEntity, new Brick { Hitpoints = UnityEngine.Random.Range(1, spawnBrickConfig.BrickHitpoints + 1) });
        }

        ecb.Playback(state.EntityManager);
        ecb.Dispose();

        state.Enabled = false;
    }
}