using Events;

namespace GameState.Events
{
    public class StartGameEvent : GlobalEvent<StartGameEvent> { }
    public class EndTurnEvent : GlobalEvent<EndTurnEvent> { }

    public class StartCombatEvent : GlobalEvent<StartCombatEvent>
    {
        public StartCombatEvent(params int[] orderedParticipantIds)
        {
            OrderedParticipantIds = orderedParticipantIds;
        }

        public int[] OrderedParticipantIds;
    }

    public class EndCombatEvent : GlobalEvent<EndCombatEvent> { }

    public class TurnEndedEvent : SourceEvent<TurnEndedEvent>
    {
        public TurnEndedEvent(int sourceInstanceId) : base(sourceInstanceId) { }
    }

    public class SpawnEnemiesEvent : GlobalEvent<SpawnEnemiesEvent>
    {
        public SpawnEnemiesEvent(int numberToSpawn)
        {
            NumberToSpawn = numberToSpawn;
        }

        public int NumberToSpawn;
    }

    public class EnemySpawnedEvent : GlobalEvent<EnemySpawnedEvent>
    {
        public EnemySpawnedEvent(int enemyInstanceId) 
        {
            EnemyInstanceId = enemyInstanceId;
        }

        public int EnemyInstanceId;
    }

    public class SpawnPlayerEvent: GlobalEvent<SpawnPlayerEvent> { }
}
