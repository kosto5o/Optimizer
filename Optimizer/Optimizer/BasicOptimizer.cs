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
                    return UpdateResult(intermediateResult, winningDeck);
                }

                // Enemy turn
                Logger.Log($"{currentEnemyDeck.GetDeckName()} turn:");
                winningDeck = PlayerTurn(currentEnemyDeck, currentPlayerDeck);
                if (winningDeck != null)
                {
                    return UpdateResult(intermediateResult, winningDeck);                    
                }

                // Decrease delay
                currentPlayerDeck.PlayedCards.ForEach(c => c.Delay--);
                currentEnemyDeck.PlayedCards.ForEach(c => c.Delay--);
            }
            
            // If no player win in 50 turns, its draw 
            intermediateResult.NumberOfPlayerDraws++;

            return intermediateResult;
        }

        private SimulationResult UpdateResult(SimulationResult intermediateResult, Deck winningDeck)
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

        private static Deck PlayerTurn(Deck currentPlayerDeck, Deck currentEnemyDeck)
        {
            // Draw new card
            currentPlayerDeck.DrawNewCard();

            // Commander turn
            // Commander doesn't have Basic attack, however does skill attacks
            // TODO: Commander turn

            // Play Deck
            foreach (var card in currentPlayerDeck.PlayedCards)
            {
                if (card.CanAttack())
                {
                    if (card.CardType == CardType.Tower)
                    {
                        // No Basic attack, only skills
                        // TODO: Tower attack
                    }
                    else
                    {
                        // Do Skill Attack first
                        foreach (var skill in card.Skills)
                        {
                            skill.Attack(currentPlayerDeck, currentEnemyDeck, card);
                        }

                        // Play Basic Attack
                        Deck deck = card.BasicAttack(currentPlayerDeck, currentEnemyDeck);

                        // Check if there is winner
                        if (deck != null)
                            return deck;

                    }
                }
            }

            return null;
        }
        
        

       

        
    }
}
