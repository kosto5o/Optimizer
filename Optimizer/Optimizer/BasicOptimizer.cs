using Optimizer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Logger.Log("{0} turn:", currentPlayerDeck.GetDeckName());
                var winningDeck = PlayerTurn(currentPlayerDeck, currentEnemyDeck);
                if (winningDeck != null)
                {
                    intermediateResult.NumberOfPlayerWins += 1;
                    return intermediateResult;
                }

                // Enemy turn
                Logger.Log("{0} turn:", currentEnemyDeck.GetDeckName());
                winningDeck = PlayerTurn(currentEnemyDeck, currentPlayerDeck);
                if (winningDeck != null)
                {
                    intermediateResult.NumberOfPlayerLosses += 1;
                    return intermediateResult;
                }

                // Decrease delay
                currentPlayerDeck.PlayedCards.ForEach(c => c.Delay -= 1);
                currentEnemyDeck.PlayedCards.ForEach(c => c.Delay -= 1);
            }

            return intermediateResult;
        }

        private static Deck PlayerTurn(Deck currentPlayerDeck, Deck currentEnemyDeck)
        {
            var newPlayerCard2 = currentPlayerDeck.Cards.Count > 0 ? currentPlayerDeck.Cards.Pop() : null;

            if (newPlayerCard2 != null)
                currentPlayerDeck.PlayedCards.Add(newPlayerCard2);

            foreach (var card in currentPlayerDeck.PlayedCards)
            {
                if (card.CanAttack())
                {
                    Logger.Log("{0} Card {1} : Basic attack ({2})", currentPlayerDeck.GetDeckName(), card.Name,
                        card.Attack);

                    // Attack commander if no enemy cards on board
                    if (currentEnemyDeck.PlayedCards.Count == 0)
                    {
                        var originalHealth = currentEnemyDeck.Commander.Health;
                        currentEnemyDeck.Commander.Health -= card.Attack;
                        Logger.Log("{0} Commander health: {1} ({2}-{3})", currentEnemyDeck.GetDeckName(),
                            currentEnemyDeck.Commander.Health, originalHealth, card.Attack);

                        
                        // If Commander health is bellow 0, game over
                        if (currentEnemyDeck.Commander.Health <= 0)
                        {
                            Logger.Log("{0} Win", currentPlayerDeck.GetDeckName());
                            return currentPlayerDeck;
                        }
                    }
                    else
                    {
                        Card enemyCard = currentEnemyDeck.PlayedCards[0];
                        var originalCardHealth = enemyCard.Health;
                        enemyCard.Health -= card.Attack;
                        Logger.Log("{0} card health: {1} ({2}-{3})", currentEnemyDeck.GetDeckName(),
                            enemyCard.Health, originalCardHealth, card.Attack);

                        // If enemy card health drop bellow 0, remove
                        if (enemyCard.Health <= 0)
                        {
                            Logger.Log("{0} card {1} removed", currentEnemyDeck.GetDeckName(), enemyCard.Name);
                            currentEnemyDeck.PlayedCards.Remove(enemyCard);
                        }
                    }
                }
            }

            return null;
        }
    }
}
