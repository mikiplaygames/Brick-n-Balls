using Unity.Entities;
using Unity.Physics;

public partial struct TheVoidSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SimulationSingleton>();
    }
    public void OnUpdate(ref SystemState state)
    {
        var sim = SystemAPI.GetSingleton<SimulationSingleton>().AsSimulation();
        sim.FinalJobHandle.Complete();

        EntityCommandBuffer ecb = new(state.WorldUpdateAllocator);
        // Check trigger events
        foreach (var triggerEvent in sim.TriggerEvents)
        {
            var entityA = triggerEvent.EntityA;
            var entityB = triggerEvent.EntityB;

            // Check if entityA is a Ball
            if (SystemAPI.HasComponent<Ball>(entityA) && SystemAPI.HasComponent<TheVoid>(entityB))
                ecb.DestroyEntity(entityA);
            else if (SystemAPI.HasComponent<Ball>(entityB) && SystemAPI.HasComponent<TheVoid>(entityA))
                ecb.DestroyEntity(entityB);
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}