Food Guessing Game API
Purpose
This Food Guessing Game API is a web-based application designed to play a guessing game with users. The game involves thinking of a food item and then answering a series of yes/no questions until the application guesses the food. If the guess is incorrect, the game learns from the user, enhancing its ability to guess correctly in future games.

Requirements
To run this application, you will need:

.NET 6.0 SDK and Runtime
An IDE like Visual Studio, VS Code, or a similar .NET-compatible IDE
Running the Application
Clone the repository to your local machine.
Open the solution in your IDE.
Build the solution to restore NuGet packages.
Run the application. This can typically be done within your IDE or via the command line with dotnet run.
Usage
The application exposes two main endpoints:

Start Game: Initializes a new game session.

Endpoint: GET /api/FoodGuessingGame/startGame
Response: A session ID and a message prompting the user to think of a food item.
Answer Question: Processes the user's answer and navigates through the game.

Endpoint: POST /api/FoodGuessingGame/answer
Request Body: JSON object containing the session ID and user's answer.
Response: Depending on the state of the game, a question, guess, or learning prompt.
Swagger Documentation

The API also includes Swagger documentation for easy interaction and testing. Access the Swagger UI at:

URL: /api-docs
Swagger provides a visual interface for testing the API endpoints, viewing request/response formats, and understanding the API's capabilities.

Example Requests
Here's how you can interact with the API:

Start a New Game Session:


GET /api/FoodGuessingGame/startGame
Response:


{
    "sessionId": "some-session-id",
    "message": "Por favor, pense em um prato que gosta."
}
Answer a Question:

Once you have your sessionId, you can start answering questions:


POST /api/FoodGuessingGame/answer
Content-Type: application/json
{
    "sessionId": "some-session-id",
    "answer": "sim" // or "nao"
}
Response:


{
    "message": "Next question or a guess from the game"
}
Continue interacting with the API using the session ID until the game makes a guess. If the guess is incorrect, you will be prompted to teach the game about your food.
