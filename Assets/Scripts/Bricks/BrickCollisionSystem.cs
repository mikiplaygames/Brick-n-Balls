using Unity.Entities;
using Unity.Physics;
using UnityEngine.Events;

public partial struct BrickCollisionSystem : ISystem
{
    public static UnityEvent OnBrickHit = new();
    public void OnUpdate(ref SystemState state)
    {
        var sim = SystemAPI.GetSingleton<SimulationSingleton>().AsSimulation();

        sim.FinalJobHandle.Complete(); 

        var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);

        foreach (var collisionEvent in sim.CollisionEvents)
        {
            Entity brickEntity;

            if (SystemAPI.HasComponent<Brick>(collisionEvent.EntityA) && SystemAPI.HasComponent<Ball>(collisionEvent.EntityB))
                brickEntity = collisionEvent.EntityA;
            else if (SystemAPI.HasComponent<Brick>(collisionEvent.EntityB) && SystemAPI.HasComponent<Ball>(collisionEvent.EntityA))
                brickEntity = collisionEvent.EntityB;
            else
                continue;

            // reduce brick hitpoints
            GameManager.Score++;
            OnBrickHit.Invoke();

            var brick = SystemAPI.GetComponentRW<Brick>(brickEntity);
            if (--brick.ValueRW.Hitpoints <= 0)
                ecb.DestroyEntity(brickEntity);
        }

        ecb.Playback(state.EntityManager);
    }
}