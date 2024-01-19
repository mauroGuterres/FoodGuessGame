<h1>API de Jogo de Adivinhação de Comidas</h1>

Propósito: Esta API de Jogo de Adivinhação de Comidas é uma aplicação baseada na web projetada para jogar um jogo de adivinhação com os usuários. O jogo envolve pensar em um item alimentar e, em seguida, responder a uma série de perguntas de sim/não até que a aplicação adivinhe a comida. Se o palpite estiver incorreto, o jogo aprende com o usuário, aprimorando sua capacidade de adivinhar corretamente em jogos futuros.

<h3>Para executar esta aplicação, você precisará de:</h3>

SDK e Runtime do .NET 6.0
Uma IDE como Visual Studio, VS Code, ou uma IDE compatível com .NET semelhante

<h3>Executando a Aplicação:</h3>

Clone o repositório para sua máquina local.
Abra a solução na sua IDE.
Build a solução para restaurar os pacotes NuGet.
Execute a aplicação. Isso geralmente pode ser feito dentro da sua IDE ou via linha de comando com dotnet run. </br>
Uso: A aplicação expõe dois principais endpoints:

Iniciar Jogo: Inicializa uma nova sessão de jogo.

Endpoint: GET /api/FoodGuessingGame/startGame
Resposta: Um ID de sessão e uma mensagem pedindo ao usuário para pensar em um item alimentar.
Responder Pergunta: Processa a resposta do usuário e navega pelo jogo.

Endpoint: POST /api/FoodGuessingGame/answer
Corpo da Requisição: Objeto JSON contendo o ID da sessão e a resposta do usuário.
Resposta: Dependendo do estado do jogo, uma pergunta, palpite ou prompt de aprendizado.
Documentação Swagger

A API também inclui documentação Swagger para fácil interação e teste.

O Swagger oferece uma interface visual para testar os endpoints da API, visualizar formatos de solicitação/resposta e entender as capacidades da API.
