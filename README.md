#ApiDistribuicaoLucros

Essa solução foi desenvolvida para calcular a distribuição de participação nos lucros de uma empresa.
Levando em consideração atributos que influenciam no resultado, como: Tempo de admissão, área de atuação na empresa, cargo e salário.

Nesse projeto utilizei o .NET Core 3.1, Firebase e inclui o swagger para documentar os endpoints.


Para rodar a solução será necessário os seguintes passos: 
- Instalar VisualStudio 2019 e SDK .NetCore 3.1;
- Para iniciar clicar no botão start no visualStudio ou tecla F5 que será aberta a tela do swagger com a documentação;
- Na tela do swagger vá até o método GET da api ProfitDistribution. Inclua um valor como parâmetro para ser distribuidos aos funcionários ;

Os dados do funcionários foram carregados previamente no firebase. Fique a vontade caso queira deletar a coleção e criar outra.