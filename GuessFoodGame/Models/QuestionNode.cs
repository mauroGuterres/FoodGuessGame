

namespace GuessFoodGame.Models
{
    public class QuestionNode
    {
        public string Question { get; set; }
        public string FoodItem { get; set; }
        public bool IsQuestionNode => FoodItem == null;
        public QuestionNode YesNode { get; set; }
        public QuestionNode NoNode { get; set; }

        public QuestionNode(string questionOrFood, bool isFood = false)
        {
            if (isFood)
            {
                FoodItem = questionOrFood;
            }
            else
            {
                Question = questionOrFood;                
            }
        }
    }
}