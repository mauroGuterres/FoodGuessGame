using GuessFoodGame.Models;
using GuessFoodGame.Service.Interface;
using System;


namespace GuessFoodGame.Service
{
    

    public class FoodGuessingGameService : IFoodGuessingGameService
    {
        private readonly QuestionNode root;
        private readonly Dictionary<string, QuestionNode> gameSessions;
        private readonly Dictionary<string, QuestionNode> lastGuessedNode;
        private readonly Dictionary<string, string> pendingNewFood;
        private readonly Dictionary<string, bool> isFirstQuestionAsked;

        public FoodGuessingGameService()
        {
            // Initialize the game tree
            root = new QuestionNode("O prato que você pensou é massa?");
            root.YesNode = new QuestionNode("Lasanha", true);
            root.NoNode = new QuestionNode("Bolo de Chocolate", true);

            gameSessions = new Dictionary<string, QuestionNode>();
            lastGuessedNode = new Dictionary<string, QuestionNode>();
            pendingNewFood = new Dictionary<string, string>();
            isFirstQuestionAsked = new Dictionary<string, bool>();
        }

        public string StartNewGame()
        {
            var sessionId = Guid.NewGuid().ToString();
            gameSessions[sessionId] = root;
            isFirstQuestionAsked[sessionId] = false; // controle para primeira questão
            return sessionId;
        }

        public GameResponse ProcessAnswer(string sessionId, string answer)
        {
            if (!gameSessions.TryGetValue(sessionId, out var currentNode))
            {
                return new GameResponse { Message = "sessionId inválida" };
            }

            if (!isFirstQuestionAsked[sessionId])
            {
                isFirstQuestionAsked[sessionId] = true; 
                return new GameResponse { Message = currentNode.Question }; 
            }

            if (pendingNewFood.ContainsKey(sessionId))
            {
                return HandleLearningPhase(sessionId, answer);
            }

            if (currentNode.IsQuestionNode)
            {
                return HandleQuestionAnswer(sessionId, answer, currentNode);
            }
            else
            {
                return HandleGuessAnswer(sessionId, answer, currentNode);
            }
        }

        private GameResponse HandleLearningPhase(string sessionId, string answer)
        {
            if (pendingNewFood[sessionId] == null)
            {
                pendingNewFood[sessionId] = answer;
                var lastFoodItem = lastGuessedNode[sessionId].FoodItem;
                return new GameResponse
                {
                    Message = $"{answer} é _________ mas {lastFoodItem} não é"
                };
            }
            else
            {
                var correctFood = pendingNewFood[sessionId];
                var lastGuessNode = lastGuessedNode[sessionId];


                // Atualiza a árvore do game
                var newFoodNode = new QuestionNode(correctFood, true);
                var oldFoodNode = new QuestionNode(lastGuessNode.FoodItem, true);

                lastGuessNode.Question = $"O prato que você pensou é {answer}?"; 
                lastGuessNode.YesNode = newFoodNode;
                lastGuessNode.NoNode = oldFoodNode;
                lastGuessNode.FoodItem = null; 

                pendingNewFood.Remove(sessionId);
                lastGuessedNode.Remove(sessionId);
                gameSessions.Remove(sessionId);

                return new GameResponse { Message = "Obrigado! Eu aprendi algo novo!" };
            }
        }

        private GameResponse HandleQuestionAnswer(string sessionId, string answer, QuestionNode currentNode)
        {
            currentNode = "sim".Equals(answer, StringComparison.OrdinalIgnoreCase) ? currentNode.YesNode : currentNode.NoNode;
            gameSessions[sessionId] = currentNode;

            if (!currentNode.IsQuestionNode)
            {
                lastGuessedNode[sessionId] = currentNode; // Store the last guessed node
                return new GameResponse { Message = $"O Prato que você pensou é {currentNode.FoodItem}?" };
            }
            else
            {
                return new GameResponse { Message = currentNode.Question };
            }
        }

        private GameResponse HandleGuessAnswer(string sessionId, string answer, QuestionNode currentNode)
        {
            if ("sim".Equals(answer, StringComparison.OrdinalIgnoreCase))
            {
                gameSessions.Remove(sessionId); // Correct guess, end the session
                return new GameResponse { Message = "Acertei de novo!" };
            }
            else
            {
                pendingNewFood[sessionId] = null; // Initiate the learning phase
                return new GameResponse { Message = "Qual prato você pensou?" };
            }
        }

    }

}


