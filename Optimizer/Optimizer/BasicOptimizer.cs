using System.Collections.Generic;
using System.Linq;
using Optimizer.Domain;
using Optimizer.Utils;

namespace Optimizer.Optimizer
{
    public class BasicOptimizer
    {
        public SimulationResult RunOptimization(Deck playerDeck, Deck enemyDeck, bool surgeMode, int iterations)
        {          
            Board board = new Board(playerDeck, enemyDeck, surgeMode);

            SimulationResult result = new SimulationResult();
            result.NumberOfIterations = iterations;

            for (int currentIteration = 0; currentIteration < iterations; currentIteration++)
            {
                board.ResetToDefault();

                RunSimulation(board, result);
            }

            return result;

        }

        private SimulationResult RunSimulation(Board board, SimulationResult intermediateResult)
        {
            for (int round = 1; round <= Board.MaxRound; round++)
            {
                Logger.Log("Round {0}", round);

                // Draw new card
                Deck currentPlayerDeck = board.GetCurrentPlayerDeck();
                Deck currentEnemyDeck = board.GetCurrentEnemyDeck();

                // Player turn
                Logger.Log($"{currentPlayerDeck.GetDeckName()} turn:");
                var winningDeck = PlayerTurn(currentPlayerDeck, currentEnemyDeck);
                if (winningDeck != null)
                {
                    if (winningDeck.IsPlayer)
                    {
                        intermediateResult.NumberOfPlayerWins ++;
                    }
                    else
                    {
                        intermediateResult.NumberOfPlayerLosses++;
                    }

                    return intermediateResult;
                }

                // Enemy turn
                Logger.Log($"{currentEnemyDeck.GetDeckName()} turn:");
                winningDeck = PlayerTurn(currentEnemyDeck, currentPlayerDeck);
                if (winningDeck != null)
                {
                    if (winningDeck.IsPlayer)
                    {
                        intermediateResult.NumberOfPlayerWins++;
                    }
                    else
                    {
                        intermediateResult.NumberOfPlayerLosses++;
                    }
                    return intermediateResult;
                }

                // Decrease delay
                currentPlayerDeck.PlayedCards.ForEach(c => c.Delay--);
                currentEnemyDeck.PlayedCards.ForEach(c => c.Delay--);
            }
            
            // If no player win in 50 turns, its draw 
            intermediateResult.NumberOfPlayerDraws++;

            return intermediateResult;
        }

        private static Deck PlayerTurn(Deck currentPlayerDeck, Deck currentEnemyDeck)
        {
            // Draw new card
            var newPlayerCard = DrawNewCard(currentPlayerDeck);

            // Play Deck
            foreach (var card in currentPlayerDeck.PlayedCards)
            {
                if (card.CanAttack())
                {
                    if (card.CardType == CardType.Tower)
                    {
                        // No Basic attack, only skills
                    }
                    else
                    {
                        // Do Skill Attack first
                        foreach (var skill   in card.Skills)
                        {
                            if (skill.SkillType == SkillType.Siege)
                            {
                                SiegeAttack(currentPlayerDeck, currentEnemyDeck, card, skill);
                            }
                        }

                        // Play Basic Attack
                        Deck deck = BasicAttack(currentPlayerDeck, currentEnemyDeck, card);

                        // Check if there is winner
                        if (deck != null)
                            return deck;

                    }
                }
            }

            return null;
        }

        private static void SiegeAttack(Deck currentPlayerDeck, Deck currentEnemyDeck, Card card, Skill currentSkill)
        {
            Logger.Log($"{currentPlayerDeck.GetDeckName()} Card {card.Name} : Siege attack ({currentSkill.Power})");

            if (currentSkill.CanAttack())
            {                
                var playedEnemyTowerCards = currentEnemyDeck.PlayedCards.Select(c => c).Where(c => c.CardType == CardType.Tower).ToList();

                // Attack first tower card, if there are no Tower cards, skip
                if (playedEnemyTowerCards.Count > 0)
                {
                    var towerCard = playedEnemyTowerCards[0];

                    var originalCardHealth = towerCard.Health;
                    towerCard.Health -= currentSkill.Power;

                    Logger.Log(
                        $"{currentEnemyDeck.GetDeckName()} card health: {towerCard.Health} ({originalCardHealth}-{currentSkill.Power})");

                    // If enemy card health drop bellow 0, remove
                    if (towerCard.Health <= 0)
                    {
                        Logger.Log($"{currentEnemyDeck.GetDeckName()} card {towerCard.Name} removed");
                        currentEnemyDeck.PlayedCards.Remove(towerCard);
                    }

                    currentSkill.ResetCounter();
                }
                
            }
            else
            {
                currentSkill.RaiseCounter();
            }



        }

        private static Card DrawNewCard(Deck currentPlayerDeck)
        {
            var newPlayerCard = currentPlayerDeck.Cards.Count > 0 ? currentPlayerDeck.Cards.Pop() : null;

            if (newPlayerCard != null)
            {
                if (newPlayerCard.CardType == CardType.Assault)
                {
                    currentPlayerDeck.PlayedCards.Add(newPlayerCard);
                }
                else if (newPlayerCard.CardType == CardType.Tower)
                {
                    currentPlayerDeck.PlayedCards.Insert(0, newPlayerCard);
                }

                Logger.Log(
                    $"{currentPlayerDeck.GetDeckName()} Card {newPlayerCard.Name} put on table (Delay: {newPlayerCard.Delay} )");
            }
            return newPlayerCard;
        }

        private static Deck BasicAttack(Deck currentPlayerDeck, Deck currentEnemyDeck, Card card)
        {
            Logger.Log($"{currentPlayerDeck.GetDeckName()} Card {card.Name} : Basic attack ({card.Attack})");

            // Attack commander if no enemy cards on board
            var playedAssaultCards = currentEnemyDeck.PlayedCards.Select(c=>c).Where(c => c.CardType == CardType.Assault).ToList();

            if (playedAssaultCards.Count == 0)
            {
                var originalHealth = currentEnemyDeck.Commander.Health;
                currentEnemyDeck.Commander.Health -= card.Attack;
                Logger.Log(
                    $"{currentEnemyDeck.GetDeckName()} Commander health: {currentEnemyDeck.Commander.Health} ({originalHealth}-{card.Attack})");


                // If Commander health is bellow 0, game over
                if (currentEnemyDeck.Commander.Health <= 0)
                {
                    Logger.Log($"{currentPlayerDeck.GetDeckName()} Win");
                    {
                        return currentPlayerDeck;                        
                    }
                }
            }
            else
            {
                Card enemyCard = currentEnemyDeck.PlayedCards.First(c=>c.CardType == CardType.Assault);

                var originalCardHealth = enemyCard.Health;
                enemyCard.Health -= card.Attack;

                Logger.Log(
                    $"{currentEnemyDeck.GetDeckName()} card health: {enemyCard.Health} ({originalCardHealth}-{card.Attack})");

                // If enemy card health drop bellow 0, remove
                if (enemyCard.Health <= 0)
                {
                    Logger.Log($"{currentEnemyDeck.GetDeckName()} card {enemyCard.Name} removed");
                    currentEnemyDeck.PlayedCards.Remove(enemyCard);
                }
            }
            return null;
        }
    }
}
