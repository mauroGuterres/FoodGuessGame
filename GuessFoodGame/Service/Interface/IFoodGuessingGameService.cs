using GuessFoodGame.Models;

namespace GuessFoodGame.Service.Interface
{
    public interface IFoodGuessingGameService
    {
        string StartNewGame();
        GameResponse ProcessAnswer(string sessionId, string answer);        
    }
}
