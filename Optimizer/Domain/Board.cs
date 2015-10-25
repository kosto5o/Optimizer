using Optimizer.Utils;

namespace Optimizer.Domain
{
    public class Board : IResettable
    {
        public static readonly int MaxRound = 50;

        public int CurrentRound { get; set; }

        private Deck PlayerDeck { get; set; }

        private Deck EnemyDeck { get; set; }

        private bool SurgeMode { get; set; }

        public Board(Deck playerDeck, Deck enemyDeck, bool surgeMode)
        {
            this.PlayerDeck = playerDeck;
            this.EnemyDeck = enemyDeck;
            this.SurgeMode = surgeMode;

            this.ShuffleDecks();
        }


        public void ShuffleDecks()
        {
            this.PlayerDeck.Cards = this.PlayerDeck.Cards.Shuffle();
            this.EnemyDeck.Cards = this.EnemyDeck.Cards.Shuffle();
        }

        public Deck GetCurrentPlayerDeck()
        {
            return SurgeMode
                ? (PlayerDeck.IsPlayer ? EnemyDeck : PlayerDeck)
                : (PlayerDeck.IsPlayer ? PlayerDeck : EnemyDeck);
        }

        public Deck GetCurrentEnemyDeck()
        {
            return SurgeMode
                ? (PlayerDeck.IsPlayer ? PlayerDeck : EnemyDeck)
                : (PlayerDeck.IsPlayer ? EnemyDeck : PlayerDeck);
        }


        public void ResetToDefault()
        {
            this.CurrentRound = 0;
            this.EnemyDeck.ResetToDefault();
            this.PlayerDeck.ResetToDefault();
            this.ShuffleDecks();
        }
    }
}
