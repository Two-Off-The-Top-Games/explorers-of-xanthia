using Events;

namespace GameState.Events
{
    public class StartGameEvent : GlobalEvent<StartGameEvent> { }
    public class EndTurnEvent : GlobalEvent<EndTurnEvent> { }

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

    public class SpawnCharactersEvent: GlobalEvent<SpawnCharactersEvent>
    {
        public SpawnCharactersEvent(int numberToSpawn)
        {
            NumberToSpawn = numberToSpawn;
        }

        public int NumberToSpawn;
    }

    public class CharacterSpawnedEvent : GlobalEvent<CharacterSpawnedEvent>
    {
        public CharacterSpawnedEvent(int characterInstanceId)
        {
            CharacterInstanceId = characterInstanceId;
        }

        public int CharacterInstanceId;
    }

    public class CombatEvent : GlobalEvent<CombatEvent>
    {
        public CombatEvent(string combatEventText)
        {
            CombatEventText = combatEventText;
        }

        public string CombatEventText;
    }
}
